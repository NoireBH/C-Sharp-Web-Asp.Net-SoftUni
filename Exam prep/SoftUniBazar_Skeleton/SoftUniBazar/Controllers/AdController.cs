using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftUniBazar.Data;
using SoftUniBazar.Data.Models;
using SoftUniBazar.Models.Ad;
using SoftUniBazar.Models.Category;
using System.Security.Claims;
using System.Xml.Linq;

namespace SoftUniBazar.Controllers
{
	public class AdController : Controller
	{
		private readonly BazarDbContext context;
		public AdController(BazarDbContext context)
		{
			this.context = context;
		}


		[HttpGet]
		public IActionResult All()
		{
			AdViewModel[] ads = context
				.Ads
				.Select(a => new AdViewModel()
				{
					Id = a.Id,
					Name = a.Name,
					Description = a.Description,
					ImageUrl = a.ImageUrl,
					Price = a.Price,
					Owner = a.Owner.UserName,
					CreatedOn = a.CreatedOn.ToString("dd/MM/yyyy H:mm"),
					Category = a.Category.Name
				}).ToArray();

			return View(ads);
		}

		[HttpGet]
		public IActionResult Cart()
		{
			string currentUserId = GetUserId();

			AdViewModel[] ads = context
				.AdBuyers
				.Where(u => u.BuyerId == currentUserId)
				.Select(ab => new AdViewModel()
				{
					Id = ab.Ad.Id,
					Name = ab.Ad.Name,
					Description = ab.Ad.Description,
					ImageUrl = ab.Ad.ImageUrl,
					Price = ab.Ad.Price,
					Owner = ab.Ad.Owner.UserName,
					CreatedOn = ab.Ad.CreatedOn.ToString("dd/MM/yyyy H:mm"),
					Category = ab.Ad.Category.Name
				}).ToArray();

			return View(ads);
		}

		[HttpGet]
		public async Task<IActionResult> Add()
		{
			AdFormModel adFormModel = new AdFormModel()
			{
				Categories = await GetCategories()
			};

			return View(adFormModel);
		}

		[HttpPost]
		public async Task<IActionResult> Add(AdFormModel model)
		{
			var categories = await GetCategories();

			if (!categories.Any(e => e.Id == model.CategoryId))
			{
				ModelState.AddModelError(nameof(model.CategoryId), "Category does not exist!");
			}

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			string currentUserId = GetUserId();

			Ad adToAdd = new Ad()
			{
				Name = model.Name,
				Description = model.Description,
				CreatedOn = DateTime.Now,
				CategoryId = model.CategoryId,
				Price = model.Price,
				OwnerId = currentUserId,
				ImageUrl = model.ImageUrl
			};

			await context.Ads.AddAsync(adToAdd);
			await context.SaveChangesAsync();

			return RedirectToAction("All", "Ad");
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			Ad adToEdit = await context.Ads.FindAsync(id);

			if (adToEdit == null)
			{
				return BadRequest();
			}

			string currentUserId = GetUserId();
			if (currentUserId != adToEdit.OwnerId)
			{
				return Unauthorized();
			}

			AdFormModel adFormModel = new AdFormModel()
			{
				Name = adToEdit.Name,
				Description = adToEdit.Description,
				Price = adToEdit.Price,
				CategoryId = adToEdit.CategoryId,
				Categories = await GetCategories(),
				ImageUrl = adToEdit.ImageUrl
			};

			return View(adFormModel);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, AdFormModel model)
		{
			Ad adToEdit = await context.Ads.FindAsync(id);

			if (adToEdit == null)
			{
				return BadRequest();
			}

			string currentUserId = GetUserId();
			if (currentUserId != adToEdit.OwnerId)
			{
				return Unauthorized();
			}

			var categories = await GetCategories();

			if (!categories.Any(e => e.Id == model.CategoryId))
			{
				ModelState.AddModelError(nameof(model.CategoryId), "Category does not exist!");
			}

			if (!ModelState.IsValid)
			{
				return View(model);
			}



			AdFormModel adFormModel = new AdFormModel()
			{
				Name = adToEdit.Name,
				Description = adToEdit.Description,
				Price = adToEdit.Price,
				CategoryId = adToEdit.CategoryId,
				Categories = await GetCategories(),
				ImageUrl = adToEdit.ImageUrl
			};

			adToEdit.Name = model.Name;
			adToEdit.Description = model.Description;
			adToEdit.ImageUrl = model.ImageUrl;
			adToEdit.CategoryId = model.CategoryId;
			adToEdit.Price = model.Price;

			await context.SaveChangesAsync();

			return RedirectToAction("All", "Ad");
		}

		[HttpPost]
		public async Task<IActionResult> AddToCart(int id)
		{
			var adToAdd = await context
				.Ads
				.FindAsync(id);

			if (adToAdd == null)
			{
				return BadRequest();
			}

			string currentUserId = GetUserId();

			var adBuyer = new AdBuyer()
			{
				AdId = adToAdd.Id,
				BuyerId = currentUserId,
			};

			if (await context.AdBuyers.ContainsAsync(adBuyer))
			{
				return RedirectToAction("Cart", "Ad");
			}

			await context.AdBuyers.AddAsync(adBuyer);
			await context.SaveChangesAsync();

			return RedirectToAction("Cart", "Ad");

		}

		[HttpPost]
		public async Task<IActionResult> RemoveFromCart(int id)
		{
			var currentUser = GetUserId();
			var adToRemove = await context.Ads.FindAsync(id);

			if (adToRemove == null)
			{
				return BadRequest();
			}

			var adBuyer = await context.AdBuyers.FirstOrDefaultAsync(ab => ab.BuyerId == currentUser && ab.AdId == id);

			context.AdBuyers.Remove(adBuyer!);
			await context.SaveChangesAsync();

			return RedirectToAction("All", "Ad");
		}


		private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier);

		private async Task<IEnumerable<CategoryViewModel>> GetCategories()
		{
			CategoryViewModel[] Categories = await context
				.Categories
				.Select(c => new CategoryViewModel()
				{
					Id = c.Id,
					Name = c.Name
				}).ToArrayAsync();

			return Categories;
		}
	}
}
