using HouseRenting.Services.Data;
using HouseRenting.Services.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouseRenting.Web.Controllers
{
	[Authorize]
	public class HouseController : Controller
	{
		private readonly IHouseService houseService;

        public HouseController(IHouseService houseService)
        {
            this.houseService = houseService;
        }

        [AllowAnonymous]
		public async Task<IActionResult> All()
		{
			return View();
		}
	}
}
