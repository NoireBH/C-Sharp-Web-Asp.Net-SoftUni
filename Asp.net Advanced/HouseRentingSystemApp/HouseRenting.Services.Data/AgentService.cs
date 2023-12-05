using HouseRenting.Data.Models;
using HouseRenting.Services.Data.Interfaces;
using HouseRenting.Web.Data;
using HouseRenting.Web.ViewModels.Agent;
using Microsoft.EntityFrameworkCore;

namespace HouseRenting.Services.Data
{
	public class AgentService : IAgentService
	{
		private readonly HouseRentingDbContext dbContext;

		public AgentService(HouseRentingDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task Create(string userId, BecomeAgentFormModel model)
		{
			var newAgent = new Agent() 
			{
				UserId = Guid.Parse(userId),
				PhoneNumber = model.PhoneNumber			
			};

			await dbContext.Agents.AddAsync(newAgent);
			await dbContext.SaveChangesAsync();
		}

		public async Task<bool> ExistsByIdAsync(string userId)
		{
			return await dbContext.Agents.AnyAsync(a => a.UserId.ToString() == userId);
		}

		public async Task<bool> UserHasRentsByIdAsync(string userId)
		{
			ApplicationUser? user = await dbContext.Users
				.FirstOrDefaultAsync(u => u.Id.ToString() == userId);

			if (user == null)
			{
				return false;
			}

			return user.RentedHouses.Any();
		}

		public async Task<bool> AgentWithPhoneNumberExistsAsync(string phoneNumber)
		{
			return await dbContext.Agents.AnyAsync(a => a.PhoneNumber == phoneNumber);
		}
	}
}
