using HouseRenting.Services.Data.Interfaces;
using HouseRenting.Web.ViewModels.Rent;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace HouseRenting.Web.Areas.Admin.Controllers
{
	using static Common.GeneralConstants;
	public class RentController : BaseAdminController
	{
		private readonly IRentService rentService;
        private readonly IMemoryCache cache;


        public RentController(IRentService rentService, IMemoryCache cache)
        {
            this.rentService = rentService;
            this.cache = cache;
        }

        [Route("Rent/All")]
        public async Task<IActionResult> All()
        {
            var rents = cache.Get<IEnumerable<RentViewModel>>(RentsCacheKey);

            if (rents == null)
            {
                rents = await rentService.GetAllRentsAsync();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

                cache.Set(RentsCacheKey, rents, cacheOptions);
            }

            return View(rents);
        }

    }
}
