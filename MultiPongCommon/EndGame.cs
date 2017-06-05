using System.Linq;

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

        public override byte[] GetBytes()
        {
            return base.GetBytes().Concat(new byte[]{Winner}).ToArray();
        }
    }
}