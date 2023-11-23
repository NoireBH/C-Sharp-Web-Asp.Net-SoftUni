using Library.ValidationConstants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Data.Entities
{
    public class Book
    {
        public Book()
        {
            UsersBooks = new HashSet<IdentityUserBook>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(EntityValidations.Book.TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
		[StringLength(EntityValidations.Book.AuthorMaxLength)]
		public string Author { get; set; } = null!;

        [Required]
		[StringLength(EntityValidations.Book.DescriptionMaxLength)]
		public string Description { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Required]
        
        public decimal Rating { get; set; }

        [Required]
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        [Required]
        public Category Category { get; set; } = null!;

        public ICollection<IdentityUserBook> UsersBooks { get; set; }
    }
}
