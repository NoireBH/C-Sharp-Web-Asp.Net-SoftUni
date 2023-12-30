using HouseRenting.Services.Data.Interfaces;
using HouseRenting.Services.Mapping;
using HouseRenting.Web.Data;
using HouseRenting.Web.ViewModels.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRenting.Services.Data
{
	public class UserService : IUserService
	{
		private readonly HouseRentingDbContext context;

        public UserService(HouseRentingDbContext context)
        {
            this.context = context;
        }

        public Task<IEnumerable<UserViewModel>> GetAllUsersAsync()
		{
			var allUsers = new List<UserViewModel>();

			var agents = context.Agents
				.Include(a => a.User)
				.To<UserViewModel>()
				.ToArrayAsync();
		}

		public async Task<string> GetUserFullNameByIdAsync(string userId)
		{
			var user = await context.Users.FirstOrDefaultAsync(u => u.Id.ToString() ==  userId);

			if (user == null)
			{
				return String.Empty;
			}

			return $"{user.FirstName} {user.LastName}";
		}
	}
}
