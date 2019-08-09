using HTTP.Requests;
using HTTP.Responses;
using MvcFramework;
using MvcFramework.Attributes.Action;
using MvcFramework.Results;
using System.Collections.Generic;

namespace IRunes.App.Controllers
{
    public class InfoController : Controller
    {
        public int MyProperty { get; set; }

        [NonAction]
        public override string ToString()
        {
            return base.ToString();
        }

        public ActionResult Json(IHttpRequest request)
        {
            return Json(new List<object>()
            {
                new
            {
                Name = "Pesho",
                Age = 25,
                Occupation = new[] { "Full-Stack Developer", "CEO" },
                Married = false
            },
                new
            {
                Name = "Pesho",
                Age = 25,
                Occupation = new[] { "Full-Stack Developer", "CEO" },
                Married = false
            },
                new
            {
                Name = "Pesho",
                Age = 25,
                Occupation = new[] { "Full-Stack Developer", "CEO" },
                Married = false
            }
            });
        }

        public ActionResult Xml(IHttpRequest request)
        {
            return Xml(new
            {
                Name = "Pesho",
                Age = 25.0,
                Married = false
            });
            return Xml(new List<object>()
            {
                new
            {
                Name = "Pesho",
                Age = 25.0,
                Occupation = new[] { "Full-Stack Developer", "CEO" },
                Married = false
            },
                new
            {
                Name = "Pesho",
                Age = 25.0,
                Occupation = new[] { "Full-Stack Developer", "CEO" },
                Married = false
            },
                new
            {
                Name = "Pesho",
                Age = 25.0,
                Occupation = new[] { "Full-Stack Developer", "CEO" },
                Married = false
            }
            });
        }

        public IHttpResponse About(IHttpRequest request)
        {
            return this.View();
        }
    }
}
