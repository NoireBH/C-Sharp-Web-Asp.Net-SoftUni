using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRenting.Web.ViewModels.House.Enums
{
	public class HouseAllViewModel
	{
		public int Id { get; set; }

		public string Title { get; set; } = null!;

		public string Address { get; set; } = null!;

		[DisplayName("Image Link")]
		public string ImageUrl { get; set; } = null!;

		[DisplayName("Price Per Month")]
		public decimal PricePerMonth { get; set; }

		[DisplayName("Is Rented")]
		public bool IsRented { get; set; }
	}
}
