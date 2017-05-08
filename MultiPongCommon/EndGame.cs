namespace MultiPongCommon
{
    public class EndGame : Message
    {
        public EndGame()
        {
            MessageType = MessageType.EndGame;
        }
    }
}