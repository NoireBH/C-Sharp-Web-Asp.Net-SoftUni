using HouseRenting.Web.ViewModels.Agent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRenting.Services.Data.Interfaces
{
	public interface IAgentService
	{
		Task<bool> ExistsByIdAsync(string userId);

		Task<bool> AgentWithPhoneNumberExistsAsync(string phoneNumber);

		Task<bool> UserHasRentsByIdAsync(string userId);

		Task Create(string userId, BecomeAgentFormModel model);

		Task<string?> GetAgentIdByUserIdAsync(string userId);
		Task<bool> HasHouseById(string houseId, string currentUserId);
	}
}
