namespace MultiPongCommon
{
    public class GetStateMessage : Message
    {
        public GetStateMessage()
        {
            MessageType = MessageType.GetState;
        }
    }
}