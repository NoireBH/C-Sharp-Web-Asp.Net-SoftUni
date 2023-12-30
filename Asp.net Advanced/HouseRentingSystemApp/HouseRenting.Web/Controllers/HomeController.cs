using HouseRenting.Services.Data.Interfaces;
using HouseRenting.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static HouseRenting.Common.GeneralConstants.AdminUser;

namespace HouseRenting.Web.Controllers
{
    public class HomeController : Controller
    {
		private readonly IHouseService houseService;

		public HomeController(IHouseService houseService)
		{
			this.houseService = houseService;
		}

		public async Task<IActionResult> Index()
        {
			if (User.IsInRole(AdminRoleName))
			{
				return RedirectToAction("Index", "Home", new {Area = AdminAreaName});
			}

			var lastThreeHouses = await houseService.GetLastThreeHousesAsync();

			return View(lastThreeHouses);
		}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error(int statusCode)
        {
			if (statusCode == 400)
			{
				return View("Error400");
			}

			if (statusCode == 400)
			{
				return View("Error400");
			}

			return View();
		}
    }
}