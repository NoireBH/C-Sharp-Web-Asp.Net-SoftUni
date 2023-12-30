using HouseRenting.Web.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRenting.Services.Data.Interfaces
{
	public interface IUserService
	{
		Task<string> GetUserFullNameByIdAsync(string userId);
		Task<IEnumerable<UserViewModel>> GetAllUsersAsync();
	}
}
