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
