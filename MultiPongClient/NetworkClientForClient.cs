using System.Net;
using System.Net.Sockets;
using MultiPongCommon;

namespace MultiPongClient
{
    public class NetworkClientForClient : NetworkClientBase
    {
        private TcpClient client;

        public void Connect(IPEndPoint endPoint)
        {
            client = new TcpClient();
            client.Connect(endPoint);

            StartReceiveAsyncInNewThread(client);
        }

        public override void Send(Message message)
        {
            message.SenderStream = client.GetStream();
            base.Send(message);
        }
    }
}