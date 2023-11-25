using System.ComponentModel.DataAnnotations;
using static HouseRenting.Common.EntityValidationConstants.Category;

namespace HouseRenting.Data.Models
{
    public class Category
    {
        public Category()
        {
            Houses = new HashSet<House>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<House> Houses { get; set; }
    }
}
