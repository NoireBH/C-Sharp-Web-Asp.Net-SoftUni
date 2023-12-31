using HouseRenting.Services.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HouseRenting.Web.Areas.Admin.Controllers
{
	public class RentController : BaseAdminController
	{
		private readonly IRentService rentService;

        public RentController(IRentService rentService)
        {
            this.rentService = rentService;
        }

        [Route("Rent/All")]
        public async Task<IActionResult> All()
        {
            var rents = await rentService.GetAllRentsAsync();

            return View(rents);
        }

    }
}
