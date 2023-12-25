using HouseRenting.Services.Data.Interfaces;
using HouseRenting.Web.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRenting.Services.Data
{
	public class ApplicationUserService : IApplicationUserService
	{
		private readonly HouseRentingDbContext context;

		public ApplicationUserService(HouseRentingDbContext context)
		{
			this.context = context;
		}

		public async Task<string?> GetUserFullName(string userId)
		{
			var user = await context.Users.FirstOrDefaultAsync(u => u.Id.ToString() == userId);

            if (user == null)
            {
				return null;
            }

            if (string.IsNullOrWhiteSpace(user.FirstName)
				|| string.IsNullOrWhiteSpace(user.LastName))
			{
				return null;
			}

			return user.FirstName + " " + user.LastName;
		}
	}
}
