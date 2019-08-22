using System.ComponentModel.DataAnnotations;

namespace RazorViewsDemo.Models.Home
{
    public class HtmlHelpersViewModel
    {
        
        public string InputText { get; set; }

        public int InputNumber { get; set; }

        [Display(Name = "Pesho")]
        [DataType(DataType.MultilineText)]
        public string TextArea { get; set; }
    }
}
