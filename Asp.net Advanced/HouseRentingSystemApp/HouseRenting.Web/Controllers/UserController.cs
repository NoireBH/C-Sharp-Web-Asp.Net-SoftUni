using HouseRenting.Data.Models;
using HouseRenting.Web.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HouseRenting.Web.Controllers
{
	public class UserController : Controller
	{
		private readonly SignInManager<ApplicationUser> signInManager;
		private readonly UserManager<ApplicationUser> userManager;
		private readonly IUserStore<ApplicationUser> userStore;

		public UserController(
			UserManager<ApplicationUser> userManager,
			IUserStore<ApplicationUser> userStore,
			SignInManager<ApplicationUser> signInManager)
		{
			this.userManager = userManager;
			this.userStore = userStore;
			this.signInManager = signInManager;
		}

		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterFormModel model)
		{

			if (!ModelState.IsValid)
			{
				return this.View(model);
			}

			ApplicationUser user = new ApplicationUser
			{
				Email = model.Email,
				FirstName = model.FirstName,
				LastName = model.LastName
			};

			await this.userManager.SetEmailAsync(user, model.Email);
			await this.userManager.SetUserNameAsync(user, model.Email);

			IdentityResult result = await this.userManager.CreateAsync(user, model.Password);

			if (!result.Succeeded)
			{
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}

				return View(model);
			}

			await signInManager.SignInAsync(user, isPersistent: false);
			return RedirectToAction("Index", "Home");
		}
	}
}
