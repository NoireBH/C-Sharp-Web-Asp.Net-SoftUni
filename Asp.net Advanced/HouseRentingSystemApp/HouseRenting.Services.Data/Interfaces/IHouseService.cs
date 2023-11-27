using HouseRenting.Web.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRenting.Services.Data.Interfaces
{
    public interface IHouseService
	{
		Task<IEnumerable<IndexViewModel>> GetLastThreeHouses();
	}
}
