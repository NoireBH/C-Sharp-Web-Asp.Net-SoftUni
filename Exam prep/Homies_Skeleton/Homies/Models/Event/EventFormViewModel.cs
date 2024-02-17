using Homies.Models.Type;
using System.ComponentModel.DataAnnotations;
using static Homies.Data.ValidationConstants.Event;
using static Homies.Data.ValidationConstants.ErrorMessages;

namespace Homies.Models.Event
{
	public class EventFormViewModel
	{
        public EventFormViewModel()
        {
            Types = new HashSet<TypeViewModel>();
        }

		[Required(ErrorMessage = FieldRequiredError)]
		[StringLength(NameMax, MinimumLength = NameMin,
			ErrorMessage = FieldLengthError)]
        public string Name { get; set; } = null!;

		[Required(ErrorMessage = FieldRequiredError)]
		[StringLength(DescriptionMax, MinimumLength = DescriptionMin,
			ErrorMessage = FieldLengthError)]
		public string Description { get; set; } = null!;

		[Required(ErrorMessage = FieldRequiredError)]
		public string Start { get; set; } = null!;

		[Required(ErrorMessage = FieldRequiredError)]
		public string End { get; set; } = null!;

		[Required(ErrorMessage = FieldRequiredError)]
		public int TypeId { get; set; }

		public virtual IEnumerable<TypeViewModel> Types { get; set; }
	}
}
