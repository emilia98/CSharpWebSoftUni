using System;
using HTTP.Enums;

namespace MvcFramework.Attributes.Http
{
    public class HttpPutMethod : BaseHttpAttribute
    {
        public override HttpRequestMethod Method => HttpRequestMethod.PUT;
    }
}
