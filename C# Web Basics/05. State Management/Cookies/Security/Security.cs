using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Security
{
    class Security
    {
        static async Task Main()
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Loopback, 3000);

            tcpListener.Start();

            while (true)
            {
                TcpClient client = tcpListener.AcceptTcpClient();

                await Task.Run(() => ProcessClient(client));
            }
        }

        public static async Task ProcessClient(TcpClient client)
        {
            var NewLine = Environment.NewLine;

            using (NetworkStream stream = client.GetStream())
            {
                var requestBytes = new byte[100000];
                int readBytes = await stream.ReadAsync(requestBytes, 0, requestBytes.Length);
                var stringRequest = Encoding.UTF8.GetString(requestBytes, 0, readBytes);

                Console.WriteLine(new string('-', 15));
                Console.WriteLine(stringRequest);

                string responseBody = stringRequest;

                string response =
                    "HTTP/1.0 200 OK" + NewLine +
                    "Message: Hello!!!" + NewLine +
                    "Content-Type: text/plain" + NewLine +
                    // document.cookie
                    "Set-Cookie: cookie1=test; HttpOnly;" + NewLine +
                    "Set-Cookie: cookie2=test; HttpOnly; Secure;" + NewLine +
                    "Set-Cookie: cookie3=test; Secure;" + NewLine +
                     "Set-Cookie: cookie4=test;" + NewLine +
                    $"Content-Length: {responseBody.Length}" + NewLine + NewLine +
                    responseBody;
                byte[] responseBytes = Encoding.UTF8.GetBytes(response);

                await stream.WriteAsync(responseBytes, 0, responseBytes.Length);
            };
        }
    }
}
