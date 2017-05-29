using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MultiPongCommon
{
    public class StateMessage : Message
    {
        public Vector2 BallPosition { get; set; }
        public Vector2 Player1Position { get; set; }
        public Vector2 Player2Position { get; set; }

        internal byte PlayerScore;

        public byte Player1Score
        {
            get { return (byte) (PlayerScore >> 4); }
            set
            {
                var lower = (PlayerScore << 4) >> 4;
                var higher = value;
                PlayerScore = (byte) (lower + (higher << 4));
            }
        }

        public byte Player2Score
        {
            get { return (byte) ((PlayerScore << 4) >> 4); }
            set
            {
                var lower = value;
                var higher = PlayerScore >> 4;
                PlayerScore = (byte) (lower + (higher << 4));
            }
        }

        public StateMessage(Vector2 ballPosition, Vector2 player1Position, Vector2 player2Position)
        {
            BallPosition = ballPosition;
            Player1Position = player1Position;
            Player2Position = player2Position;

            MessageType = MessageType.State;
        }

        public override byte[] GetBytes()
        {
            List<byte> result = new List<byte>();
            byte[] baseBytes = base.GetBytes();
            result.AddRange(baseBytes);
            result.AddRange(BallPosition.GetBytes());
            result.AddRange(Player1Position.GetBytes());
            result.AddRange(Player2Position.GetBytes());
            result.Add(PlayerScore);
            return result.ToArray();
        }
    }
}