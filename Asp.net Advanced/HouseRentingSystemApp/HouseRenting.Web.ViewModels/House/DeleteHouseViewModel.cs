using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRenting.Web.ViewModels.House
{
	public class DeleteHouseViewModel
	{
		public string Id { get; set; } = null!;

		public string Title { get; set; } = null!;

		public string Address { get; set; } = null!;

		public string ImageUrl { get; set; } = null!;


	}
}
