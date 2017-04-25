﻿using System;
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
            return new byte[] { (byte)MessageType, (byte)PlayerId };
        }

        protected static Vector2 ReadVector2(BinaryReader binReader)
        {
            float x = binReader.ReadSingle();
            float y = binReader.ReadSingle();
            return new Vector2(x, y);
        }

        public static Message FromBytes(byte[] bytes)
        {
            if (bytes.Length <= 0) throw new Exception("FromBytes: invalid array");

            using (var memStream = new MemoryStream(bytes))
            {
                using (var binReader = new BinaryReader(memStream))
                {
                    MessageType messageType = (MessageType)binReader.ReadByte();
                    int playerId = binReader.ReadByte();
                    switch (messageType)
                    {
                        case MessageType.Register:
                            return new RegisterMessage() { PlayerId = playerId };

                        case MessageType.RegisterConfirmation:
                            return new RegisterConfirmation(playerId);

                        case MessageType.GetState:
                            return new GetStateMessage() { PlayerId = playerId };

                        case MessageType.State:
                            var ballPosition = ReadVector2(binReader);
                            var player1Position = ReadVector2(binReader);
                            var player2Position = ReadVector2(binReader);
                            return new StateMessage(ballPosition, player1Position, player2Position);

                        case MessageType.UpdatePad:
                            var padPosition = ReadVector2(binReader);
                            return new UpdatePadMessage(padPosition, playerId);
                    }
                }
            }

            throw new Exception("FromBytes: invalid message type");
        }

        public Stream SenderStream { get; set; }
    }
}