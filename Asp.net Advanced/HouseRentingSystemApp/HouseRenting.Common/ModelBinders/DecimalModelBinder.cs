using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRenting.Common.ModelBinders
{
	public class DecimalModelBinder : IModelBinder
	{
		public Task BindModelAsync(ModelBindingContext bindingContext)
		{
			var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

			if (valueProviderResult == null)
			{
				return Task.CompletedTask;
			}

			string value = valueProviderResult.FirstValue;

			if (string.IsNullOrEmpty(value))
			{
				return Task.CompletedTask;
			}

			value = value.Replace(",", ".").Trim();

			decimal myValue = 0;
			if (!decimal.TryParse(value, out myValue))
			{
				// Error
				bindingContext.ModelState.TryAddModelError(
										bindingContext.ModelName,
										"Could not parse MyValue.");
				return Task.CompletedTask;
			}

			bindingContext.Result = ModelBindingResult.Success(myValue);
			return Task.CompletedTask;
		}
	}
}
