using System;
using HTTP.Enums;

namespace MvcFramework.Attributes.Http
{
    public class HttpPostAttribute : BaseHttpAttribute
    {
        public override HttpRequestMethod Method => HttpRequestMethod.POST;
    }
}
