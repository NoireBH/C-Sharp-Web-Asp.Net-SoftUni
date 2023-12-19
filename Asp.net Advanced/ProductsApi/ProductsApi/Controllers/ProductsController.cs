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

		/// <summary>
		/// Gets all products.
		/// </summary>
		/// <remarks>
		/// Sample request: 
		/// 
		///		GET /api/products
		///		{
		///		
		///		}
		/// </remarks>
		/// <response code="200">
		/// Returns "OK" with a list of all products
		/// </response>
		[HttpGet]
		public ActionResult<IEnumerable<Product>> GetProducts()
		{
			return  productService.GetAllProducts();
		}


		/// <summary>
		/// Gets a specific product by id.
		/// </summary>
		/// <remarks>
		/// Sample request: 
		/// 
		///		GET /api/products/{id}
		///		{
		///		
		///		}
		/// </remarks>
		/// <response code="200">
		/// Returns "OK" with the requested product.
		/// </response>
		/// <response code="404">
		/// Returns "Not found" if a product with that id does not exist.
		/// </response>
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

		/// <summary>
		/// Creates a new product.
		/// </summary>
		/// <remarks>
		/// Sample request: 
		/// 
		///		POST /api/products/
		///		{
		///			"name": "Fish"
		///			"description": "A Cat's favorite food"
		///		}
		/// </remarks>
		/// <response code="201">
		/// Returns "Created" and the created product.
		/// </response>
		[HttpPost]
		public async Task<ActionResult<Product>> PostProduct(Product product)
		{
			product = await productService.CreateProduct(product.Name,product.Description);

			return CreatedAtAction(nameof(GetProduct), product, product);
		}

		/// <summary>
		/// Edits all of the information of a product.
		/// </summary>
		/// <remarks>
		/// Sample request: 
		/// 
		///		PUT /api/products/{id}
		///		{
		///			"name": "Edited Fish"
		///			"description": "This description also needed to be edited"
		///		}
		/// </remarks>
		/// <response code="400">
		/// Returns "Bad Request" when an invalid request was send.
		/// </response>
		/// <response code="204">
		/// Returns "No content".
		/// </response>
		/// <response code="404">
		/// Returns "Not found" if a product with this id does not exist.
		/// </response>
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

			await productService.EditProduct(id, product);

			return NoContent();
		}

		/// <summary>
		/// Edits only the information that you provide.
		/// </summary>
		/// <remarks>
		/// Sample request: 
		/// 
		///		PATCH /api/products/{id}
		///		{		
		///			"description": "You don't need to edit the name if you dont want to"
		///		}
		/// </remarks>
		/// <response code="400">
		/// Returns "Bad Request" when an invalid request was send.
		/// </response>
		/// <response code="204">
		/// Returns "No content".
		/// </response>
		/// <response code="404">
		/// Returns "Not found" if a product with this id does not exist.
		/// </response>
		[HttpPatch("{id}")]
		public async Task<IActionResult> PatchProduct(int id, Product product)
		{
			if (await productService.GetById(id) == null)
			{
				return NotFound();
			}

			await productService.EditProductPartially(id, product);

			return NoContent();
		}

		/// <summary>
		/// Deletes a product by id.
		/// </summary>
		/// <remarks>
		/// Sample request: 
		/// 
		///		DELETE /api/products/{id}
		///		{
		///			
		///		}
		/// </remarks>
		/// <response code="404">
		/// Returns "Not found" if a product with this id does not exist.
		/// </response>
		/// <response code="200">
		/// Returns "OK" with the deleted product.
		/// </response>
		[HttpDelete("{id}")]
		public async Task<ActionResult<Product>> DeleteProduct(int id)
		{
			Product? product = await productService.GetById(id);

			if (product == null)
			{
				return NotFound();
			}

			await productService.DeleteProduct(id);

			return product;
		}
	}
}
