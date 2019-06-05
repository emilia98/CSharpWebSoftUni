using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Form_Post
{
    class FormPost
    {
        static void Main()
        {
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

                    string responseBody = "<form method=\"POST\"> <input type=\"text\" name=\"username\" /> <input type=\"password\" name=\"password\" /> <input type=\"submit\" /></form>";

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("HTTP/1.0 200 OK");
                    sb.AppendLine("Content-Type: text/html");
                    sb.AppendLine($"Content-Length: ${responseBody.Length}");
                    sb.AppendLine();
                    sb.AppendLine(responseBody);

                    string response = sb.ToString();
                    var responseBytes = Encoding.UTF8.GetBytes(response);

                    stream.Write(responseBytes, 0, responseBytes.Length);
                }
            }
        }
    }
}
