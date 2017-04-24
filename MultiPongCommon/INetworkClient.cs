namespace MultiPongCommon
{
    public interface INetworkClient
    {
        Message Receive();
        void Send(Message message);
    }
}