using HTTP.Enums;
using HTTP.Headers;
using HTTP.Requests;
using HTTP.Responses;
using System;
using System.Globalization;
using System.Text;
using WebServer;
using WebServer.Results;
using WebServer.Routing;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string request = "POST /url/asd?name=pesho&id=1&id=7#fragment HTTP/1.1" +
                "\r\n"
                + "Authorization: Basic 1234567890"
                + "\r\n"
                + "Date: " + DateTime.Now +
                "\r\n" +
                "Cookie: cookie1=5; HttpOnly;" 
                +
                "\r\n" +
                "Host: localhost:5000"
                + "\r\n"
                + "\r\n" +
                "username=pesho&password=1234&username=PESHO";

            HttpRequest httpRequest = new HttpRequest(request);

            var a = 5;
            

            /*
            HttpResponseStatusCode statusCode = HttpResponseStatusCode.NotFound;

            HttpResponse response = new HttpResponse(statusCode);
            response.AddHeader(new HttpHeader("Host", "localhost:5000"));
            response.AddHeader(new HttpHeader("Date", DateTime.Now.ToString(CultureInfo.InvariantCulture)));

            response.Content = Encoding.UTF8.GetBytes("<h1>Hello World </h1>");

            Console.WriteLine(Encoding.UTF8.GetString(response.GetBytes()));
            */

            /*
            IServerRoutingTable serverRoutingTable = new ServerRoutingTable();


            serverRoutingTable.Add(HttpRequestMethod.GET, "/", httpRequest => new HtmlResult("<h1> Hello World!</h1>", HttpResponseStatusCode.Ok));
            Server server = new Server(8000, serverRoutingTable);
            server.Run();
            */
        }
    }
}
