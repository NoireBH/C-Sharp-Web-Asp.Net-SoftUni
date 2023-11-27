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
	}
}
