using HouseRenting.Services.Data.Models.House;
using HouseRenting.Services.Data.Models.Statistics;
using HouseRenting.Web.ViewModels.Category;
using HouseRenting.Web.ViewModels.Home;
using HouseRenting.Web.ViewModels.House;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRenting.Services.Data.Interfaces
{
	public interface IHouseService
	{
		Task<IEnumerable<IndexViewModel>> GetLastThreeHousesAsync();

		Task<IEnumerable<HouseCategoryFormModel>> GetAllHouseCategoriesAsync();

		Task<bool> CategoryExistsAsync(int categoryId);

		Task CreateAsync(string title, string address, string description,
			 string imageUrl, decimal price, int categoryId, string agentId);

		Task<AllHousesFilteredServiceModel> AllAsync(AllHousesQueryModel model);

		Task<IEnumerable<string>> GetAllCategoryNamesAsync();

		Task<IEnumerable<HouseAllViewModel>> GetAllAgentHousesByIdAsync(string agentId);

		Task<IEnumerable<HouseAllViewModel>> GetAllUserHousesByIdAsync(string userId);

		Task<bool> ExistsByIdAsync(string houseId);

		Task<HouseDetailsViewModel> GetHouseDetailsByIdAsync(string houseId);

		Task EditHouseAsync(AddOrEditHouseFormModel model, string houseId);

		Task<int> GetCategoryIdAsync(string houseId);

		Task DeleteAsync(string houseId);

		Task<bool> IsRentedAsync(string id);

		Task<bool> IsRentedByCurrentUserAsync(string houseId, string userId);

		Task RentAsync(string houseId, string userId);

		Task LeaveAsync(string houseId);

		Task<StatisticServiceModel> GetStatisticsAsync();
	}
}
