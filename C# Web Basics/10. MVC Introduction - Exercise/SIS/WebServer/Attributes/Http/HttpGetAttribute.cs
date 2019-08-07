using HTTP.Enums;

namespace MvcFramework.Attributes.Http
{
    public class HttpGetAttribute : BaseHttpAttribute
    {
        public override HttpRequestMethod Method => HttpRequestMethod.GET;
    }
}
