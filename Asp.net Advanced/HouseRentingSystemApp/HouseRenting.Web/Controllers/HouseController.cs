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

			bool houseCategoryExists = await houseService.CategoryExistsAsync(model.CategoryId);

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

				await houseService.CreateAsync(model.Title, model.Address, model.Description, model.ImageUrl, model.PricePerMonth, model.CategoryId, agentId);
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

			try
			{
				bool agentExists = await agentService.ExistsByIdAsync(userId);

				if (agentExists)
				{
					var agentId = await agentService.GetAgentIdByUserIdAsync(userId);

					myHouses = await houseService.GetAllAgentHousesByIdAsync(agentId);
				}
				else
				{
					myHouses = await houseService.GetAllUserHousesByIdAsync(userId);
				}
			}
			catch (Exception)
			{

				TempData[ErrorMessage] = "Something went wrong! Please try again or contact support.";
			}


			return View(myHouses);

		}

		[AllowAnonymous]
		public async Task<IActionResult> Details(string id)
		{
			bool houseExists = await houseService.ExistsByIdAsync(id);

			if (!houseExists)
			{
				return BadRequest();
			}

			var house = await houseService.GetHouseDetailsByIdAsync(id);

			return View(house);
		}

		public async Task<IActionResult> Edit(string id)
		{
			if (!await houseService.ExistsByIdAsync(id))
			{
				return BadRequest();
			}

			if (!await agentService.HasHouseByIdAsync(id, User.GetId()!))
			{
				TempData[ErrorMessage] = "You must be the owner of this house to be able to edit!";
				return RedirectToAction("Mine", "House");
			}

			var house = await houseService.GetHouseDetailsByIdAsync(id);
			int houseCategoryId = await houseService.GetCategoryIdAsync(id);

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

			bool houseCategoryExists = await houseService.CategoryExistsAsync(model.CategoryId);

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
				await houseService.EditHouseAsync(model, id);
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
			try
			{
				if (!await houseService.ExistsByIdAsync(id))
				{
					TempData[ErrorMessage] = "A house with that id does not exist!";
					return RedirectToAction("All", "House");
				}

				if (!await agentService.HasHouseByIdAsync(id, User.GetId()!))
				{
					TempData[ErrorMessage] = "You must be the owner of this house to delete it!";
					return RedirectToAction("Mine", "House");
				}

				var house = await houseService.GetHouseDetailsByIdAsync(id);

				var houseModel = new DeleteHouseViewModel()
				{
					Title = house.Title,
					Address = house.Address,
					ImageUrl = house.ImageUrl,
				};

				return View(houseModel);
			}
			catch (Exception)
			{

				TempData[ErrorMessage] = "Something went wrong! Please try again or contact support.";
			}

			return View();

		}

		[HttpPost]
		public async Task<IActionResult> Delete(DeleteHouseViewModel model)
		{
			try
			{
				if (!await houseService.ExistsByIdAsync(model.Id))
				{
					TempData[ErrorMessage] = "A house with that id does not exist!";
					return RedirectToAction("All", "House");
				}

				if (!await agentService.HasHouseByIdAsync(model.Id, User.GetId()!))
				{
					TempData[ErrorMessage] = "You must be the agent of this house to delete it!";
					return RedirectToAction("All", "House");
				}

				await houseService.DeleteAsync(model.Id);
			}
			catch (Exception)
			{
				TempData[ErrorMessage] = "Something went wrong! Please try again or contact support.";
			}

			return RedirectToAction(nameof(Mine));
		}

		[HttpPost]
		public async Task<IActionResult> Rent(string id)
		{
			try
			{
				if (!await houseService.ExistsByIdAsync(id))
				{
					TempData[ErrorMessage] = "A house with that id does not exist!";
					return RedirectToAction("All", "House");
				}

				if (!await agentService.ExistsByIdAsync(User.GetId()!))
				{
					TempData[ErrorMessage] = "You have to be an agent in order to add a new house!";
					return RedirectToAction(nameof(AgentController.Become), "Agent");
				}

				if (await houseService.IsRentedAsync(id))
				{
					TempData[ErrorMessage] = "The house is already rented!";
					return RedirectToAction("All", "House");
				}

				if (await agentService.HasHouseByIdAsync(id, User.GetId()))
				{
					TempData[ErrorMessage] = "You can't rent you own house!";
					return RedirectToAction("All", "House");
				}

				await houseService.RentAsync(id, User.GetId()!);
			}
			catch (Exception)
			{
				TempData[ErrorMessage] = "Something went wrong! Please try again or contact support.";
			}


			return RedirectToAction(nameof(All));
		}

		[HttpPost]
		public async Task<IActionResult> Leave(string id)
		{
			try
			{
				if (!await houseService.ExistsByIdAsync(id))
				{
					TempData[ErrorMessage] = "A house with that id does not exist!";
					return RedirectToAction("All", "House");
				}

				if (!await houseService.IsRentedByCurrentUserAsync(id, User.GetId()!))
				{
					TempData[ErrorMessage] = "You need to be the renter of this house in order to leave it!";
					return RedirectToAction("All", "House");
				}

				await houseService.LeaveAsync(id);
			}
			catch (Exception)
			{
				TempData[ErrorMessage] = "Something went wrong! Please try again or contact support.";
			}



			return RedirectToAction(nameof(Mine));
		}
	}
}
