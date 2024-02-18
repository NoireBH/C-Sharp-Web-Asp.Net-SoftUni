using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeminarHub.Data;
using SeminarHub.Data.Models;
using SeminarHub.Models.Category;
using SeminarHub.Models.Seminar;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using static SeminarHub.Data.ValidationConstants.GeneralConstants;

namespace SeminarHub.Controllers
{
    public class SeminarController : Controller
	{
		private readonly SeminarHubDbContext context;

        public SeminarController(SeminarHubDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
		public async Task<IActionResult> All()
		{
            SeminarViewModel[] seminars = await context.Seminars
                .AsNoTracking()
                .Select(s => new SeminarViewModel
                {
                    Id = s.Id,
                    Topic = s.Topic,
                    Lecturer = s.Lecturer,
                    Category = s.Category.Name,
                    DateAndTime = s.DateAndTime.ToString(DateTimeFormat),
                    Organizer = s.Organizer.UserName

                }).ToArrayAsync();

            return View(seminars);
		}

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            IEnumerable<CategoryVIewModel> categories = await GetCategories();

            SeminarFormViewModel model = new SeminarFormViewModel
            {
                Categories = categories
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(SeminarFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await GetCategories();

                return View(model);
            }

            Seminar seminar = new Seminar()
            {
                Topic = model.Topic,
                Lecturer = model.Lecturer,
                Details = model.Details,
                DateAndTime = model.DateAndTime,
                Duration = model.Duration,
                CategoryId = model.CategoryId,
                OrganizerId = GetCurrentUserId()
            };

            await context.Seminars.AddAsync(seminar);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var s = await context
                .Seminars
                .FindAsync(id);

            if (s == null)
            {
                return BadRequest();
            }

            if (s.OrganizerId != GetCurrentUserId())
            {
                return Unauthorized();
            }

            var formModel = new SeminarEditViewModel()
            {              
                Topic = s.Topic,
                Lecturer = s.Lecturer,
                Details = s.Details,
                DateAndTime = s.DateAndTime.ToString(),
                Duration = s.Duration,
                CategoryId = s.CategoryId,
                Categories = await GetCategories()
            };

            return View(formModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, SeminarFormViewModel model)
        {
            var s = await context.Seminars
                .FindAsync(id);

            if (s == null)
            {
                return BadRequest();
            }

            if (s.OrganizerId != GetCurrentUserId())
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await GetCategories();

                return View(model);
            }

            s.Topic = model.Topic;
            s.Lecturer = model.Lecturer;
            s.Details = model.Details;
            s.DateAndTime = model.DateAndTime;
            s.Duration = model.Duration;
            s.CategoryId = model.CategoryId;

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Join(int id)
        {
            var seminars = await context
                .Seminars
                .Where(s => s.Id == id)
                .Include(s => s.SeminarsParticipants)
                .FirstOrDefaultAsync();

            if (seminars == null)
            {
                return BadRequest();
            }

            string userId = GetCurrentUserId();

            if (!seminars.SeminarsParticipants.Any(p => p.ParticipantId == userId))
            {
                seminars.SeminarsParticipants.Add(new SeminarParticipant()
                {
                    SeminarId = seminars.Id,
                    ParticipantId = userId
                });

                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Joined));
            }

            return RedirectToAction(nameof(All));
            
        }

        [HttpGet]
        public async Task<IActionResult> Joined()
        {
            string userId = GetCurrentUserId();

            var model = await context.SeminarsParticipants
                .Where(sp => sp.ParticipantId == userId)
                .AsNoTracking()
                .Select(ep => new SeminarJoinedViewModel
                {
                    Id = ep.SeminarId,
                    Topic = ep.Seminar.Topic,
                    Lecturer = ep.Seminar.Lecturer,
                    DateAndTime = ep.Seminar.DateAndTime.ToString(DateTimeFormat),
                    Organizer = ep.Seminar.Organizer.UserName

                })
                .ToArrayAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            var seminar = await context.Seminars
                .Where(s => s.Id == id)
                .Include(s => s.SeminarsParticipants)
                .FirstOrDefaultAsync();

            if (seminar == null)
            {
                return BadRequest();
            }

            string userId = GetCurrentUserId();

            var sp = seminar.SeminarsParticipants.FirstOrDefault(ep => ep.ParticipantId == userId);

            if (sp == null)
            {
                return BadRequest();
            }

            seminar.SeminarsParticipants.Remove(sp);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Joined));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await context.Seminars
                .Where(s => s.Id == id)
                .AsNoTracking()
                .Select(s => new SeminarDetailsViewModel()
                {
                    Id = s.Id,
                    Topic = s.Topic,
                    DateAndTime = s.DateAndTime.ToString(DateTimeFormat),
                    Duration = s.Duration,
                    Lecturer = s.Lecturer,
                    Category = s.Category.Name,
                    Details = s.Details,
                    Organizer = s.Organizer.UserName
                })
                .FirstOrDefaultAsync();

            if (model == null)
            {
                return BadRequest();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var seminar = await context.Seminars.FindAsync(id);

            if (seminar == null)
            {
                return BadRequest();
            }

            string currentUser = GetCurrentUserId();

            if (currentUser != seminar.OrganizerId)
            {
                return Unauthorized();
            }

            context.Seminars.Remove(seminar);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var seminar = await context.Seminars
                .Where(s => s.Id == id)
                .Include(s => s.SeminarsParticipants)
                .FirstOrDefaultAsync();

            if (seminar == null)
            {
                return BadRequest();
            }

            string userId = GetCurrentUserId();

            if (userId != seminar.OrganizerId)
            {
                return Unauthorized();
            }

            SeminarDeleteViewModel seminarModel = new SeminarDeleteViewModel()
            {
                Id = seminar.Id ,
                DateAndTime = seminar.DateAndTime,
                Topic = seminar.Topic
            };

            return View(seminarModel);
        }

        private async Task<IEnumerable<CategoryVIewModel>> GetCategories()
        {
            CategoryVIewModel[] categories = await context.Categories
                .AsNoTracking()
                .Select(t => new CategoryVIewModel
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .ToArrayAsync();

            return categories;
        }

        private string GetCurrentUserId() => User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
    }
}
