using HTTP.Enums;

namespace MvcFramework.Attributes
{
    public class HttpDeleteAttribute : BaseHttpAttribute
    {
        public override HttpRequestMethod Method => HttpRequestMethod.DELETE;
    }
}
