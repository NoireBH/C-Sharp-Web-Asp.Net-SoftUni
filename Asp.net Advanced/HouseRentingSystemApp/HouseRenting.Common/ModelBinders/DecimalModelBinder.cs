using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRenting.Common.ModelBinders
{
	public class DecimalModelBinder : IModelBinder
	{
		public Task BindModelAsync(ModelBindingContext bindingContext)
		{

			if (bindingContext == null)
			{
				throw new ArgumentNullException(nameof(bindingContext));
			}

			var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
			string value = valueProviderResult.FirstValue;
			bool bindingSucceeded = false;
			decimal parsedValue = 0m;

			if (valueProviderResult != ValueProviderResult.None &&
				!string.IsNullOrWhiteSpace(value))
			{
				try
				{
					value = value.Replace
						(",", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
					value = value.Replace
						(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);

					parsedValue = Convert.ToDecimal(value);
					bindingSucceeded = true;
				}
				catch (Exception e)
				{
					bindingContext.ModelState
						.TryAddModelError(bindingContext.ModelName, e, bindingContext.ModelMetadata);
				}

				if (bindingSucceeded)
				{
					bindingContext.Result = ModelBindingResult.Success(parsedValue);
				}		
			}

			return Task.CompletedTask;




			//Its better to use convert than tryparse because tryparse needs a console?
			//if (!decimal.TryParse(value, out myValue))
			//{
			//	// Error
			//	bindingContext.ModelState.TryAddModelError(
			//							bindingContext.ModelName,
			//							"Could not parse MyValue.");
			//	return Task.CompletedTask;
			//}

			//bindingContext.Result = ModelBindingResult.Success(myValue);
			//return Task.CompletedTask;
		}
	}
}
