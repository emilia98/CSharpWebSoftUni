using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorViewsDemo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorViewsDemo.TagHelpers
{
    [HtmlTargetElement("h1", Attributes = "greeting-name")]
    [HtmlTargetElement("h2", Attributes = "greeting-name")]
    [HtmlTargetElement("h3", Attributes = "greeting-name")]
    [HtmlTargetElement("h4")]
    [HtmlTargetElement("h5")]
    [HtmlTargetElement("h6")]
    public class HelloTagHelper : TagHelper
    {
        private readonly IUsersService usersService;

        public HelloTagHelper(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public string GreetingName { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.SetAttribute("Pesho", this.GreetingName);
            output.Content.SetContent(this.GreetingName);
            output.PreElement.SetContent(this.GreetingName);
            output.PostContent.SetContent(this.GreetingName);
        }
    }
}
