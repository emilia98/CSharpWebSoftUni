using System.Text;
using HTTP.Enums;
using HTTP.Headers;

namespace MvcFramework.Results
{
    public class XmlResult : ActionResult
    {
        private const string contentType = "application/xml";

        public XmlResult(string xmlContent, HttpResponseStatusCode httpResponseStatusCode = HttpResponseStatusCode.Ok) : base(httpResponseStatusCode)
        {
            this.AddHeader(new HttpHeader(HttpHeader.ContentType, contentType));
            this.Content = Encoding.UTF8.GetBytes(xmlContent);
        }
    }
}
