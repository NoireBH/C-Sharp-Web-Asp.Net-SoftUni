using HouseRenting.Services.Mapping;

namespace HouseRenting.Web.ViewModels.User
{
	using AutoMapper;
	using Data.Models;
	public class UserViewModel : IMapFrom<Agent>, IMapFrom<ApplicationUser>, IHaveCustomMappings
	{
		public string Id { get; set; } = null!;

		public string Email { get; set; } = null!;

		public string FullName { get; set; } = null!;

		public string PhoneNumber { get; set; } = null!;

		public void CreateMappings(IProfileExpression configuration)
		{
			configuration.CreateMap<Agent, UserViewModel>()
				.ForMember(d => d.Email, opt => opt.MapFrom(s => s.User.Email))
				.ForMember(d => d.FullName, opt =>
					opt.MapFrom(s => s.User.FirstName + " " + s.User.LastName));

			configuration.CreateMap<ApplicationUser, UserViewModel>()
				.ForMember(d => d.PhoneNumber, opt => opt.MapFrom(s => string.Empty))
				.ForMember(d => d.FullName, opt =>
					opt.MapFrom(s => s.FirstName + " " + s.LastName));
		}
	}
}
