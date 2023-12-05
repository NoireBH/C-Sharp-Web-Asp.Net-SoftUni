using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HouseRenting.Common.EntityValidationConstants.Agent;

namespace HouseRenting.Web.ViewModels.Agent
{
	public class BecomeAgentFormModel
	{
		[Required]
		[Phone]
		[Display(Name = "Phone Number")]
		[StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
		public string PhoneNumber { get; set; } = null!;
    }
}
