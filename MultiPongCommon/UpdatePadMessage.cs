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
            byte[] b1 = base.GetBytes();
            result.AddRange(b1);
            result.AddRange(PadPosition.GetBytes());
            result[0] = (byte)result.Count;
            return result.ToArray();
        }
    }
}