using HouseRenting.Services.Data;
using HouseRenting.Services.Data.Interfaces;
using HouseRenting.Web.Infrastructure.Extensions;
using HouseRenting.Web.ViewModels.House;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static HouseRenting.Common.NotificationMessagesConstants;

namespace HouseRenting.Web.Controllers
{
	[Authorize]
	public class HouseController : Controller
	{
		private readonly IHouseService houseService;
		private readonly IAgentService agentService;

        public HouseController(IHouseService houseService, IAgentService agentService)
        {
            this.houseService = houseService;
			this.agentService = agentService;
        }

        [AllowAnonymous]
		public async Task<IActionResult> All()
		{
			return View();
		}

		public async Task<IActionResult> Add()
		{
			bool agentExists = await agentService.ExistsByIdAsync(User.GetId()!);

			if (!agentExists)
			{
				TempData[ErrorMessage] = "You have to be an agent in order to add a new house!";
				return RedirectToAction(nameof(AgentController.Become), "Agent");
			}

			return View(new AddHouseFormModel
			{
				Categories = await houseService.GetAllHouseCategoriesAsync()
			});
		}
	}
}
