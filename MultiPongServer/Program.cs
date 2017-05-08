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

        private GameState gameState;

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
            //TODO: switch(message.MessageType)
        }
    }
}