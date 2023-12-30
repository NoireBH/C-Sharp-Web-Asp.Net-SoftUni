using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouseRenting.Web.Areas.Admin.Controllers
{
	using static Common.GeneralConstants.AdminUser;

	[Authorize(Roles = AdminRoleName)]
	[Area(AdminAreaName)]
	public class BaseAdminController : Controller
	{
		
	}
}
