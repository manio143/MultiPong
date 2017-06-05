using System;
using System.Linq;
using System.Net;

namespace MultiPongClient
{
    public class Program
    {
        static int Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("USAGE: MultiPongClient server\n");
                return 1;
            }
            var serverIP = Dns.GetHostAddresses(args[0]).First();
            var game = new PongGame(serverIP);
            game.Run();
            return 0;
        }
    }
}

