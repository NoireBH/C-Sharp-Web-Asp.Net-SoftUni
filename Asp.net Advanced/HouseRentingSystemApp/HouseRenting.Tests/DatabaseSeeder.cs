using HouseRenting.Data.Models;
using HouseRenting.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRenting.Services.Tests
{
	public static class DatabaseSeeder
	{
		public static ApplicationUser AgentUser;
		public static ApplicationUser RenterUser;
		public static Agent Agent;
		public static House House;
		public static House RentedHouse;

		public static void SeedDatabase(HouseRentingDbContext dbContext)
		{
			AgentUser = new ApplicationUser()
			{
				FirstName = "Elmir",
				LastName = "Mustafov",
				UserName = "Noire",
				NormalizedUserName = "ELMIR.MUSTAFOV@ABV.BG",
				Email = "Elmir.mustafov@abv.bg",
				EmailConfirmed = true,
				PasswordHash = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92",
				SecurityStamp = "3b2a5234-dd4a-4473-98e5-9f782ab38e07",
				TwoFactorEnabled = true
			};

			RenterUser = new ApplicationUser()
			{
				FirstName = "Mash",
				LastName = "Burndead",
				UserName = "Mashle",
				NormalizedUserName = "MASH.BURNDEAD@ABV.BG",
				Email = "Mash.burndead@abv.bg",
				EmailConfirmed = true,
				PasswordHash = "6a95bbab63d587b596398c4bd7e91a132f24032d2007d107e5ea71967724b092",
				SecurityStamp = "ee4b3312-b039-49d2-b206-7274306f4852",
				TwoFactorEnabled = true
			};

			Agent = new Agent()
			{
				User = AgentUser,
				PhoneNumber = "0874319991"
			};

			House = new House()
			{
				Title = "Not Rented House",
				Address = "In your heart",
				Description = "A test non-rented house",
				ImageUrl = "https://www.jamesedition.com/stories/wp-content/uploads/2022/05/4-6.jpg",
				PricePerMonth = 1000,
				CreatedOn = DateTime.Now,
				IsActive = true,
				Category = new Category { Name = "Cottage"},
				Agent = Agent,

			};

			RentedHouse = new House()
			{
				Title = "Rented House",
				Address = "Closer than you think",
				Description = "A test rented house",
				ImageUrl = "https://i.pinimg.com/originals/a6/f5/85/a6f5850a77633c56e4e4ac4f867e3c00.jpg",
				PricePerMonth = 500,
				CreatedOn = DateTime.Now,
				IsActive = true,
				Category = new Category { Name = "Duplex" },
				Agent = Agent,
				Renter = RenterUser
			};

			dbContext.Users.Add(AgentUser);
			dbContext.Users.Add(RenterUser);
			dbContext.Agents.Add(Agent);
			dbContext.Houses.Add(House);
			dbContext.Houses.Add(RentedHouse);

			dbContext.SaveChanges();
		}
	}
}
