namespace MultiPongCommon
{
    public class RegisterMessage : Message
    {
        public RegisterMessage()
        {
            MessageType = MessageType.Register;
        }

        public override byte[] GetBytes()
        {
            return base.GetBytes();
        }
    }
}