using HouseRenting.Data.Models;
using HouseRenting.Services.Data.Interfaces;
using HouseRenting.Services.Data.Models.House;
using HouseRenting.Services.Data.Models.Statistics;
using HouseRenting.Web.Data;
using HouseRenting.Web.ViewModels.Agent;
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
				.OrderByDescending(h => h.CreatedOn),
				HouseSort.Oldest => housesQuery
				.OrderBy(h => h.CreatedOn),
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

		public async Task<bool> CategoryExistsAsync(int categoryId)
		{
			return await dbContext.Categories.AnyAsync(c => c.Id == categoryId);
		}

		public async Task CreateAsync(string title, string address, string description, string imageUrl, decimal price, int categoryId, string agentId)
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

		public async Task EditHouseAsync(AddOrEditHouseFormModel model, string houseId)
		{
			House? house = await dbContext.Houses.FirstAsync(h => h.Id.ToString() == houseId);

			house!.Title = model.Title;
			house.Address = model.Address;
			house.Description = model.Description;
			house.ImageUrl = model.ImageUrl;
			house.PricePerMonth = model.PricePerMonth;
			house.CategoryId = model.CategoryId;

			await dbContext.SaveChangesAsync();
		}

		public async Task<bool> ExistsByIdAsync(string houseId)
		{
			return await dbContext.Houses.AnyAsync(h => h.Id.ToString() == houseId);
		}

		public async Task<IEnumerable<HouseAllViewModel>> GetAllAgentHousesByIdAsync(string agentId)
		{
			var houses = await dbContext.Houses
				.Where(h => h.AgentId.ToString() == agentId)
				.Select(h => new HouseAllViewModel
				{
					Id = h.Id.ToString(),
					Title = h.Title,
					Address = h.Address,
					ImageUrl = h.ImageUrl,
					PricePerMonth = h.PricePerMonth,
					IsRented = h.RenterId != null
				})
				.ToListAsync();

			return houses;
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

		public async Task<IEnumerable<HouseAllViewModel>> GetAllUserHousesByIdAsync(string userId)
		{
			var houses = await dbContext.Houses
				.Where(h => h.RenterId.ToString() == userId)
				.Select(h => new HouseAllViewModel
				{
					Id = h.Id.ToString(),
					Title = h.Title,
					Address = h.Address,
					ImageUrl = h.ImageUrl,
					PricePerMonth = h.PricePerMonth,
					IsRented = h.RenterId != null
				})
				.ToListAsync();

			return houses;
		}

		public async Task<HouseDetailsViewModel> GetHouseDetailsByIdAsync(string houseId)
		{
			var house = await dbContext.Houses
				.Where(h => h.Id.ToString() ==  houseId)
				.Select(h => new HouseDetailsViewModel
				{
					Id= h.Id.ToString(),
					Title = h.Title,
					Address = h.Address,
					ImageUrl = h.ImageUrl,
					PricePerMonth = h.PricePerMonth,
					Description = h.Description,
					Category = h.Category.Name,
					IsRented = h.RenterId != null,
					Agent = new AgentDetailsViewModel()
					{
						Email = h.Agent.User.Email,
						PhoneNumber = h.Agent.PhoneNumber
					}
				})
				.FirstOrDefaultAsync();

			return house!;
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

		public async Task<int> GetCategoryIdAsync(string houseId)
		{
			int categoryId = await dbContext.Houses
				.Where( h => h.Id.ToString() == houseId )
				.Select(h => h.CategoryId)
				.FirstOrDefaultAsync();

			return categoryId;
		}

		public async Task DeleteAsync(string houseId)
		{
			var house = await dbContext.Houses.FirstOrDefaultAsync(h => h.Id.ToString() == houseId);

			dbContext.Houses.Remove(house!);
			await dbContext.SaveChangesAsync();

		}

		public async Task<bool> IsRentedAsync(string id)
		{
			var house = await dbContext.Houses.FirstOrDefaultAsync(h => h.Id.ToString() == id);
			var result = house!.RenterId != null;
			return result;
		}

		public async Task<bool> IsRentedByCurrentUserAsync(string houseId, string userId)
		{
			var house = await dbContext.Houses.FirstOrDefaultAsync(h => h.Id.ToString() == houseId);

			if (house!.RenterId.ToString() != userId)
			{
				return false;
			}

			return true;
		}

		public async Task RentAsync(string houseId, string userId)
		{
			var house = await dbContext.Houses.FirstOrDefaultAsync(h => h.Id.ToString() == houseId);
			house!.RenterId = Guid.Parse(userId);
			await dbContext.SaveChangesAsync();
		}

		public async Task LeaveAsync(string houseId)
		{
			var house = await dbContext.Houses.FirstOrDefaultAsync(h => h.Id.ToString() == (houseId));
			house!.RenterId = null;

			await dbContext.SaveChangesAsync();
		}

		public async Task<StatisticServiceModel> GetStatisticsAsync()
		{
			int totalHouses = await dbContext.Houses.CountAsync();
			int totalRents = await dbContext.Houses
				.Where(h => h.RenterId != null)
				.CountAsync();

			return new StatisticServiceModel()
			{
				TotalHouses = totalHouses,
				TotalRents = totalRents
			};
		}
	}
}
