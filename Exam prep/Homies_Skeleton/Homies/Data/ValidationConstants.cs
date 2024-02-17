namespace Homies.Data
{
	public static class ValidationConstants
	{
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
	}
}
