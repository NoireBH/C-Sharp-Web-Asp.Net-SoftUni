using HouseRenting.Services.Data;
using HouseRenting.Services.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HouseRenting.Web.Areas.Admin.Controllers
{
	public class UserController : BaseAdminController
	{
		private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [Route("User/All")]
		public async Task<IActionResult> All()
		{
			var users = await userService.GetAllUsersAsync();

			return View(users);
		}
	}
}
