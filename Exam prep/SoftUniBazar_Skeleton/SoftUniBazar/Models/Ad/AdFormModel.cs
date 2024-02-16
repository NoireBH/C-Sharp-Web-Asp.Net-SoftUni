using Microsoft.AspNetCore.Identity;
using SoftUniBazar.Data.Models;
using SoftUniBazar.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static SoftUniBazar.Data.ValidationConstants.Ad;
using SoftUniBazar.Models.Category;

namespace SoftUniBazar.Models.Ad
{
	public class AdFormModel
	{
        public AdFormModel()
        {
            Categories = new HashSet<CategoryViewModel>();
        }

        public int Id { get; set; }

		[Required]
		[StringLength(NameMaxLength, MinimumLength = NameMinLength)]
		public string Name { get; set; } = null!;

		[Required]
		[StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
		public string Description { get; set; } = null!;

		[Required]
		public decimal Price { get; set; }


		[Required]
		public string ImageUrl { get; set; } = null!;


		[Required]
		public int CategoryId { get; set; }

		public IEnumerable<CategoryViewModel> Categories { get; set; } = null!;
	}
}
