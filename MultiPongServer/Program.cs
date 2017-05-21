using System;
using System.Collections.Generic;
using System.Diagnostics;
using MultiPongCommon;

namespace MultiPongServer
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Starting server...");

            var program = new Program();

            program.Loop();

            Console.WriteLine("Press any key to exit");
            Console.ReadKey(false);
        }

        public Program()
        {
            networkClient = new NetworkClient();
            networkClient.ListenAsync();
        }

        private INetworkClient networkClient;

        private Stopwatch stopwatch;

        private GameState gameState = new GameState(
            Constants.BALL_INITIAL_POSITION,
            Constants.PLAYER1_INITIAL_POSITION,
            Constants.PLAYER2_INITIAL_POSITION,
            Constants.INITIAL_VELOCITY,
            Constants.SCREEN_HEIGHT);
        
        public void Loop()
        {
            TimeSpan previous, current;

            stopwatch = new Stopwatch();
            stopwatch.Start();
            previous = current = stopwatch.Elapsed;

            while (true)
            {
                var message = networkClient.Receive();
                Handle(message);
                current = stopwatch.Elapsed;
                gameState.Update(TimeSpan.FromTicks(current.Ticks - previous.Ticks));
                previous = current;
            }
        }

        private void Handle(Message message)
        {
            Message messageToSend;
            switch (message.MessageType)
            {
                case MessageType.Register:
                    if (gameState.Players >= 2)
                    {
                        messageToSend = new RegisterRejection();
                        messageToSend.SenderStream = message.SenderStream;
                        networkClient.Send(messageToSend);
                    }
                    else
                    {
                        ++gameState.Players;
                        messageToSend = new RegisterConfirmation(gameState.Players);
                        messageToSend.SenderStream = message.SenderStream;
                        networkClient.Send(messageToSend);
                    }
                    return;

                case MessageType.GetState:
                    messageToSend = new StateMessage(gameState.DummyBall.position, gameState.Player1.position, gameState.Player2.position);
                    messageToSend.SenderStream = message.SenderStream;
                    networkClient.Send(messageToSend);
                    return;
            }

            throw new Exception("Handle: invalid message type");
        }
    }
}