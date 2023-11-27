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
	public class AgentService : IAgentService
	{
		private readonly HouseRentingDbContext dbContext;

        public AgentService(HouseRentingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> ExistsByIdAsync(string userId)
		{
			return await dbContext.Agents.AnyAsync(a => a.UserId.ToString() == userId);
		}
	}
}
