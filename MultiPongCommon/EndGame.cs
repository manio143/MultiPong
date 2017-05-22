namespace MultiPongCommon
{
    public class EndGame : Message
    {
        public EndGame(byte winner)
        {
            MessageType = MessageType.EndGame;
            Winner = winner;
        }

        public byte Winner { get; }
    }
}