using HTTP.Enums;
using HTTP.Headers;

namespace MvcFramework.Results
{
    public class FileResult : ActionResult
    {
        private const string contentDisposition = "attachment";

        public FileResult(byte[] fileContent, HttpResponseStatusCode httpResponseStatusCode = HttpResponseStatusCode.Ok) : base(httpResponseStatusCode)
        {
            this.Headers.AddHeader(new HttpHeader(HttpHeader.ContentLength, fileContent.Length.ToString()));
            this.Headers.AddHeader(new HttpHeader(HttpHeader.ContentDisposition, contentDisposition));
            this.Content = fileContent;
        }
    }
}
