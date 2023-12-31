using HouseRenting.Web.ViewModels.Rent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRenting.Services.Data.Interfaces
{
	public interface IRentService
	{
		Task<IEnumerable<RentViewModel>> GetAllRentsAsync();
	}
}
