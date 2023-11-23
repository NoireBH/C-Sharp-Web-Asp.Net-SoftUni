using Library.Interfaces;
using Library.Models;
using Library.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Library.Controllers
{
	[Authorize]
	public class BookController : Controller
	{
		private readonly IBookService bookService;

		public BookController(IBookService bookService)
		{
			this.bookService = bookService;
		}

		public async Task<IActionResult> All()
		{
			var allBooks = await bookService.GetAllBooksAsync();

			return View(allBooks);
		}

		public async Task<IActionResult> Mine()
		{
			var myBooks = await bookService.GetMyBooksAsync(GetUserId());

			return View(myBooks);
		}

		public async Task<IActionResult> AddToCollection(int id)
		{
			var myBook = await bookService.GetBookByIdAsync(id);

			if (myBook == null)
			{
				return RedirectToAction(nameof(All));
			}

			string userId = GetUserId();

			await bookService.AddBookToCollection(userId, myBook);

			return RedirectToAction(nameof(All));
		}

		public async Task<IActionResult> RemoveFromCollection(int id)
		{
			var book = await bookService.GetBookByIdAsync(id);

			if (book == null)
			{
				return RedirectToAction(nameof(Mine));
			}

			string userId = GetUserId();

			await bookService.RemoveBookFromCollection(userId, book);

			return RedirectToAction(nameof(Mine));
		}

		[HttpGet]
		public async Task<IActionResult> Add()
		{
			AddBookViewModel model = await bookService.GetNewAddBookViewModelAsync();

			return View(model);
		}

        [HttpPost]
        public async Task<IActionResult> Add(AddBookViewModel bookModel)
        {
			if (!ModelState.IsValid)
			{
				return View(bookModel);
			}

			await bookService.AddBookAsync(bookModel);

			return RedirectToAction(nameof(All));
        }

        private string GetUserId()
		{
			//if no user with such id exists Mine will return an empty list which is good
			string id = string.Empty;

			if (User != null)
			{
				id = User.FindFirstValue(ClaimTypes.NameIdentifier);
			}

			return id;
		}
	}
}
