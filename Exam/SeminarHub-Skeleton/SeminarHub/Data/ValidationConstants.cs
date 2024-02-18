namespace SeminarHub.Data
{
	public static class ValidationConstants
	{
		public class GeneralConstants
		{
            public const string DateTimeFormat = "dd/MM/yyyy HH:mm";
        }	

		public class Seminar
		{
			public const int TopicMinLength = 3;
			public const int TopicMaxLength = 100;

			public const int LecturerMinLength = 5;
			public const int LecturerMaxLength = 60;

			public const int DetailsMinLength = 10;
			public const int DetailsMaxLength = 500;

			public const int DurationMinLength = 30;
			public const int DurationMaxLength = 180;
		}

		public class Category
		{
			public const int NameMinLength = 3;
			public const int NameMaxLength = 50;
		}
	}
}
