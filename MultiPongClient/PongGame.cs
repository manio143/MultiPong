﻿using System;
using System.Diagnostics;
using System.Net;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MultiPongCommon;

namespace MultiPongClient
{
    public class PongGame : Game
    {
        GraphicsDeviceManager gdm;
        SpriteBatch spriteBatch;

        Ball ball;

        Pad player1, player2;

        private bool running = true;
        private NetworkClientForClient networkClient = new NetworkClientForClient();
        private byte myId;
        private bool nextUpdate = true;

        public PongGame(IPAddress address)
        {
            gdm = new GraphicsDeviceManager(this);
            networkClient.Connect(new IPEndPoint(address, 7575));
        }

        protected override void Initialize()
        {
            base.Initialize();
            spriteBatch = new SpriteBatch(gdm.GraphicsDevice);
            ball = new Ball(createCircleText(Ball.RADIUS), Constants.BALL_INITIAL_POSITION,
                Constants.INITIAL_VELOCITY);

            player1 = new Pad(createRectangle(Constants.PAD_WIDTH, Constants.PAD_LENGTH),
                Constants.PLAYER1_INITIAL_POSITION);
            player2 = new Pad(createRectangle(Constants.PAD_WIDTH, Constants.PAD_LENGTH),
                Constants.PLAYER2_INITIAL_POSITION);

            networkClient.Send(new RegisterMessage());
            var message = networkClient.ReceiveBlocking();
            if (message is RegisterRejection)
                throw new ApplicationException("The server is busy");
            if (message is RegisterConfirmation)
            {
                myId = (message as RegisterConfirmation).PlayerId;
            }
            else throw new ApplicationException("Unexpected message received");
        }

        protected override void Draw(GameTime gameTime)
        {
            gdm.GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            ball.Draw(spriteBatch);
            player1.Draw(spriteBatch);
            player2.Draw(spriteBatch);
            spriteBatch.End();
        }

        protected override void Update(GameTime gameTime)
        {
            if (!running) return;
            if (nextUpdate)
            {
                networkClient.Send(new GetStateMessage() { PlayerId = myId });
                nextUpdate = false;
            }
            var message = networkClient.Receive();
            if (message != null)
            {
                var stateMessage = message as StateMessage;
                if (stateMessage != null)
                {
                    ball.Move(stateMessage.BallPosition);
                    Debug.WriteLine($"New ball position: {stateMessage.BallPosition.X}, {stateMessage.BallPosition.Y}");
                    player1.Move(stateMessage.Player1Position);
                    player2.Move(stateMessage.Player2Position);
                    Window.Title = $"MultiPong - {stateMessage.Player1Score} : {stateMessage.Player2Score}";
                }
                else if (message is EndGame)
                {
                    var winner = (message as EndGame).Winner;
                    Window.Title += $" winner: {winner}";
                    running = false;
                }
                else throw new ApplicationException("Unexpected message received");

                movePad();

                nextUpdate = true;
            }
            base.Update(gameTime);

        }

        private void movePad()
        {
            var mouseState = Mouse.GetState();
            var myX = myId == 1 ? player1.Position.X : player2.Position.X;
            var updateMessage = new UpdatePadMessage(new Vector2(myX, mouseState.Y), myId);
            networkClient.Send(updateMessage);
        }

        public Texture2D createRectangle(int width, int height)
        {
            Texture2D texture = new Texture2D(gdm.GraphicsDevice, width, height);
            Color[] colorData = new Color[width * height];

            for (int i = 0; i < colorData.Length; i++)
                colorData[i] = Color.DarkBlue;

            texture.SetData(colorData);
            return texture;
        }

        public Texture2D createCircleText(int diameter)
        {
            Texture2D texture = new Texture2D(gdm.GraphicsDevice, diameter, diameter);
            Color[] colorData = new Color[diameter * diameter];

            float radius = diameter / 2f;
            float diamsq = radius * radius;

            for (int x = 0; x < diameter; x++)
            {
                for (int y = 0; y < diameter; y++)
                {
                    int index = x * diameter + y;
                    Vector2 pos = new Vector2(x - radius, y - radius);
                    if (pos.LengthSquared() <= diamsq)
                    {
                        colorData[index] = Color.White;
                    }
                    else
                    {
                        colorData[index] = Color.Transparent;
                    }
                }
            }

            texture.SetData(colorData);
            return texture;
        }
    }
}