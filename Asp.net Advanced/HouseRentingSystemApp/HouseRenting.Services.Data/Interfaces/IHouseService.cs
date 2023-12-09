using HouseRenting.Services.Data.Models.House;
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

		Task<bool> CategoryExists(int categoryId);

		Task Create(string title, string address, string description,
			 string imageUrl, decimal price, int categoryId, string agentId);

		Task<AllHousesFilteredServiceModel> AllAsync(AllHousesQueryModel model);

		Task<IEnumerable<string>> GetAllCategoryNamesAsync();

		Task<IEnumerable<HouseAllViewModel>> GetAllAgentHousesById(string agentId);

		Task<IEnumerable<HouseAllViewModel>> GetAllUserHousesById(string userId);

		Task<bool> ExistsById(string houseId);

		Task<HouseDetailsViewModel> GetHouseDetailsById(string houseId);

		Task EditHouse(AddOrEditHouseFormModel model, string houseId);

		Task<int> GetCategoryId(string houseId);
	}
}
