using HouseRenting.Data.Models;
using HouseRenting.Services.Data.Interfaces;
using HouseRenting.Services.Data.Models.House;
using HouseRenting.Web.Data;
using HouseRenting.Web.ViewModels.Category;
using HouseRenting.Web.ViewModels.Home;
using HouseRenting.Web.ViewModels.House;
using HouseRenting.Web.ViewModels.House.Enums;
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

		public async Task<AllHousesFilteredServiceModel> AllAsync(AllHousesQueryModel model)
		{
			var housesQuery = dbContext.Houses.AsQueryable();

			if (!string.IsNullOrWhiteSpace(model.Category))
			{
				housesQuery = dbContext.Houses
					.Where(h => h.Category.Name == model.Category);
			}

			if (!string.IsNullOrWhiteSpace(model.SearchTerm))
			{
				housesQuery = housesQuery
					.Where(h =>
						h.Title.ToLower().Contains(model.SearchTerm.ToLower()) ||
						h.Address.ToLower().Contains(model.SearchTerm.ToLower()) ||
						h.Description.ToLower().Contains(model.SearchTerm.ToLower()));
			}

			housesQuery = model.HouseSorting switch
			{
				HouseSort.Newest => housesQuery
				.OrderBy(h => h.CreatedOn),
				HouseSort.Oldest => housesQuery
				.OrderByDescending(h => h.CreatedOn),
				HouseSort.PriceDescending => housesQuery
				.OrderByDescending(h => h.PricePerMonth),
				HouseSort.PriceAscending => housesQuery
				.OrderBy(h => h.PricePerMonth),
				HouseSort.NotRentedFirst => housesQuery
				.OrderBy(h => h.RenterId != null)
				.ThenByDescending(h => h.Id),
				_ => housesQuery.OrderByDescending(h => h.Id)
			};

			var houses = await housesQuery
				.Skip((model.CurrentPage - 1) * model.HousesPerPage)
				.Take(model.HousesPerPage)
				.Select(h => new HouseAllViewModel
				{
					Id = h.Id.ToString(),
					Title = h.Title,
					Address = h.Address,
					ImageUrl = h.ImageUrl,
					IsRented = h.RenterId.HasValue,
					PricePerMonth = h.PricePerMonth
				})
				.ToArrayAsync();

			int totalHouses = housesQuery.Count();

			return new AllHousesFilteredServiceModel()
			{
				TotalHousesCount = totalHouses,
				Houses = houses
			};
		}

		public async Task<bool> CategoryExists(int categoryId)
		{
			return await dbContext.Categories.AnyAsync(c => c.Id == categoryId);
		}

		public async Task Create(string title, string address, string description, string imageUrl, decimal price, int categoryId, string agentId)
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
		}

		public async Task<IEnumerable<string>> GetAllCategoryNamesAsync()
		{
			return await dbContext.Categories.Select(c => c.Name).ToArrayAsync();
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
