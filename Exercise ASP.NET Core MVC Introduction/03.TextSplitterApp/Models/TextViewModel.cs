using _03.TextSplitterApp.Common;
using System.ComponentModel.DataAnnotations;

namespace _03.TextSplitterApp.Models
{
    public class TextViewModel
    {
        [Required]
        [StringLength(ValidationConstants.textMaxLength, MinimumLength =ValidationConstants.textMinLength)]
        public string Text { get; set; } = null!;

        public string SplitText { get; set; } = null!;

    }
}
