using HouseRenting.Services.Data.Interfaces;
using HouseRenting.Web.Infrastructure.Extensions;
using HouseRenting.Web.ViewModels.Agent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static HouseRenting.Common.NotificationMessagesConstants;

namespace HouseRenting.Web.Controllers
{
	[Authorize]
	public class AgentController : Controller
	{
		private readonly IAgentService agentService;

		public AgentController(IAgentService agentService)
		{
			this.agentService = agentService;
		}

		public async Task<IActionResult> Become()
		{
			string? userId = User.GetId();

			bool isAlreadyAgent = await agentService.ExistsByIdAsync(userId);

			if (isAlreadyAgent)
			{
				TempData[ErrorMessage] = "You are already an Agent!";

				return RedirectToAction("Index", "Home");
			}

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Become(BecomeAgentFormModel model)
		{
			string? userId = User.GetId();

			bool isAlreadyAgent = await agentService.ExistsByIdAsync(userId);

			if (isAlreadyAgent)
			{
				TempData[ErrorMessage] = "You are already an Agent!";

				return RedirectToAction("Index", "Home");
			}

			bool IsPhoneNumberTaken = await agentService.AgentWithPhoneNumberExistsAsync(model.PhoneNumber);

			if (IsPhoneNumberTaken)
			{
				ModelState.AddModelError(nameof(model.PhoneNumber), "An agent with this phone number already exists! Please choose another phone number.");
			}		

			bool userHasRents = await agentService.UserHasRentsByIdAsync(userId);

			if (userHasRents)
			{
				TempData[ErrorMessage] = "You must have no rents to become an agent!";
				return RedirectToAction("Mine", "House");
			}

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			try
			{
				await agentService.Create(userId, model);
			}
			catch (Exception)
			{
				TempData[ErrorMessage] = "An error Occured while trying to register as an agent. Please try again!";
				return RedirectToAction("Index", "Home");
			}

			return RedirectToAction("All", "House");	
		}
	}
}
