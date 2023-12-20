using HouseRenting.Services.Data;
using HouseRenting.Services.Data.Interfaces;
using HouseRenting.Services.Data.Models.Statistics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HouseRenting.Web.Api.Controllers
{
	[Route("api/statistics")]
	[ApiController]
	public class StatisticsApiController : ControllerBase
	{
		private readonly IHouseService houseService;

		public StatisticsApiController(IHouseService houseService)
		{
			this.houseService = houseService;
		}

		[HttpGet]
		[Produces("application/json")]
		[ProducesResponseType(200, Type = typeof(StatisticServiceModel))]
		[ProducesResponseType(400)]
		public async Task<IActionResult> GetStatistics()
		{
			try
			{
				StatisticServiceModel statistics = await houseService.GetStatisticsAsync();

				return Ok(statistics);
			}
			catch (Exception)
			{
				return BadRequest();
			}

		}
	}
}
