namespace Library.ValidationConstants
{
	public static class EntityValidations
	{
		public class ApplicationUser
		{
			public const int UserNameMinLength = 5;
			public const int UserNameMaxLength = 20;

			public const int EmailMinLength = 10;
			public const int EmailMaxLength = 60;

			public const int PasswordMinLength = 5;
			public const int PasswordMaxLength = 20;

		}

		public class Book
		{
			public const int TitleMinLength = 10;
			public const int TitleMaxLength = 50;

			public const int AuthorMinLength = 5;
			public const int AuthorMaxLength = 50;

			public const int DescriptionMinLength = 5;
			public const int DescriptionMaxLength = 5000;

			public const double RatingMinLength = 0;
			public const double RatingMaxLength = 10;
		}

		public class Category
		{
			public const int CategoryNameMinLength = 5;
			public const int CategoryNameMaxLength = 50;
		}
	}
}
