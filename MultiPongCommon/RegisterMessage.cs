namespace MultiPongCommon
{
    public class RegisterMessage : Message
    {
        public RegisterMessage()
        {
            MessageType = MessageType.Register;
        }
    }
}