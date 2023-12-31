using HouseRenting.Data.Models;
using HouseRenting.Web.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using static HouseRenting.Common.GeneralConstants.AdminUser;

namespace HouseRenting.Web.Infrastructure.Extensions
{
	public static class WebApplicationBuilderExtensions
	{
		public static void AddApplicationServices(this IServiceCollection services, Type serviceType)
		{
			Assembly? serviceAssembly = Assembly.GetAssembly(serviceType);

			if (serviceAssembly == null)
			{
				throw new InvalidOperationException("Invalid service type provided!");
			}

			Type[] serviceTypes = serviceAssembly
				.GetTypes()
				.Where(t => t.Name.EndsWith("Service") && !t.IsInterface)
				.ToArray();

			foreach (var service in serviceTypes)
			{
				Type? interfaceType = service
									.GetInterface($"I{service.Name}");

				if (interfaceType == null)
				{
					throw new InvalidOperationException(
						$"No interface exists for the service: {service.Name}");
				}

				services.AddScoped(interfaceType, service);
			}
		}

		public static IApplicationBuilder SeedAdministrator(this IApplicationBuilder app, string email)
		{
			using IServiceScope scopedServices = app.ApplicationServices.CreateScope();
			IServiceProvider serviceProvider = scopedServices.ServiceProvider;

			var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
			var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

			Task
				.Run(async () =>
				{
					if (await roleManager.RoleExistsAsync(AdminRoleName))
					{
						return;
					}

					var role = new IdentityRole<Guid>(AdminRoleName);
					await roleManager.CreateAsync(role);

					var adminUser = await userManager.FindByEmailAsync(email);
					await userManager.AddToRoleAsync(adminUser, AdminRoleName);
				})
				.GetAwaiter()
				.GetResult();

			return app;
		}

		public static IApplicationBuilder EnableOnlineUsersChecker(this IApplicationBuilder app)
		{
			return app.UseMiddleware<OnlineUsersMiddleware>();
		}
	}
}
