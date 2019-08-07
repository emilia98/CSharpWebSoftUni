using HTTP.Enums;
using HTTP.Responses;

namespace MvcFramework.Results
{
    public abstract class ActionResult : HttpResponse
    {
        protected ActionResult(HttpResponseStatusCode httpResponseStatusCode) : base(httpResponseStatusCode)
        {
        }
    }
}
