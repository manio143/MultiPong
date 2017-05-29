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
        private ConcurrentQueue<Message> messageQueue = new ConcurrentQueue<Message>();

        public Message Receive()
        {
            Message m;
            messageQueue.TryDequeue(out m);
            return m;
        }

        public Message ReceiveBlocking()
        {
            Message m;
            while((m = Receive()) == null);
            return m;
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
                    var message = Message.FromBytes(bytes);
                    message.SenderStream = stream;
                    messageQueue.Enqueue(message);
                    Debug.WriteLine("Message received ({{{0}}}", message);
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