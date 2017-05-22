using System;
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

        private NetworkClientForClient networkClient = new NetworkClientForClient();
        private byte myId;

        public PongGame()
        {
            gdm = new GraphicsDeviceManager(this);
            networkClient.Connect(new IPEndPoint(IPAddress.Loopback, 7575));
            //TODO: add paramter to program for server address
        }

        protected override void Initialize()
        {
            base.Initialize();
            spriteBatch = new SpriteBatch(gdm.GraphicsDevice);
            ball = new Ball(createCircleText(Ball.RADIUS), Constants.BALL_INITIAL_POSITION,
                Constants.INITIAL_VELOCITY, Constants.SCREEN_HEIGHT);

            player1 = new Pad(createRectangle(Constants.PAD_WIDTH, Constants.PAD_LENGTH),
                Constants.PLAYER1_INITIAL_POSITION);
            player2 = new Pad(createRectangle(Constants.PAD_WIDTH, Constants.PAD_LENGTH),
                Constants.PLAYER2_INITIAL_POSITION);

            networkClient.Send(new RegisterMessage());
            var message = networkClient.Receive();
            if (message is RegisterRejection)
                throw new ApplicationException("The server is busy");
            if (message is RegisterConfirmation)
            {
                //TODO: make id a field and use it for pad update
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
            networkClient.Send(new GetStateMessage(){PlayerId = myId});
            var message = networkClient.Receive();
            var stateMessage = message as StateMessage;
            if (stateMessage != null)
            {
                ball.Move(stateMessage.BallPosition);
                Debug.WriteLine($"New ball position: {stateMessage.BallPosition.X}, {stateMessage.BallPosition.Y}");
                player1.Move(stateMessage.Player1Position);
                player2.Move(stateMessage.Player2Position);
            }
            else if (message is EndGame)
            {
                var winner = (message as EndGame).Winner;
                //TODO: Display winner
                //Handle exit
            }
            else throw new ApplicationException("Unexpected message received");

            base.Update(gameTime);
            ball.Update(gameTime);

            var mouseState = Mouse.GetState();
            //TODO: calculate difference in mouseState Y
            //var diff
            //TODO: networkClient.Send(new UpdatePadMessage(...);
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

        public Texture2D createCircleText(int radius)
        {
            Texture2D texture = new Texture2D(gdm.GraphicsDevice, radius, radius);
            Color[] colorData = new Color[radius * radius];

            float diam = radius / 2f;
            float diamsq = diam * diam;

            for (int x = 0; x < radius; x++)
            {
                for (int y = 0; y < radius; y++)
                {
                    int index = x * radius + y;
                    Vector2 pos = new Vector2(x - diam, y - diam);
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