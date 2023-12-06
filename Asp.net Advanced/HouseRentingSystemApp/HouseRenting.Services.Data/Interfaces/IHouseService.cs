﻿using HouseRenting.Services.Data.Models.House;
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
	}
}
