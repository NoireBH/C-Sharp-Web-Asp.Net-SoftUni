using HouseRenting.Services.Data.Interfaces;
using HouseRenting.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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
			var lastThreeHouses = await houseService.GetLastThreeHousesAsync();

			return View(lastThreeHouses);
		}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}