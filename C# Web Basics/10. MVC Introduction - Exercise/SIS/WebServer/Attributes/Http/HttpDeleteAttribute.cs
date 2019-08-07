using HTTP.Enums;

namespace MvcFramework.Attributes.Http
{
    public class HttpDeleteAttribute : BaseHttpAttribute
    {
        public override HttpRequestMethod Method => HttpRequestMethod.DELETE;
    }
}
