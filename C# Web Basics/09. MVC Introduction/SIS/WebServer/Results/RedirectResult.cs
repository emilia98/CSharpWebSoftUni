﻿using HTTP.Enums;
using HTTP.Headers;
using HTTP.Responses;

namespace MvcFramework.Results
{
    public class RedirectResult : HttpResponse
    {
        public RedirectResult(string location) : base(HttpResponseStatusCode.SeeOther)
        {
            this.Headers.AddHeader(new HttpHeader("Location", location));
        }
    }
}
