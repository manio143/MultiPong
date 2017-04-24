using Microsoft.Xna.Framework;

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

        //TODO: override virtual methods
    }
}