using HouseRenting.Web.ViewModels.House.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HouseRenting.Common.GeneralConstants;

namespace HouseRenting.Web.ViewModels.House
{
	public class AllHousesQueryModel
	{

        public AllHousesQueryModel()
        {
			CurrentPage = DefaultPage;
			HousesPerPage = DefaultHousesPerPage;
            Categories = new HashSet<string>();
			Houses = new HashSet<HouseAllViewModel>();
        }

        public string? Category { get; set; }

		[DisplayName("Search by text")]
		public string? SearchTerm { get; set; }

		[DisplayName("Sort houses by")]
		public HouseSort HouseSorting { get; set; }

		public int CurrentPage { get; set; }

		[DisplayName("Houses per page")]
		public int HousesPerPage { get; set; }

		public int TotalHousesCount { get; set; }

		public IEnumerable<string> Categories { get; set; }

		public IEnumerable<HouseAllViewModel> Houses { get; set; }

	}
}
