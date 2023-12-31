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

        public async Task<IEnumerable<UserViewModel>> GetAllUsersAsync()
		{
			List<UserViewModel> allUsers = await this.context
				.Users
				.Select(u => new UserViewModel()
				{
					Id = u.Id.ToString(),
					Email = u.Email,
					FullName = u.FirstName + " " + u.LastName
				})
				.ToListAsync();
			foreach (UserViewModel user in allUsers)
			{
				var agent = this.context
					.Agents
					.FirstOrDefault(a => a.UserId.ToString() == user.Id);
				if (agent != null)
				{
					user.PhoneNumber = agent.PhoneNumber;
				}
				else
				{
					user.PhoneNumber = string.Empty;
				}
			}

			return allUsers;
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
