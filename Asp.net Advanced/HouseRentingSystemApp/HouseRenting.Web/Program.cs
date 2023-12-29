using HouseRenting.Common.ModelBinders;
using HouseRenting.Data.Models;
using HouseRenting.Services.Data.Interfaces;
using HouseRenting.Services.Mapping;
using HouseRenting.Web.Data;
using HouseRenting.Web.Infrastructure.Extensions;
using HouseRenting.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using static HouseRenting.Common.GeneralConstants.AdminUser;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("SqlConnection")
	?? throw new InvalidOperationException("Connection string 'SqlConnection' not found.");

builder.Services.AddDbContext<HouseRentingDbContext>(options =>
	options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddMvc((options) =>
{
	options.ModelBinderProviders.Insert(0, new CustomBinderProvider());
	options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
});

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
	options.SignIn.RequireConfirmedAccount =
		builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");
	options.Password.RequireLowercase =
		builder.Configuration.GetValue<bool>("Identity:Password:RequireLowercase");
	options.Password.RequireUppercase =
		builder.Configuration.GetValue<bool>("Identity:Password:RequireUppercase");
	options.Password.RequireNonAlphanumeric =
		builder.Configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumeric");
	options.Password.RequiredLength =
		builder.Configuration.GetValue<int>("Identity:Password:RequiredLength");
})
 .AddRoles<IdentityRole<Guid>>()
 .AddEntityFrameworkStores<HouseRentingDbContext>();

builder.Services.AddApplicationServices(typeof(IHouseService));

builder.Services.AddControllersWithViews();

WebApplication app = builder.Build();

AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);


if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
	app.UseDeveloperExceptionPage();
}
else
{
	app.UseExceptionHandler("/Home/Error/500");
	app.UseStatusCodePagesWithRedirects("/Home/Error?statusCode={0}");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.SeedAdministrator(AdminEmail);

app.MapDefaultControllerRoute();
app.MapRazorPages();

app.Run();
