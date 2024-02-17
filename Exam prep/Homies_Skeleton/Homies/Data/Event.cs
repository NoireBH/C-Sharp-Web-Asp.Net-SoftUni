using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Homies.Data.ValidationConstants.Event;
namespace Homies.Data.Models
{
	public class Event
	{
        public Event()
        {
			EventsParticipants = new HashSet<EventParticipant>();

		}

        [Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(NameMax)]
		public string Name { get; set; } = null!;

		[Required]
		[MaxLength(DescriptionMax)]
		public string Description { get; set; } = null!;

		[Required]
		public int OrganizerId { get; set; }

		[Required]
		[ForeignKey(nameof(OrganizerId))]
		public IdentityUser Organizer { get; set; } = null!;

		[Required]
		public DateTime CreatedOn {  get; set; }

		[Required]
		public DateTime Start {  get; set; }

		[Required]
		public DateTime End {  get; set; }

		[Required]
		public int TypeId { get; set; }

		[Required]
		[ForeignKey(nameof(TypeId))]
		public Type Type { get; set; } = null!;

		public virtual ICollection<EventParticipant> EventsParticipants { get; set; }
	}
}
