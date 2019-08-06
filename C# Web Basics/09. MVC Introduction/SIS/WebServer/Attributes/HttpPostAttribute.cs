using System;
using HTTP.Enums;

namespace MvcFramework.Attributes
{
    public class HttpPostAttribute : BaseHttpAttribute
    {
        public override HttpRequestMethod Method => HttpRequestMethod.POST;
    }
}
