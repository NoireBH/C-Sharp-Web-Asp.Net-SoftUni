using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumApp.Common.Validations
{
	public static class EntityValidations
	{
		public static class Post
		{
			public const int TitleMinLength = 10;
			public const int TitleMaxLength = 50;
			public const int ContentMinLength = 10;
			public const int ContentMaxLength = 1500;
		}

		
	}
}
