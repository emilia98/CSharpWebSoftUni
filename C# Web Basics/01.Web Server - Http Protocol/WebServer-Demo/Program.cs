using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace WebServer_Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            // IPAddress.Loopback -> localhost
            // This is the prefered way => only with port is depricated
            TcpListener tcpListener = new TcpListener(IPAddress.Loopback, 3000);

            tcpListener.Start();

            while (true)
            {
                TcpClient client = tcpListener.AcceptTcpClient();

                using (NetworkStream stream = client.GetStream())
                {
                    var requestBytes = new byte[100000];
                    int readBytes = stream.Read(requestBytes, 0, requestBytes.Length);
                    var stringRequest = Encoding.UTF8.GetString(requestBytes, 0, requestBytes.Length);

                    Console.WriteLine(new string('-', 15));
                    Console.WriteLine(stringRequest);

                    // Environment.NewLine => on each OS, the new line is different
                    string response = "HTTP/1.0 200 OK" + Environment.NewLine + "Message: Hello!!!" + Environment.NewLine;
                    byte[] responseBytes = Encoding.UTF8.GetBytes(response);

                    stream.Write(responseBytes, 0, responseBytes.Length);

                };
            }

        }
    }
}
