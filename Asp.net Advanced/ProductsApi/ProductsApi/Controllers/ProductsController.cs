using Microsoft.AspNetCore.Mvc;
using ProductsApi.Data.Models;
using ProductsApi.Services.Data;
using ProductsApi.Services.Data.Interfaces;

namespace ProductsApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
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

		[HttpGet("{id}", Name = "GetProduct")]
		public async Task<ActionResult<Product>> GetProduct(int id)
		{
			var product = await productService.GetById(id);

			if (product == null)
			{
				return NotFound();
			}

			return product;
		}

		[HttpPost]
		public async Task<ActionResult<Product>> PostProduct(Product product)
		{
			product = await productService.CreateProduct(product.Name,product.Description);

			return CreatedAtAction(nameof(GetProduct), product, product);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> PutProduct(int id , Product product)
		{
			if (id != product.Id)
			{
				return BadRequest();
			}


			if (await productService.GetById(id) == null)
			{
				return NotFound();
			}

			productService.EditProduct(id, product);

			return NoContent();
		}
	}
}
