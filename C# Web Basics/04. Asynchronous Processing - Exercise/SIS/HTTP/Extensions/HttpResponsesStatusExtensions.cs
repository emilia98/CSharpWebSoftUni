using HTTP.Enums;

namespace HTTP.Extensions
{
    public static class HttpResponsesStatusExtensions
    {
        public static string GetStatusLine(HttpResponseStatusCode statusCode)
        {
            switch((int)statusCode)
            {
                case 200: return "OK";
                case 201: return "Created";
                case 302: return "Found";
                case 303: return "See Other";
                case 400: return "Bad Request";
                case 401: return "Unauthorized";
                case 403: return "Forbidden";
                case 404: return "Not Found";
                case 500: return "Internla Server Error";
            }

            return null;
        }
    }
}
