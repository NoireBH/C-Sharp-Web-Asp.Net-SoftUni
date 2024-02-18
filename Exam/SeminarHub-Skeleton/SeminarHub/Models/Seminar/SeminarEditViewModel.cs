using SeminarHub.Models.Category;
using System.ComponentModel.DataAnnotations;
using static SeminarHub.Data.ValidationConstants.Seminar;

namespace SeminarHub.Models.Seminar
{
    public class SeminarEditViewModel
    {
        public SeminarEditViewModel()
        {
            Categories = new HashSet<CategoryVIewModel>();
        }

        [Required]
        [StringLength(TopicMaxLength, MinimumLength = TopicMinLength)]
        public string Topic { get; set; } = null!;

        [Required]
        [StringLength(LecturerMaxLength, MinimumLength = LecturerMinLength)]
        public string Lecturer { get; set; } = null!;

        [Required]
        [StringLength(DetailsMaxLength, MinimumLength = DetailsMinLength)]
        public string Details { get; set; } = null!;

        [Required]
        public string DateAndTime { get; set; } = null!;

        [Range(DurationMinLength, DurationMaxLength)]
        public int? Duration { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryVIewModel> Categories { get; set; } = null!;
    }

}
