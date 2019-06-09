using System;
using System.Collections.Generic;
using System.Linq;
using HTTP.Common;
using HTTP.Enums;
using HTTP.Exceptions;
using HTTP.Headers;

namespace HTTP.Requests
{
    public class HttpRequest : IHttpRequest
    {
        public HttpRequest(string requestString)
        {
            CoreValidator.ThrowIfNullOrEmpty(requestString, nameof(requestString));

            this.FormData = new Dictionary<string, object>();
            this.QueryData = new Dictionary<string, object>();
            this.Headers = new HttpHeaderCollection();

            this.ParseRequest(requestString);
        }

        public string Path { get; private set; }

        public string Url { get; private set; }

        // Body from POST request
        public Dictionary<string, object> FormData { get; }

        // Query params from URL
        public Dictionary<string, object> QueryData { get; }

        public IHttpHeaderCollection Headers { get; }

        public HttpRequestMethod RequestMethod { get; private set; }


        private bool isValidRequestLine(string[] requestLines)
        {
            return requestLines.Length == 3 && requestLines[2] == GlobalConstants.HttpOneProtocolFragment;
        }

        private bool IsValidRequestQueryString(string queryString, string[] queryParameters)
        {
            return !string.IsNullOrEmpty(queryString) && queryParameters.Length >= 1;
        }

        private void ParseRequestMethod(string[] requestLine)
        {
            HttpRequestMethod method; 

            bool result = HttpRequestMethod.TryParse(requestLine[0], out method);

            if(!result)
            {
                throw new BadRequestException();
            }

            this.RequestMethod = method;
        }

        private void ParseRequestUrl(string[] requestLine)
        {
            this.Url = requestLines[1];
        }

        private void ParseRequestPath()
        {

        }

        private void ParseHeaders(string[] requestContent)
        {

        }

        private void ParseCookies()
        {

        }

        private void ParseQueryParameters()
        {

        }

        private void ParseFormDataParameters(string formData)
        {

        }

        private void ParseRequestParameters(string formData)
        {
            this.ParseQueryParameters();
            this.ParseFormDataParameters(formData);
        }

        private void ParseRequest(string requestString)
        {
            string[] splitRequestContent = requestString.Split(
                new[] { GlobalConstants.HttpNewLine }, 
                StringSplitOptions.None);

            string[] requestLine = splitRequestContent[0]
                                    .Trim()
                                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            string[] headersArray = splitRequestContent.Skip(1).ToArray();

            if (!this.isValidRequestLine(requestLine))
            {
                throw new BadRequestException();
            }

            this.ParseRequestMethod(requestLine);
            this.ParseRequestUrl(requestLine);
            this.ParseRequestPath();

            this.ParseHeaders(headersArray);
            this.ParseCookies();

            this.ParseRequestParameters(splitRequestContent[splitRequestContent.Length - 1]);
        }

    }
}
