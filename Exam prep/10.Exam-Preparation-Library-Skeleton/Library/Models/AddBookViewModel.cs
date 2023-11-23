using Library.ValidationConstants;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
	public class AddBookViewModel
	{
		public AddBookViewModel()
		{
			Categories = new HashSet<CategoryViewModel>();
		}

		[Required]
		[StringLength(EntityValidations.Book.TitleMaxLength, MinimumLength = EntityValidations.Book.TitleMinLength)]
		public string Title { get; set; } = null!;

		[Required]
		[StringLength(EntityValidations.Book.AuthorMaxLength, MinimumLength = EntityValidations.Book.AuthorMinLength)]
		public string Author { get; set; } = null!;

		[Required]
		[Range(EntityValidations.Book.RatingMinLength, EntityValidations.Book.RatingMaxLength)]
		public decimal Rating { get; set; }

		[Required]
		[StringLength(EntityValidations.Book.DescriptionMaxLength, MinimumLength = EntityValidations.Book.DescriptionMinLength)]
		public string Description { get; set; } = null!;

		[Required(AllowEmptyStrings = false)]
		public string Url { get; set; } = null!;

		//required doesnt work so we use Range so that -20 for example isnt valid
		[Range(1, int.MaxValue)]
		public int CategoryId { get; set; }

		public IEnumerable<CategoryViewModel> Categories { get; set; } = null!;
	}
}
