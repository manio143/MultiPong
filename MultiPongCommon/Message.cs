using System;
using Microsoft.Xna.Framework;
using System.IO;

namespace MultiPongCommon
{
    public abstract class Message
    {
        public MessageType MessageType { get; set; }

        public int PlayerId { get; set; } = 0;

        public virtual byte[] GetBytes()
        {
            return new byte[] { 2, (byte)MessageType, (byte)PlayerId };
        }

        protected static Vector2 ReadVector2(BinaryReader br)
        {
            float x = br.ReadSingle();
            float y = br.ReadSingle();
            return new Vector2(x, y);
        }

        public static Message FromBytes(byte[] bytes)
        {
            if (bytes.Length <= 0) throw new Exception("FromBytes: invalid array");

            using (var ms = new MemoryStream(bytes))
            {
                using (var br = new BinaryReader(ms))
                {
                    br.ReadByte(); // count
                    MessageType ty = (MessageType)br.ReadByte();
                    int pl = br.ReadByte();
                    switch (ty)
                    {
                        case MessageType.Register:
                            return new RegisterMessage() { PlayerId = pl };

                        case MessageType.RegisterConfirmation:
                            return new RegisterConfirmation(pl);

                        case MessageType.GetState:
                            return new GetStateMessage() { PlayerId = pl };

                        case MessageType.State:
                            var ball = ReadVector2(br);
                            var p1 = ReadVector2(br);
                            var p2 = ReadVector2(br);
                            return new StateMessage(ball, p1, p2);

                        case MessageType.UpdatePad:
                            var p = ReadVector2(br);
                            return new UpdatePadMessage(p, pl);
                    }
                }
            }

            throw new Exception("FromBytes: invalid message type");
        }

        public Stream SenderStream { get; set; }
    }
}