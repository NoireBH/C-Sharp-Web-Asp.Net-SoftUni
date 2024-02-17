namespace Homies.Data
{
	public static class ValidationConstants
	{
		public const string DateTimeFormat = "yyyy-MM-dd H:mm";

		public class Event
		{
			public const int NameMin = 5;
			public const int NameMax = 20;

			public const int DescriptionMin = 15;
			public const int DescriptionMax = 150;
		}

		public class Type
		{
			public const int NameMin = 5;
			public const int NameMax = 15;
		}

		public class ErrorMessages
		{
			public const string FieldRequiredError = "The field {0} is required!";
			public const string FieldLengthError = "The field {0} must be between {2} and {1} characters long!";
		}
	}
}
