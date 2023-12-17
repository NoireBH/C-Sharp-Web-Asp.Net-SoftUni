using Microsoft.EntityFrameworkCore;
using ProductsApi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsApi.Data
{
	public class ProductsApiDbContext : DbContext
	{
		public ProductsApiDbContext(DbContextOptions<ProductsApiDbContext> options)
			: base(options)
		{
			this.Database.EnsureCreated();
		}

		public DbSet<Product> Products { get; set; } = null!;

		protected override void OnModelCreating(ModelBuilder builder)
		{			
			base.OnModelCreating(builder);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
		}
	}
}
