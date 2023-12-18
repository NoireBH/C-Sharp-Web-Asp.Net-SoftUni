using Microsoft.EntityFrameworkCore;
using ProductsApi.Data;
using ProductsApi.Data.Models;
using ProductsApi.Services.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsApi.Services.Data
{
	public class ProductService : IProductService
	{
		private readonly ProductsApiDbContext context;

		public ProductService(ProductsApiDbContext context)
		{
			this.context = context;
		}

		public async Task<Product> CreateProduct(string name, string description)
		{
			Product product = new Product()
			{
				Name = name,
				Description = description
			};

			await context.Products.AddAsync(product);
			await context.SaveChangesAsync();

			return product;
		}

		public async void EditProduct(int id, Product product)
		{
			var productToEdit = await context.Products.FindAsync(id);

			productToEdit.Name = product.Name;
			productToEdit.Description = product.Description;

			await context.SaveChangesAsync();
		}

		public List<Product> GetAllProducts()
		{
			return context.Products.ToList();
		}

		public async Task<Product?> GetById(int id)
		{
			return await context.Products.FindAsync(id);
		}
	}
}
