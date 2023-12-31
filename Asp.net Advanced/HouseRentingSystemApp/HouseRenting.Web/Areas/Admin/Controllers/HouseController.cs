using HouseRenting.Services.Data;
using HouseRenting.Services.Data.Interfaces;
using HouseRenting.Web.Areas.Admin.ViewModels.House;
using HouseRenting.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace HouseRenting.Web.Areas.Admin.Controllers
{
	public class HouseController : BaseAdminController
	{
		private readonly IAgentService agentService;
		private readonly IHouseService houseService;

		public HouseController(
			IAgentService agentService,
			IHouseService houseService
			)
		{
			this.agentService = agentService;
			this.houseService = houseService;
		}

		public async Task<IActionResult> Mine()
		{
			string? agentId = await agentService.GetAgentIdByUserIdAsync(User.GetId()!);

			MyHousesViewModel model = new MyHousesViewModel()
			{
				AddedHouses = await houseService.GetAllAgentHousesByIdAsync(agentId!),
				RentedHouses = await houseService.GetAllUserHousesByIdAsync(User.GetId()!)
			};

			return View(model);
		}
	}
}
