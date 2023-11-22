using Library.ValidationConstants;
using System.ComponentModel.DataAnnotations;

namespace Library.Data.Entities
{
    public class Category
    {
        public Category()
        {
            Books = new HashSet<Book>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(EntityValidations.Category.CategoryNameMaxLength)]
        public string Name { get; set; } = null!;

        public ICollection<Book> Books { get; set; }
    }
}
