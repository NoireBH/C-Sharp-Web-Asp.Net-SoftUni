using Library.ValidationConstants;
using System.ComponentModel.DataAnnotations;

namespace Library.Data.Entities
{
    public class ApplicationUser 
    {
        public ApplicationUser()
        {
            ApplicationUsersBooks = new HashSet<ApplicationUserBook>();
        }

        [Key]
        public string Id { get; set; } = null!;

        [Required]
        [StringLength(EntityValidations.ApplicationUser.UserNameMaxLength)]
        public string UserName { get; set; } = null!;

        [Required]
        [StringLength(EntityValidations.ApplicationUser.EmailMaxLength)]

        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        public ICollection<ApplicationUserBook> ApplicationUsersBooks { get; set; }
    }
}
