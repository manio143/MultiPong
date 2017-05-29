using System;
using System.IO;
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

        private volatile bool canReceive;
        public bool CanReceive => canReceive;
        public Message Receive()
        {
            //queueSemaphore.WaitOne();
            //lock (messageQueue)
            if (messageQueue.Count > 0)
            {
                var m = messageQueue.Dequeue();
                if (messageQueue.Count == 0) canReceive = false;
                return m;
            }
            else return null;
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
            var client = (TcpClient)oclient;
            var stream = client.GetStream();
            while (client.Connected)
            {
                try
                {
                    var bytesToRead = stream.ReadByte();
                    if (bytesToRead < 0)
                        break;
                    var bytes = new byte[bytesToRead];
                    while (bytesToRead > 0)
                        bytesToRead -= stream.Read(bytes, bytes.Length - bytesToRead, bytesToRead);
                    //lock (messageQueue)
                    //{
                    var message = Message.FromBytes(bytes);
                    message.SenderStream = stream;
                    messageQueue.Enqueue(message);
                    canReceive = true;
                    Console.WriteLine("Message received ({{{0}}}", message);
                    //}
                    queueSemaphore.Release();
                }
                catch (IOException ioex)
                {
                    client.Close();
                    break;
                }
                catch (SocketException sex)
                {
                    client.Close();
                    break;
                }
            }
        }
    }
}