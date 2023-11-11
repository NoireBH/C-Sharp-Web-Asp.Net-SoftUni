

namespace ForumApp.Data.Models
{

	using System.ComponentModel.DataAnnotations;
    using static ForumApp.Common.Validations.EntityValidations.Post;

	public class Post
    {
        public Post()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(ContentMaxLength)]
        public string Content { get; set; } = null!;
    }
}
