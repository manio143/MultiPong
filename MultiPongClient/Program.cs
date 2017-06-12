using System.Net;

namespace MultiPongClient
{
    public class Program
    {
        static void Main(string[] args)
        {
            var ipaddr = args.Length < 1 ? IPAddress.Loopback : Dns.GetHostAddresses(args[0])[0];
            var game = new PongGame(ipaddr);
            game.Run();
        }
    }
}

