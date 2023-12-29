using HouseRenting.Services.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRenting.Web.ViewModels.House
{
	using AutoMapper;
	using Data.Models;
	public class HouseAllViewModel : IMapFrom<House>, IHaveCustomMappings
	{
        public string Id { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Address { get; set; } = null!;

        [DisplayName("Image Link")]
        public string ImageUrl { get; set; } = null!;

        [DisplayName("Price Per Month")]
        public decimal PricePerMonth { get; set; }

        [DisplayName("Is Rented")]
        public bool IsRented { get; set; }

		public void CreateMappings(IProfileExpression configuration)
		{
			configuration.CreateMap<House, HouseAllViewModel>()
				.ForMember(h => h.IsRented, cfg => cfg
					.MapFrom(h => h.Renter != null));
		}
	}
}
