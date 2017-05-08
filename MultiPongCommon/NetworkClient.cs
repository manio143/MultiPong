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
    public class NetworkClient : INetworkClient
    {
        private bool singleClient;
        private List<TcpClient> clients = new List<TcpClient>();
        private TcpListener listener;

        private Semaphore queueSemaphore = new Semaphore(0, int.MaxValue);

        private Queue<Message> messageQueue = new Queue<Message>();

        private List<Thread> communicationThreads = new List<Thread>();

        public Message Receive()
        {
            queueSemaphore.WaitOne();
            lock (messageQueue)
                return messageQueue.Dequeue();
        }

        public void Send(Message message)
        {
            var bytes = message.GetBytes();
            if (singleClient)
            {
                var stream = clients.First().GetStream();
                stream.WriteByte((byte)bytes.Length);
                stream.Write(bytes, 0, bytes.Length);
            }
            else
            {
                message.SenderStream.WriteByte((byte)bytes.Length);
                message.SenderStream.Write(bytes, 0, bytes.Length);
            }
        }


        public void ListenAsync()
        {
            listener = new TcpListener(IPAddress.Any, 7575);
            listener.Start();
            listener.BeginAcceptTcpClient(AcceptClient, null);
        }

        private void AcceptClient(IAsyncResult ar)
        {
            var client = listener.EndAcceptTcpClient(ar);
            lock (clients)
                clients.Add(client);

            Debug.WriteLine("New client accepted.");

            var thread = new Thread(ReceiveAsync);
            thread.Start(client);

            lock (communicationThreads)
                communicationThreads.Add(thread);

            listener.BeginAcceptTcpClient(AcceptClient, null);
        }

        public void Connect(IPEndPoint endPoint)
        {
            singleClient = true;

            var client = new TcpClient();
            client.Connect(endPoint);

            clients.Add(client);

            var thread = new Thread(ReceiveAsync);
            thread.Start(client);

            lock (communicationThreads)
                communicationThreads.Add(thread);
        }

        private void ReceiveAsync(object oclient)
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