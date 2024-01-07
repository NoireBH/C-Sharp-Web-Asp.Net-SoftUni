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
		private static ApplicationUser AgentUser;
		private static ApplicationUser RenterUser;
		private static Agent Agent;

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

			dbContext.Add(AgentUser);
			dbContext.Add(RenterUser);
			dbContext.Add(Agent);

			dbContext.SaveChanges();
		}
	}
}
