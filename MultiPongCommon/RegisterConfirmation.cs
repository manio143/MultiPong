namespace MultiPongCommon
{
    public class RegisterConfirmation : Message
    {
        public RegisterConfirmation(int playerId)
        {
            PlayerId = playerId;
            MessageType = MessageType.RegisterConfirmation;
        }
    }
}