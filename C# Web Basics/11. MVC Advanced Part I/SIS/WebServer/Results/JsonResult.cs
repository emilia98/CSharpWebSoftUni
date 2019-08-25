using System.Text;
using HTTP.Enums;
using HTTP.Headers;

namespace MvcFramework.Results
{
    public class JsonResult : ActionResult
    {
        private const string contentType = "application/json";
        public JsonResult(string jsonContent, HttpResponseStatusCode httpResponseStatusCode = HttpResponseStatusCode.Ok) : base(httpResponseStatusCode)
        {
            this.AddHeader(new HttpHeader(HttpHeader.ContentType, contentType));
            this.Content = Encoding.UTF8.GetBytes(jsonContent);
        }
    }
}
