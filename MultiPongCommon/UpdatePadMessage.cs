using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MultiPongCommon
{
    public class UpdatePadMessage : Message
    {
        public Vector2 PadPosition { get; set; }

        public UpdatePadMessage(Vector2 padPosition, int playerId)
        {
            PadPosition = padPosition;
            PlayerId = playerId;

            MessageType = MessageType.UpdatePad;
        }

        public override byte[] GetBytes()
        {
            List<byte> result = new List<byte>();
            byte[] baseBytes = base.GetBytes();
            result.AddRange(baseBytes);
            result.AddRange(PadPosition.GetBytes());
            return result.ToArray();
        }
    }
}