using HouseRenting.Web.ViewModels.Agent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRenting.Web.ViewModels.House
{
	public class HouseDetailsViewModel : HouseAllViewModel
	{
		public string Description { get; set; } = null!;

		public string Category { get; set; } = null!;

		public AgentDetailsViewModel Agent { get; set; } = null!;
	}
}
