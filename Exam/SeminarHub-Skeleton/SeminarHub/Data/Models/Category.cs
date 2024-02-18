using System.ComponentModel.DataAnnotations;
using static SeminarHub.Data.ValidationConstants.Category;

namespace SeminarHub.Data.Models
{
	public class Category
	{
        public Category()
        {
			Seminars = new HashSet<Seminar>();
        }

        [Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(NameMaxLength)]
		public string Name { get; set; } = null!;

		public virtual ICollection<Seminar> Seminars { get; set; } = null!;
	}
}
