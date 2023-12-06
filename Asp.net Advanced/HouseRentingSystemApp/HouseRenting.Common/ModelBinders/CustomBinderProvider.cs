using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRenting.Common.ModelBinders
{
	public class CustomBinderProvider : IModelBinderProvider
	{
		public IModelBinder? GetBinder(ModelBinderProviderContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException(nameof(context));
			}
			if (context.Metadata.ModelType == typeof(decimal)
				|| context.Metadata.ModelType == typeof(decimal))
			{
				return new DecimalModelBinder();
			}

			return null;
		}
	}
}
