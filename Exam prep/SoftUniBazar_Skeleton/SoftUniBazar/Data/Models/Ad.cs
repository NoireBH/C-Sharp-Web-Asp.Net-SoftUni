using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftUniBazar.Data.Models
{
	public class Ad
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(ValidationConstants.Ad.NameMaxLength)]
		public string Name { get; set; } = null!;

		[Required]
		[MaxLength(ValidationConstants.Ad.DescriptionMaxLength)]
		public string Description { get; set; } = null!;

		[Required]
		public decimal Price { get; set; }

		[Required]
		public string OwnerId { get; set; } = null!;

		[Required]
		[ForeignKey(nameof(OwnerId))]
		public IdentityUser Owner { get; set; } = null!;

		[Required]
		public string ImageUrl { get; set; } = null!;

		[Required]
		public DateTime CreatedOn { get; set; }

		[Required]
		public int CategoryId { get; set; }

		[Required]
		[ForeignKey(nameof(CategoryId))]
		public Category Category { get; set; } = null!;
	}
}
