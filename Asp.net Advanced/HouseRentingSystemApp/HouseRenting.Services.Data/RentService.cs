using HouseRenting.Services.Data.Interfaces;
using HouseRenting.Services.Mapping;
using HouseRenting.Web.Data;
using HouseRenting.Web.ViewModels.Rent;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRenting.Services.Data
{
	public class RentService : IRentService
	{
		private readonly HouseRentingDbContext context;

		public RentService(HouseRentingDbContext context)
		{
			this.context = context;
		}

		public async Task<IEnumerable<RentViewModel>> GetAllRentsAsync()
		{
			var rents = await context.Houses
				.Include(h => h.Agent.User)
				.Include(h => h.Renter)
				.Where(h => h.RenterId != null)
				.To<RentViewModel>()
				.ToListAsync();

			return rents;
		}
	}

}
