using HouseRenting.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRenting.Data.Configurations
{
	internal class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			builder.HasData(SeedCategories());
		}

		private Category[] SeedCategories()
		{
			var categories = new HashSet<Category>();

			Category category;

			category = new Category()
			{
				Id = 1,
				Name = "Cottage"
			};

			categories.Add(category);

			category = new Category()
			{
				Id = 2,
				Name = "Single-Family"
			};

			categories.Add(category);

			category = new Category()
			{
				Id = 3,
				Name = "Duplex"
			};

			categories.Add(category);

			return categories.ToArray();
		}

	}
}
