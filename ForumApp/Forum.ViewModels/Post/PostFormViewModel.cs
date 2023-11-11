namespace ForumApp.ViewModels.Post
{
	using System.ComponentModel.DataAnnotations;

	using static ForumApp.Common.Validations.EntityValidations.Post;

	public class PostFormViewModel
	{
		[Required]
		[StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
		public string Title { get; set; } = null!;

		[Required]
		[StringLength(ContentMaxLength, MinimumLength = ContentMinLength)]
		public string Content { get; set; } = null!;
	}
}