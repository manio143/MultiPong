namespace MultiPongCommon
{
    public class RegisterConfirmation : Message
    {
        public RegisterConfirmation(int playerId)
        {
            PlayerId = playerId;
            MessageType = MessageType.RegisterConfirmation;
        }

        public override byte[] GetBytes()
        {
            return base.GetBytes();
        }
    }
}