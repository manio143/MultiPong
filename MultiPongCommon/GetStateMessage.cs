namespace MultiPongCommon
{
    public class GetStateMessage : Message
    {
        public GetStateMessage()
        {
            MessageType = MessageType.GetState;
        }

        public override byte[] GetBytes()
        {
            return base.GetBytes();
        }
    }
}