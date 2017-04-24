using System.Net;

namespace MultiPongCommon
{
    public interface INetworkClient
    {
        Message Receive();
        void Send(Message message);
        void ListenAsync();
        void Connect(IPEndPoint endPoint);
    }
}