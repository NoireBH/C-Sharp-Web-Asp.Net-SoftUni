using HouseRenting.Data.Models;
using HouseRenting.Services.Data.Interfaces;
using HouseRenting.Web.Data;
using HouseRenting.Web.ViewModels.Category;
using HouseRenting.Web.ViewModels.Home;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRenting.Services.Data
{
	public class HouseService : IHouseService
	{
		private readonly HouseRentingDbContext dbContext;

		public HouseService(HouseRentingDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<bool> CategoryExists(int categoryId)
		{
			return await dbContext.Categories.AnyAsync(c => c.Id == categoryId);
		}

		public async Task<int> Create(string title, string address, string description, string imageUrl, decimal price, int categoryId, string agentId)
		{
			House house = new House()
			{
				Title = title,
				Address = address,
				Description = description,
				ImageUrl = imageUrl,
				PricePerMonth = price,
				CategoryId = categoryId,
				AgentId = Guid.Parse(agentId)
			};

			await dbContext.AddAsync(house);
			await dbContext.SaveChangesAsync();

			throw new NotImplementedException();
		}

		public async Task<IEnumerable<HouseCategoryFormModel>> GetAllHouseCategoriesAsync()
		{
			IEnumerable<HouseCategoryFormModel> categories = await dbContext.Categories
				.Select(c => new HouseCategoryFormModel
				{
					Id = c.Id,
					Name = c.Name
				})
				.AsNoTracking()
				.ToListAsync();

			return categories;
		}

		public async Task<IEnumerable<IndexViewModel>> GetLastThreeHousesAsync()
		{
			IEnumerable<IndexViewModel> lastThreeHouses = await dbContext.Houses
				.Where(h => h.IsActive)
				.OrderByDescending(h => h.CreatedOn)
				.Take(3)
				.Select(h => new IndexViewModel
				{
					Id = h.Id.ToString(),
					Title = h.Title,
					ImageUrl = h.ImageUrl
				})
				.ToArrayAsync();

			return lastThreeHouses;
		}
	}
}
