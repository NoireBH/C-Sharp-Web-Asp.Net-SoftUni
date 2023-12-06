using HouseRenting.Web.ViewModels.House;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRenting.Services.Data.Models.House
{
	public class AllHousesFilteredServiceModel
	{
        public AllHousesFilteredServiceModel()
        {
            Houses = new HashSet<HouseAllViewModel>();
        }

        public int TotalHousesCount { get; set; }

		public IEnumerable<HouseAllViewModel> Houses { get; set; }
	}
}
