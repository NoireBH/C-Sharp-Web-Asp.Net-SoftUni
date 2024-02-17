using Homies.Data;
using Homies.Models.Event;
using Homies.Models.Type;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Claims;

namespace Homies.Controllers
{
	public class EventController : Controller
	{
		private HomiesDbContext context;
		public EventController(HomiesDbContext context)
		{
			this.context = context;
		}

		[HttpGet]
		public IActionResult All()
		{
			var events = context
				.Events
				.AsNoTracking()
				.Select(e => new EventViewModel
				{
					Id = e.Id,
					Name = e.Name,
					Start = e.Start.ToString(ValidationConstants.DateTimeFormat),
					Type = e.Type.Name,
					Organiser = e.Organizer.UserName
				})	
				.ToArray();

			return View(events);
		}

		[HttpGet]
		public async Task<IActionResult> Add()
		{
			var formModel = new EventFormViewModel()
			{
				Types = await GetEventTypes()
			};

			return View(formModel);
		}

		[HttpPost]
		public async Task<IActionResult> Add(EventFormViewModel model)
		{
			DateTime start = DateTime.Now;
			DateTime end = DateTime.Now;

			if (!DateTime.TryParseExact(
				model.Start,
				ValidationConstants.DateTimeFormat,
				CultureInfo.InvariantCulture,
				DateTimeStyles.None,
				out start))
			{
				ModelState
					.AddModelError(nameof(model.Start), $"Invalid date! Format must be: {ValidationConstants.DateTimeFormat}!");
			}

			if (!DateTime.TryParseExact(
				model.End,
				ValidationConstants.DateTimeFormat,
				CultureInfo.InvariantCulture,
				DateTimeStyles.None,
				out end))
			{
				ModelState
					.AddModelError(nameof(model.End), $"Invalid date! Format must be: {ValidationConstants.DateTimeFormat} !");
			}

			if (!ModelState.IsValid)
			{
				model.Types = await GetEventTypes();

				return View(model);
			}

			var newEvent = new Event()
			{
				CreatedOn = DateTime.Now,
				Description = model.Description,
				Name = model.Name,
				OrganiserId = GetCurrentUserId(),
				TypeId = model.TypeId,
				Start = start,
				End = end
			};

			await context.Events.AddAsync(newEvent);
			await context.SaveChangesAsync();

			return RedirectToAction(nameof(All));
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			Event? EventToEdit = await context
				.Events
				.FindAsync(id);

			if (EventToEdit == null)
			{
				return BadRequest();
			}

			if (EventToEdit.OrganiserId != GetCurrentUserId())
			{
				return Unauthorized();
			}

			var formModel = new EventFormViewModel()
			{
				Description = EventToEdit.Description,
				Name = EventToEdit.Name,
				End = EventToEdit.End.ToString(ValidationConstants.DateTimeFormat),
				Start = EventToEdit.Start.ToString(ValidationConstants.DateTimeFormat),
				TypeId = EventToEdit.TypeId,
				Types = await GetEventTypes()
			};

			return View(formModel);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, EventFormViewModel model)
		{
			var eventToEdit = await context.Events
				.FindAsync(id);

			if (eventToEdit == null)
			{
				return BadRequest();
			}

			if (eventToEdit.OrganiserId != GetCurrentUserId())
			{
				return Unauthorized();
			}

			DateTime start = DateTime.Now;
			DateTime end = DateTime.Now;


			if (!DateTime.TryParseExact(
				model.Start,
				ValidationConstants.DateTimeFormat,
				CultureInfo.InvariantCulture,
				DateTimeStyles.None,
				out start))
			{
				ModelState
					.AddModelError(nameof(model.Start), $"Invalid date! Format must be: {ValidationConstants.DateTimeFormat}!");
			}

			if (!DateTime.TryParseExact(
				model.End,
				ValidationConstants.DateTimeFormat,
				CultureInfo.InvariantCulture,
				DateTimeStyles.None,
				out end))
			{
				ModelState
					.AddModelError(nameof(model.End), $"Invalid date! Format must be: {ValidationConstants.DateTimeFormat}!");
			}

			if (!ModelState.IsValid)
			{
				model.Types = await GetEventTypes();

				return View(model);
			}

			eventToEdit.Name = model.Name;
			eventToEdit.Description = model.Description;
			eventToEdit.Start = start;
			eventToEdit.End = end;
			eventToEdit.TypeId = model.TypeId;

			await context.SaveChangesAsync();

			return RedirectToAction(nameof(All));
		}


		private async Task<IEnumerable<TypeViewModel>> GetEventTypes()
		{
			return await context.Types
				.AsNoTracking()
				.Select(t => new TypeViewModel
				{
					Id = t.Id,
					Name = t.Name
				})
				.ToArrayAsync();
		}

		private string GetCurrentUserId() => User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
	}
}
