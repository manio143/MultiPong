namespace MultiPongCommon
{
    public class RegisterConfirmation : Message
    {
        public RegisterConfirmation(byte playerId)
        {
            PlayerId = playerId;
            MessageType = MessageType.RegisterConfirmation;
        }
    }
}