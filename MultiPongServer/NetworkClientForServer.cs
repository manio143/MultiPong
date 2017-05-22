using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using MultiPongCommon;

namespace MultiPongServer
{
    public class NetworkClientForServer : NetworkClientBase
    {
        private TcpListener listener;

        public void ListenAsync()
        {
            listener = new TcpListener(IPAddress.Any, 7575);
            listener.Start();
            listener.BeginAcceptTcpClient(AcceptClient, null);
        }

        private void AcceptClient(IAsyncResult ar)
        {
            var client = listener.EndAcceptTcpClient(ar);

            Debug.WriteLine("New client accepted.");

            StartReceiveAsyncInNewThread(client);

            listener.BeginAcceptTcpClient(AcceptClient, null);
        }
    }
}