using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;

namespace MultiPongCommon
{
    public abstract class NetworkClientBase
    {
        private Semaphore queueSemaphore = new Semaphore(0, int.MaxValue);

        private Queue<Message> messageQueue = new Queue<Message>();

        public Message Receive()
        {
            queueSemaphore.WaitOne();
            lock (messageQueue)
                return messageQueue.Dequeue();
        }

        public virtual void Send(Message message)
        {
            var bytes = message.GetBytes();
            message.SenderStream.WriteByte((byte)bytes.Length);
            message.SenderStream.Write(bytes, 0, bytes.Length);
        }

        protected void StartReceiveAsyncInNewThread(TcpClient client)
        {
            var thread = new Thread(ReceiveAsync);
            thread.IsBackground = true;
            thread.Start(client);
        }

        protected void ReceiveAsync(object oclient)
        {
            var client = (TcpClient) oclient;
            var stream = client.GetStream();
            while (client.Connected)
            {
                var bytesToRead = stream.ReadByte();
                var bytes = new byte[bytesToRead];
                while (bytesToRead > 0)
                    bytesToRead -= stream.Read(bytes, bytes.Length - bytesToRead, bytesToRead);
                lock (messageQueue)
                {
                    var message = Message.FromBytes(bytes);
                    message.SenderStream = stream;
                    messageQueue.Enqueue(message);
                    Debug.WriteLine("Message received ({{{0}}}", message);
                }
                queueSemaphore.Release();
            }
        }
    }
}