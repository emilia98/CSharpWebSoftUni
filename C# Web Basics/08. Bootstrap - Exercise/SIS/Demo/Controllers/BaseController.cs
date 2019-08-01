using HTTP.Cookies;
using HTTP.Enums;
using HTTP.Responses;
using System.IO;
using System.Runtime.CompilerServices;
using WebServer.Results;

namespace Demo.Controllers
{
    public abstract class BaseController
    {
        public IHttpResponse View([CallerMemberName] string view = null)
        {
            string controllerName = this.GetType().Name.Replace("Controller", string.Empty);
           
            string viewName = view;
            string viewContent = File.ReadAllText(string.Format("Views\\{0}\\{1}.html", controllerName, viewName));

            HtmlResult htmlResult = new HtmlResult(viewContent, HttpResponseStatusCode.Ok);

            return htmlResult;
        }
    }
}
