using Microsoft.AspNetCore.Mvc;

namespace HouseRenting.Web.Areas.Admin.Controllers
{
	public class HomeController : BaseAdminController
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
