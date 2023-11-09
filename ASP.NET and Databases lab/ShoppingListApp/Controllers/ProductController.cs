using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Infrastructure;
using ShoppingList.Infrastructure.Models;
using ShoppingListApp.Models.Product;

namespace ShoppingListApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly ShoppingListDbContext context;

        public ProductController(ShoppingListDbContext context)
        {
            this.context = context;
        }

		[HttpGet]
		public async Task<IActionResult> All()
        {
            var products = await context.Products
                .Select(p => new ProductViewModel()
                {
                    Id = p.Id,
                    ProductName = p.ProductName
                })
                .AsNoTracking()
                .ToListAsync();

            return View(products);
        }

        [HttpGet]
        public  IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductFormModel model)
        {
            Product product = new Product()
            {
                ProductName = model.ProductName
            };

            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var productToEdit = await context.Products.FindAsync(id);

            return View(new ProductFormModel()
            {
                ProductName = productToEdit!.ProductName
            });

        }

		[HttpPost]
		public async Task<IActionResult> Edit(int id, Product model)
		{
            var product = await context.Products.FindAsync(id);
            product.ProductName = model.ProductName;

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));

		}

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var productToDelete = await context.Products.FindAsync(id);

            context.Products.Remove(productToDelete!);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }
	}
}
