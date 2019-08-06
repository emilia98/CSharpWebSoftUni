using HTTP.Enums;

namespace MvcFramework.Attributes
{
    public class HttpGetAttribute : BaseHttpAttribute
    {
        public override HttpRequestMethod Method => HttpRequestMethod.GET;
    }
}
