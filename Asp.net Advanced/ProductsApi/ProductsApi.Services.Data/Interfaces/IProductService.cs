using ProductsApi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsApi.Services.Data.Interfaces
{
	public interface IProductService
	{
		List<Product> GetAllProducts();

		Task<Product?> GetById(int id);

		Task<Product> CreateProduct(string name, string description);

		Task EditProduct(int id, Product product);

		Task EditProductPartially(int id, Product product);
	}
}
