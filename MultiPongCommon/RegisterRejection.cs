namespace MultiPongCommon
{
    public class RegisterRejection : Message
    {
        public RegisterRejection()
        {
            MessageType = MessageType.RegesterRejection;
        }
    }
}