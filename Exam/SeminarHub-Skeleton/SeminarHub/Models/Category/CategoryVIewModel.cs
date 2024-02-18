using System.ComponentModel.DataAnnotations;

namespace SeminarHub.Models.Category
{
    public class CategoryVIewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;
    }
}
