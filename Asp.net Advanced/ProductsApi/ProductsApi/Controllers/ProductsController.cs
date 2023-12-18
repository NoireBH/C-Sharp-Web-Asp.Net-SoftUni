using Microsoft.AspNetCore.Mvc;
using ProductsApi.Data.Models;
using ProductsApi.Services.Data;
using ProductsApi.Services.Data.Interfaces;

namespace ProductsApi.Controllers
{
	[ApiController]
	[Route("api/products")]
	public class ProductsController : Controller
	{
		private readonly IProductService productService;

		public ProductsController(IProductService productService)
		{
			this.productService = productService;
		}

		[HttpGet]
		public ActionResult<IEnumerable<Product>> GetProducts()
		{
			return  productService.GetAllProducts();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetProduct(int id)
		{
			var product = await productService.GetById(id);

			if (product == null)
			{
				return NotFound();
			}

			return product;
		}
	}
}
