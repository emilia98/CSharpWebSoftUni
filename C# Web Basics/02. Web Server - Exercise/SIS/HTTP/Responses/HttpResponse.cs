using System;
using System.Collections.Generic;
using System.Text;
using HTTP.Common;
using HTTP.Enums;
using HTTP.Extensions;
using HTTP.Headers;

namespace HTTP.Responses
{
    public class HttpResponse : IHttpResponse
    {
        public HttpResponse()
        {
            this.Headers = new HttpHeaderCollection();
            this.Content = new byte[0];
        }

        public HttpResponse(HttpResponseStatusCode statusCode) : this()
        {
            CoreValidator.ThrowIfNull(statusCode, nameof(statusCode));
            this.StatusCode = statusCode;
        }

        public HttpResponseStatusCode StatusCode { get; set; }

        public IHttpHeaderCollection Headers { get; }

        public byte[] Content { get; set; }

        public void AddHeader(HttpHeader header)
        {
            CoreValidator.ThrowIfNull(header, nameof(header));
            this.Headers.AddHeader(header);
        }

        // Here is the body of the response
        public byte[] GetBytes()
        {
            string toString = this.ToString();

             
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result
                .Append($"{GlobalConstants.HttpOneProtocolFragment} {(int)this.StatusCode} {HttpResponsesStatusExtensions.GetStatusLine(this.StatusCode)}")
                .Append(GlobalConstants.HttpNewLine)
                .Append(this.Headers)
                .Append(GlobalConstants.HttpNewLine);

            result.Append(GlobalConstants.HttpNewLine);

            return result.ToString();
        }
    }
}
