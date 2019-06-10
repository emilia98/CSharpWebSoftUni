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

            bool result = HttpRequestMethod.TryParse(requestLine[0], true, out method);

            if(!result)
            {
                throw new BadRequestException(String.Format(GlobalConstants.UnsupportedHttpMethodExceptionMessage, requestLine[0]));
            }

            this.RequestMethod = method;
        }

        private void ParseRequestUrl(string[] requestLine)
        {
            this.Url = requestLine[1];
        }

        private void ParseRequestPath()
        {
            this.Path = this.Url.Split('?')[0];
        }

        private void ParseRequestHeaders(string[] requestContent)
        {
            foreach (var headerLine in requestContent)
            {
                if(string.IsNullOrEmpty(headerLine) || headerLine == GlobalConstants.HttpNewLine)
                {
                    break;
                }

                string[] pair = headerLine.Split(new[] { ':', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if(pair.Length < 2)
                {
                    throw new BadRequestException("This is not a valid line of header!");
                }

                string key = pair[0];
                string value = pair[1];

                this.Headers.AddHeader(new HttpHeader(key, value));
            }
        }

        private void ParseCookies()
        {

        }

        private void ParseRequestQueryParameters()
        {
            string splitUrl = this.Url.Split('?')[1];

            if(splitUrl.Length == 0)
            {
                return;
            }
            // remove fragment from query params
            string queryParams = splitUrl.Split('#')[0];

            string[] pairs = queryParams.Split('&');

            foreach (var pair in pairs)
            {
                string[] queryPair = pair.Split('=');
                string key = queryPair[0];
                string value = queryPair[1];

                CoreValidator.ThrowIfNullOrEmptyInQueryParams(key, nameof(key));
                CoreValidator.ThrowIfNullOrEmptyInQueryParams(value, nameof(value));

                this.QueryData.Add(key, value);
            }
        }

        private void ParseRequestFormDataParameters(string formData)
        {
            // Parse multiple parameters
            if(formData == GlobalConstants.HttpNewLine)
            {
                return;
            }

            string[] pairs = formData.Split('&');

            foreach (var pair in pairs)
            {
                string[] queryPair = pair.Split('=');
                string key = queryPair[0];
                string value = queryPair[1];

                CoreValidator.ThrowIfNullOrEmptyInRequestBody(key, nameof(key));
                CoreValidator.ThrowIfNullOrEmptyInRequestBody(value, nameof(value));

                this.FormData.Add(key, value);
            }

        }

        private void ParseRequestParameters(string formData)
        {
            this.ParseRequestQueryParameters();
            this.ParseRequestFormDataParameters(formData);
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

            this.ParseRequestHeaders(headersArray);
            this.ParseCookies();

            this.ParseRequestParameters(splitRequestContent[splitRequestContent.Length - 1]);
        }

    }
}
