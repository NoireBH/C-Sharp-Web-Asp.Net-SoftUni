﻿using HouseRenting.Services.Data;
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
			return View(this.Ok());
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

		[HttpPost]
		public async Task<IActionResult> Add(AddHouseFormModel model)
		{
			bool agentExists = await agentService.ExistsByIdAsync(User.GetId()!);

            if (!agentExists)
            {
				return RedirectToAction(nameof(AgentController.Become), "Agent");
            }

			bool houseCategoryExists = await houseService.CategoryExists(model.CategoryId);

			if (!houseCategoryExists)
			{
				//makes the model state invalid just like !ModelState.Isvalid
				ModelState.AddModelError(nameof(model.CategoryId), "This category does not exist!");
			}

			if (!ModelState.IsValid)
			{
				model.Categories = await houseService.GetAllHouseCategoriesAsync();
				return View(model);
			}


			//should always use try-catch incase of Database errors or incase the db goes offline!
			try
			{
				string? agentId = await agentService.GetAgentIdByUserIdAsync(User.GetId()!);

				await houseService.Create(model.Title, model.Address, model.Description, model.ImageUrl, model.PricePerMonth, model.CategoryId, agentId);
			}
			catch (Exception e)
			{
				ModelState.AddModelError(string.Empty, "Unexpected error has occured, please try again...");
				model.Categories = await houseService.GetAllHouseCategoriesAsync();
				return View(model);

			}

			return RedirectToAction("All", "House");
        }
	}
}
