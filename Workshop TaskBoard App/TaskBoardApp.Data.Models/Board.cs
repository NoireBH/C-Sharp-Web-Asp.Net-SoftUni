using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskBoardApp.Common.ValidationConstants;

namespace TaskBoardApp.Data.Models
{
    public class Board
    {
        public Board()
        {
            Tasks = new HashSet<Task>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.Board.BoardMaxName)]
        public string Name { get; set; } = null!;

        public ICollection<Task> Tasks { get; set; }
    }
}
