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
		public async Task<IActionResult> All([FromQuery] AllHousesQueryModel queryModel)
		{
			var queryResult = await houseService.AllAsync(queryModel);

			queryModel.TotalHousesCount = queryResult.TotalHousesCount;
			queryModel.Houses = queryResult.Houses;
			queryModel.Categories = await houseService.GetAllCategoryNamesAsync();

			return View(queryModel);
		}

		public async Task<IActionResult> Add()
		{
			bool isAgent = await agentService.ExistsByIdAsync(User.GetId()!);

			if (!isAgent)
			{
				TempData[ErrorMessage] = "You have to be an agent in order to add a new house!";
				return RedirectToAction(nameof(AgentController.Become), "Agent");
			}

			return View(new AddOrEditHouseFormModel
			{
				Categories = await houseService.GetAllHouseCategoriesAsync()
			});
		}

		[HttpPost]
		public async Task<IActionResult> Add(AddOrEditHouseFormModel model)
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
			catch (Exception)
			{
				ModelState.AddModelError(string.Empty, "Unexpected error has occured, please try again...");
				model.Categories = await houseService.GetAllHouseCategoriesAsync();
				return View(model);

			}

			return RedirectToAction("All", "House");
		}

		public async Task<IActionResult> Mine()
		{
			IEnumerable<HouseAllViewModel> myHouses = null;

			string? userId = User.GetId();
			bool agentExists = await agentService.ExistsByIdAsync(userId);

			if (agentExists)
			{
				var agentId = await agentService.GetAgentIdByUserIdAsync(userId);

				myHouses = await houseService.GetAllAgentHousesById(agentId);
			}
			else
			{
				myHouses = await houseService.GetAllUserHousesById(userId);
			}

			return View(myHouses);

		}

		[AllowAnonymous]
		public async Task<IActionResult> Details(string id)
		{
			bool houseExists = await houseService.ExistsById(id);

			if (!houseExists)
			{
				return BadRequest();
			}

			var house = await houseService.GetHouseDetailsById(id);

			return View(house);
		}

		public async Task<IActionResult> Edit(string id)
		{
			if (!await houseService.ExistsById(id))
			{
				return BadRequest();
			}

			if (!await agentService.HasHouseById(id, User.GetId()!))
			{
				TempData[ErrorMessage] = "You must be the owner of this house to be able to edit!";
				return RedirectToAction("Mine", "House");
			}

			var house = await houseService.GetHouseDetailsById(id);
			int houseCategoryId = await houseService.GetCategoryId(id);

			var houseModel = new AddOrEditHouseFormModel()
			{
				Title = house.Title,
				Address = house.Address,
				Description = house.Description,
				ImageUrl = house.ImageUrl,
				PricePerMonth = house.PricePerMonth,
				CategoryId = houseCategoryId,
				Categories = await houseService.GetAllHouseCategoriesAsync()
			};

			return View(houseModel);

		}

		[HttpPost]
		public async Task<IActionResult> Edit(string id, AddOrEditHouseFormModel model)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			bool isAgent = await agentService.ExistsByIdAsync(User.GetId()!);

			if (!isAgent)
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
				await houseService.EditHouse(model, id);
			}
			catch (Exception)
			{
				ModelState.AddModelError
					(string.Empty, "Unexpected error has occured, while trying to edit house details, please try again later...");
				model.Categories = await houseService.GetAllHouseCategoriesAsync();
				return View(model);

			}			

			return RedirectToAction(nameof(Details), new { id = id });
		}

		public async Task<IActionResult> Delete(string id)
		{
			if (!await houseService.ExistsById(id))
			{
				TempData[ErrorMessage] = "A house with that id does not exist!";
				return RedirectToAction("All", "House");
			}

			if (!await agentService.HasHouseById(id, User.GetId()!))
			{
				TempData[ErrorMessage] = "You must be the agent of this house to delete it!";
				return RedirectToAction("Mine", "House");
			}

			var house = await houseService.GetHouseDetailsById(id);

			var houseModel = new DeleteHouseViewModel()
			{
				Title = house.Title,
				Address = house.Address,
				ImageUrl = house.ImageUrl,
			};

			return View(houseModel);
		}

		[HttpPost]
		public async Task<IActionResult> Delete(DeleteHouseViewModel model)
		{
			if (!await houseService.ExistsById(model.Id))
			{
				TempData[ErrorMessage] = "A house with that id does not exist!";
				return RedirectToAction("All", "House");
			}

			if (!await agentService.HasHouseById(model.Id, User.GetId()!))
			{
				TempData[ErrorMessage] = "You must be the agent of this house to delete it!";
				return RedirectToAction("All", "House");
			}

			await houseService.Delete(model.Id);

			return RedirectToAction(nameof(Mine));
		}
	}
}
