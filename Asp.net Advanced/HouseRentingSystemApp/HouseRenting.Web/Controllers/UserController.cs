using HouseRenting.Data.Models;
using HouseRenting.Web.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace HouseRenting.Web.Controllers
{
	using static Common.GeneralConstants;
	public class UserController : Controller
	{
		private readonly SignInManager<ApplicationUser> signInManager;
		private readonly UserManager<ApplicationUser> userManager;
		private readonly IMemoryCache cache;

		public UserController(
			UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager,
			IMemoryCache cache)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
			this.cache = cache;
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
			cache.Remove(UsersCacheKey);
			return RedirectToAction("Index", "Home");
		}
	}
}
