using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskBoardApp.Common.ValidationConstants;
using TaskBoardApp.ViewModels.Board;

namespace TaskBoardApp.ViewModels.Task
{
    public class TaskFormModel
    {
        [Required]
        [StringLength(ValidationConstants.Task.TaskMaxTitle, MinimumLength = ValidationConstants.Task.TaskMinTitle,
            ErrorMessage = "Title should be at least {2} characters long.")]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(ValidationConstants.Task.TaskMaxDescription, MinimumLength = ValidationConstants.Task.TaskMinDescription,
            ErrorMessage = "Description should be at least {2} characters long.")]
        public string Description { get; set; } = null!;

        [Required]
        [Display(Name = "Board")]
        public int BoardId { get; set; }

        public IEnumerable<TaskBoardModel>? Boards { get; set; }
    }
}
