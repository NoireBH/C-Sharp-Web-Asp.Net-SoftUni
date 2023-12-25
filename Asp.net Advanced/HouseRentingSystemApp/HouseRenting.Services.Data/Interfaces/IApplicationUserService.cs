using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRenting.Services.Data.Interfaces
{
	public interface IApplicationUserService
	{
		Task<string?> GetUserFullName(string userId);
	}
}
