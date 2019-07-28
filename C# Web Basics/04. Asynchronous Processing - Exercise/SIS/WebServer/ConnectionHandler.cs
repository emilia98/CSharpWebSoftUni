using HTTP.Common;
using HTTP.Enums;
using HTTP.Exceptions;
using HTTP.Requests;
using HTTP.Responses;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using WebServer.Results;
using WebServer.Routing;

namespace WebServer
{
    public class ConnectionHandler
    {
        private readonly Socket client;

        private readonly IServerRoutingTable serverRoutingTable;

        public ConnectionHandler(Socket client, IServerRoutingTable serverRoutingTable)
        {
            CoreValidator.ThrowIfNull(client, nameof(client));
            CoreValidator.ThrowIfNull(serverRoutingTable, nameof(serverRoutingTable));

            this.client = client;
            this.serverRoutingTable = serverRoutingTable;
        }

        public async Task ProcessRequestAsync()
        {
            try
            {
                var httpRequest = await this.ReadRequestAsync();

                if(httpRequest != null)
                {
                    Console.WriteLine($"Processing: {httpRequest.RequestMethod} {httpRequest.Path}...");

                    var httpResponse = this.HandleRequest(httpRequest);

                    await this.PrepareResponseAsync(httpResponse);
                }

            }
            catch(BadRequestException e)
            {
                await this.PrepareResponseAsync(new TextResult(e.ToString(), HttpResponseStatusCode.BadRequest));
            }
            catch(Exception e)
            {
                await this.PrepareResponseAsync(new TextResult(e.ToString(), HttpResponseStatusCode.InternalServerError));
            }

            this.client.Shutdown(SocketShutdown.Both);
        }

        private async Task<IHttpRequest> ReadRequestAsync()
        {
            var result = new StringBuilder();
            var data = new ArraySegment<byte>(new byte[1024]);

            while (true)
            {
                int numberOfBytesRead = await this.client.ReceiveAsync(data.Array, SocketFlags.None);

                if (numberOfBytesRead == 0)
                {
                    break;
                }

                var bytesAsString = Encoding.UTF8.GetString(data.Array, 0, numberOfBytesRead);
                result.Append(bytesAsString);

                if (numberOfBytesRead < 1023)
                {
                    break;
                }
            }


            if (result.Length == 0)
            {
                return null;
            }

            return new HttpRequest(result.ToString());
        }

        private IHttpResponse HandleRequest(IHttpRequest httpRequest)
        {
            bool? isContained = this.serverRoutingTable.Contains(httpRequest.RequestMethod, httpRequest.Path);
            if (isContained == null || isContained.Value == false)
            {
                return new TextResult($"Route with method {httpRequest.RequestMethod} and path \"{httpRequest.Path}\" not found.", HttpResponseStatusCode.NotFound);
            }

            var func = this.serverRoutingTable.Get(httpRequest.RequestMethod, httpRequest.Path);

            if(func == null)
            {
                return null;
            }

            return func.Invoke(httpRequest);
        }

        private async Task PrepareResponseAsync(IHttpResponse httpResponse)
        {
            byte[] byteSegments = httpResponse.GetBytes();

            await this.client.SendAsync(byteSegments, SocketFlags.None);
        }
    }
}
