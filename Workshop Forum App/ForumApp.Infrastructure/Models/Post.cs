using ForumApp.Core.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumApp.Infrastructure.Models
{
	public class Post
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(ValidationConstants.postTitleMaxLength, MinimumLength = ValidationConstants.postTitleMinLenght)]
		public string Title { get; set; } = null!;

		[Required]
		[StringLength(ValidationConstants.postContentMaxLength, MinimumLength = ValidationConstants.postContentMinLength)]
		public string Content { get; set; } = null!;

	}
}
