using HouseRenting.Services.Data.Interfaces;
using HouseRenting.Web.Data;
using HouseRenting.Web.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectionString = builder.Configuration.GetConnectionString("SqlConnection")
	?? throw new InvalidOperationException("Connection string 'SqlConnection' not found.");

builder.Services.AddDbContext<HouseRentingDbContext>(options =>
	options.UseSqlServer(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices(typeof(IHouseService));

builder.Services.AddCors(options =>
{
	options.AddPolicy(name: "HouseRenting", builder =>
	{
		builder
		.WithOrigins("https://localhost:7107")
		.AllowAnyHeader()
		.AllowAnyMethod();
	});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("HouseRenting");

app.Run();
