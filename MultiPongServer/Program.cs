using System;
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
            networkClient = new NetworkClientForServer();
            networkClient.ListenAsync();
        }

        private NetworkClientForServer networkClient;

        private Stopwatch stopwatch;

        private bool started;

        private GameState gameState = new GameState(
            Constants.BALL_INITIAL_POSITION,
            Constants.PLAYER1_INITIAL_POSITION,
            Constants.PLAYER2_INITIAL_POSITION,
            Constants.INITIAL_VELOCITY,
            Constants.SCREEN_HEIGHT);

        TimeSpan previous, current;

        public void Loop()
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();

            while (true)
            {
                var message = networkClient.Receive();
                Handle(message);
                current = stopwatch.Elapsed;
                if (started)
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
                    if (gameState.CanAddNewPlayer())
                    {
                        ++gameState.Players;
                        messageToSend = new RegisterConfirmation(gameState.Players);
                        messageToSend.SenderStream = message.SenderStream;
                        networkClient.Send(messageToSend);
                        if (!gameState.CanAddNewPlayer())
                        {
                            previous = current = stopwatch.Elapsed;
                            started = true;
                        }
                    }
                    else
                    {
                        messageToSend = new RegisterRejection();
                        messageToSend.SenderStream = message.SenderStream;
                        networkClient.Send(messageToSend);
                    }
                    break;

                case MessageType.GetState:
                    messageToSend = new StateMessage(gameState.DummyBall.Position, gameState.Player1.Position,
                        gameState.Player2.Position);
                    messageToSend.SenderStream = message.SenderStream;
                    networkClient.Send(messageToSend);
                    break;

                case MessageType.UpdatePad:
                    var updatePadMessage = message as UpdatePadMessage;
                    if(message != null)
                    {
                        switch(updatePadMessage.PlayerId)
                        {
                            case 1:
                                gameState.Player1.Move(updatePadMessage.PadPosition);
                                break;

                            case 2:
                                gameState.Player2.Move(updatePadMessage.PadPosition);
                                break;

                            default:
                                throw new Exception("Handle: invalid player id");
                        }
                    }
                    else throw new Exception("Handle: invalid update pad message");
                    break;

                default:
                    throw new Exception("Handle: invalid message type");
            }
        }
    }
}