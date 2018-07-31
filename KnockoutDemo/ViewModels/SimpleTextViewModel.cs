using System.ComponentModel.DataAnnotations;

namespace KnockoutDemo.ViewModels
{
    public class SimpleTextViewModel
    {
        [Required, Display(Name = "simple text")]
        public string SimpleText { get; set; }
    }
}