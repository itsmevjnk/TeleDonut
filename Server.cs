/*
 * Simple Telnet server implementation for C#
 * Based on robertripoll's TelnetServer: https://github.com/robertripoll/TelnetServer
 */

using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TeleDonut
{
    class Server
    {
        private IPAddress listenIP;
        private int listenPort;

        private Socket serverSocket;
        public List<Socket> clients = new List<Socket>();

        public Server(IPAddress ip, int port = 23)
        {
            listenIP = ip; listenPort = port;
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Start()
        {
            serverSocket.Bind(new IPEndPoint(listenIP, listenPort));
            serverSocket.Listen(0);
            serverSocket.BeginAccept(new AsyncCallback(HandleIncomingConnection), serverSocket);

            Console.WriteLine($"Listening on {listenIP} port {listenPort}");
        }

        public void Stop()
        {
            serverSocket.Close();
        }

        private void HandleIncomingConnection(IAsyncResult result)
        {
            try
            {
                Socket socket = ((Socket) result.AsyncState).EndAccept(result);
                clients.Add(socket);

                Console.WriteLine($"New client connected, count: {clients.Count}");

                serverSocket.BeginAccept(new AsyncCallback(HandleIncomingConnection), serverSocket); // resume accepting
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void Broadcast(string message)
        {
            var msgBytes = Encoding.ASCII.GetBytes(message);

            var clientsCopy = new List<Socket>(clients); // so we can remove sockets from clients as we go
            foreach (Socket s in clientsCopy)
            {
                try
                {
                    s.BeginSend(msgBytes, 0, msgBytes.Length, SocketFlags.None, new AsyncCallback(HandleDataSent), s);
                }
                catch
                {
                    clients.Remove(s);
                    Console.WriteLine($"Connection broken, remaining client count: {clients.Count}");
                }
            }
        }

        private void HandleDataSent(IAsyncResult result)
        {
            Socket socket = (Socket) result.AsyncState;
            try
            {
                socket.EndSend(result);
            }
            catch
            {
                // Console.WriteLine(e);
                clients.Remove(socket);
                Console.WriteLine($"Connection broken, remaining client count: {clients.Count}");
            }
        }
    }
}