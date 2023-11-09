using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumApp.Data.Models
{
    public class Post
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public string Content { get; set; } = null!;
    }
}
