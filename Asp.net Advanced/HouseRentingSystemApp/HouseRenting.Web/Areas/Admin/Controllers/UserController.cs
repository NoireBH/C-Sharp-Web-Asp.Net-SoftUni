using HouseRenting.Services.Data;
using HouseRenting.Services.Data.Interfaces;
using HouseRenting.Web.ViewModels.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace HouseRenting.Web.Areas.Admin.Controllers
{
	using static Common.GeneralConstants;
	public class UserController : BaseAdminController
	{
		private readonly IUserService userService;
		private readonly IMemoryCache cache;

		public UserController(IUserService userService, IMemoryCache cache)
		{
			this.userService = userService;
			this.cache = cache;
		}

		[Route("User/All")]
		public async Task<IActionResult> All()
		{
			IEnumerable<UserViewModel> users = cache.Get<IEnumerable<UserViewModel>>(UsersCacheKey);

			if (users == null)
			{
				users = await userService.GetAllUsersAsync();
				var cacheOptions = new MemoryCacheEntryOptions()
					.SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

				cache.Set(UsersCacheKey, users, cacheOptions);
			}

			return View(users);
		}
	}
}
