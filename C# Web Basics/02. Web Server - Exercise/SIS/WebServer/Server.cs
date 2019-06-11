using System;
using System.Net;
using System.Net.Sockets;
using WebServer.Routing;

namespace WebServer
{
    public class Server
    {
        private const string LocalhostIpAddress = "127.0.0.1";

        private readonly int port;

        private readonly TcpListener listener;

        private readonly IServerRoutingTable serverRoutingTable;

        private bool isRunning;

        public Server(int port, IServerRoutingTable serverRoutingTable)
        {
            this.port = port;
            this.serverRoutingTable = serverRoutingTable;

            this.listener = new TcpListener(IPAddress.Parse(LocalhostIpAddress), port);
        }

        private void Listen(Socket client)
        {
            ConnectionHandler connectionHandler = new ConnectionHandler(client, this.serverRoutingTable);
            connectionHandler.ProcessRequest();
        }

        public void Run()
        {
            this.listener.Start();
            this.isRunning = true;

            Console.WriteLine($"Server started at http://{LocalhostIpAddress}:{this.port}");

            while(this.isRunning)
            {
                Console.WriteLine($"Waiting for client...");

                var client = this.listener.AcceptSocket();

                this.Listen(client);
            }
        }

        /*
        public async Task Listen(Socket client)
        {
            ConnectionHandler connectionHandler = new ConnectionHandler(client, serverRoutingTable);
            connectionHandler.ProcessRequest();
        }
        */
    }
}
