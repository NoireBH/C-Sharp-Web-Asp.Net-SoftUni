using HouseRenting.Web.ViewModels.Category;
using System.ComponentModel.DataAnnotations;
using static HouseRenting.Common.EntityValidationConstants.House;

namespace HouseRenting.Web.ViewModels.House
{
	public class AddHouseFormModel
	{
        public AddHouseFormModel()
        {
			Categories = new HashSet<HouseCategoryFormModel>();
        }

        [Required]
		[StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
		public string Title { get; set; } = null!;

		[Required]
		[StringLength(AddressMaxLength, MinimumLength = AddressMinLength)]
		public string Address { get; set; } = null!;

		[Required]
		[StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
		public string Description { get; set; } = null!;

		[Required]
		[Display(Name = "Image Url")]
		[MaxLength(ImageUrlMaxLength)]
		public string ImageUrl { get; set; } = null!;

		[Required]
		[Display(Name = "Price Per Month")]
		[Range(typeof(decimal), PricePerMonthMinValue, PricePerMonthMaxValue)]
		public string PricePerMonth { get; set; } = null!;

		[Required]
		[Display(Name = "Category")]
		public string CategoryId { get; set; } = null!;

		public IEnumerable<HouseCategoryFormModel> Categories { get; set; }

	}
}
