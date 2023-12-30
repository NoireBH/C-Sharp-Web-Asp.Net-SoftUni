using HouseRenting.Services.Mapping;

namespace HouseRenting.Web.ViewModels.User
{
	using AutoMapper;
	using Data.Models;
	public class UserViewModel : IMapFrom<Agent>, IHaveCustomMappings
	{
		public string Email { get; set; } = null!;

		public string FullName { get; set; } = null!;

		public string PhoneticName { get; set; } = null!;

		public void CreateMappings(IProfileExpression configuration)
		{
			configuration.CreateMap<Agent, UserViewModel>()
				.ForMember(d => d.Email, opt => opt.MapFrom(s => s.User.Email))
				.ForMember(d => d.FullName, opt =>
					opt.MapFrom(s => s.User.FirstName + " " + s.User.LastName));
		}
	}
}
