using HouseRenting.Services.Data.Interfaces;
using HouseRenting.Web.Infrastructure.Extensions;
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
	}
}
