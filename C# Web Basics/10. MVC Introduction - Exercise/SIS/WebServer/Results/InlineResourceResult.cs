using HTTP.Enums;
using HTTP.Headers;

namespace MvcFramework.Results
{
    // returns resources like images, css, js
    public class InlineResourceResult : ActionResult
    {
        public InlineResourceResult(byte[] content, HttpResponseStatusCode responseStatusCode) : base(responseStatusCode)
        {
            this.Headers.AddHeader(new HttpHeader(HttpHeader.ContentLength, content.Length.ToString()));
            this.Headers.AddHeader(new HttpHeader(HttpHeader.ContentDisposition, "inline"));
            this.Content = content;
        }
    }
}
