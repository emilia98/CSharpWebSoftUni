using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SimpleWebServer
{
    class SimpleWebServer
    {
        static void Main()
        {
            var NewLine = Environment.NewLine;
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
                    // ERROR: Content Length Mismatch
                    // string response = "HTTP/1.0 200 OK" + NewLine + "Message: Hello!!!" + NewLine + "Content-Length: 30" + NewLine + NewLine + "<h1> Hello, user</h1>" + NewLine;
                    string responseBody = "<h1> Hello, user</h1>";

                    // {responseBody.Length} => because is simple string with Latin Letters
                    string response =
                        "HTTP/1.0 200 OK" + NewLine +
                        "Message: Hello!!!" + NewLine +
                        "Content-Type: text/plain" + NewLine +
                        // "Content-Disposition: attachment; filename=\"index.html\"" + NewLine + 
                        $"Content-Length: {responseBody.Length}" + NewLine + NewLine +
                        responseBody;
                    byte[] responseBytes = Encoding.UTF8.GetBytes(response);

                    stream.Write(responseBytes, 0, responseBytes.Length);
                };
            }

        }
    }
}

// Default Content-Type : text/html