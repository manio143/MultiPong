using System;

namespace MultiPongCommon
{
    public abstract class Message
    {
        public MessageType MessageType { get; set; }

        public int PlayerId { get; set; } = 0;

        //TODO: Perform serialization
        //  public virtual byte[] GetBytes()
        //  public virtual Message FromBytes(byte[] bytes)
    }
}