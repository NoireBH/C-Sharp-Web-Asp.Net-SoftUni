using HouseRenting.Web.ViewModels.House;

namespace HouseRenting.Web.Areas.Admin.ViewModels.House
{
	public class MyHousesViewModel
	{

		public IEnumerable<HouseAllViewModel> AddedHouses { get; set; } = null!;

		public IEnumerable<HouseAllViewModel> RentedHouses { get; set; } = null!;
	}
}
