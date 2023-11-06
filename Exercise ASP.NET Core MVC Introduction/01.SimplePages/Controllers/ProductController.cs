using _01.SimplePages.Models.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace _01.SimplePages.Controllers
{
	public class ProductController : Controller
	{
		private IEnumerable<ProductViewModel> products = new List<ProductViewModel>()
		{
			new ProductViewModel()
			{
				Id = 1,
				Name = "Swiss Cheese",
				Price = 9
			},
			new ProductViewModel()
			{
				Id= 2,
				Name = "Milk",
				Price = 2.50m
			},
			new ProductViewModel()
			{
				Id = 3,
				Name = "Monster Energy Drink",
				Price = 3
			}

		};
		public IActionResult Index()
		{
			return View();
		}

		[ActionName("My-Products")]
		public IActionResult All(string keyword)
		{
			if (keyword != null)
			{
				var productsToShow = products
					.Where(p => p.Name.ToLower()
					.Contains(keyword.ToLower()));

				return View(productsToShow);
			}

			return View(products);
		}

		public IActionResult ById(int id)
		{
			var product = products.FirstOrDefault(p => p.Id == id);

			if (product == null)
			{
				return BadRequest();
			}

			return View(product);
		}

		public IActionResult AllAsJson()
		{
			var options = new JsonSerializerOptions
			{
				WriteIndented = true
			};

			return Json(products, options);
		}

		public IActionResult AllAsText()
		{
			StringBuilder sb = new StringBuilder();

			foreach (var product in products)
			{
				sb.AppendLine($"Product {product.Id}: {product.Name} - {product.Price} lv.");
			}

			return Content(sb.ToString());
		}

		public IActionResult AllAsTextFile()
		{
			StringBuilder sb = new StringBuilder();

			foreach (var product in products)
			{
				sb.AppendLine($"Product {product.Id}: {product.Name} - {product.Price} lv.");
			}

			Response.Headers.Add(HeaderNames.ContentDisposition, @"attachment;filename=products.txt");

			return File(Encoding.UTF8.GetBytes(sb.ToString().TrimEnd()), "text/plain");
		}
	}
}
