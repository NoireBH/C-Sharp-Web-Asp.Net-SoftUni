using System.ComponentModel.DataAnnotations;

namespace SoftUniBazar.Data.Models
{
	public class Category
	{
        public Category()
        {
            Ads = new HashSet<Ad>();
        }

        [Key]
		public int Id { get; set; }

		[Required]
		public string Name { get; set; } = null!;

		public virtual ICollection<Ad> Ads { get; set; } = null!;
	}
}
