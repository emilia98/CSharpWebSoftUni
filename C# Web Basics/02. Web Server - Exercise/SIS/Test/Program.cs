using HTTP.Requests;
using System;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string request = "POST /url/asd?name=pesho&id=1#fragment HTTP/1.1" +
                "\r\n"
                + "Authorization: Basic 1234567890"
                + "\r\n"
                + "Date: " + DateTime.Now +
                "\r\n" +
                "Host: localhost:5000"
                + "\r\n"
                + "\r\n" +
                "username=pesho&password=1234";

            HttpRequest httpRequest = new HttpRequest(request);

            var a = 5;

        }
    }
}
