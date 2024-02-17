using System.ComponentModel.DataAnnotations;
using static Homies.Data.ValidationConstants.Type;

namespace Homies.Data
{
    public class Type
    {
        public Type()
        {
            Events = new HashSet<Event>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMax)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Event> Events { get; set; }
    }
}
