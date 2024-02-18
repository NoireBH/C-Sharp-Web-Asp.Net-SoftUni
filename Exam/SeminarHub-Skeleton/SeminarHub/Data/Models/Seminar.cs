using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static SeminarHub.Data.ValidationConstants.Seminar;

namespace SeminarHub.Data.Models
{
	public class Seminar
	{
        public Seminar()
        {
            SeminarsParticipants = new HashSet<SeminarParticipant>();
        }

        [Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(TopicMaxLength)]
		public string Topic { get; set; } = null!;

		[Required]
		[MaxLength(LecturerMaxLength)]
		public string Lecturer { get; set; } = null!;

		[Required]
		[MaxLength(DetailsMaxLength)]
		public string Details { get; set; } = null!;

		[Required]
		public string OrganizerId { get; set; } = null!;

		[Required]
		[ForeignKey(nameof(OrganizerId))]
		public IdentityUser Organizer { get; set; } = null!;

		[Required]
		public DateTime DateAndTime { get; set; }

		[Range(DurationMinLength, DurationMaxLength)]
		public int? Duration {  get; set; }

		[Required]
		public int CategoryId { get; set; }

		[Required]
		[ForeignKey(nameof(CategoryId))]
		public Category Category { get; set; } = null!;

		public virtual ICollection<SeminarParticipant> SeminarsParticipants { get; set; } = null!;

	}
}
