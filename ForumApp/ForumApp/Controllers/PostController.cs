using ForumApp.ViewModels.Post;
using ForumApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ForumApp.Controllers
{
	public class PostController : Controller
	{
		private readonly IPostService postService;

		public PostController(IPostService postService)
		{
			this.postService = postService;
		}

		public async Task<IActionResult> All()
		{
			IEnumerable<PostListViewModel> posts = await postService.ListAllAsync();

			return View(posts);
		}

		public IActionResult Add()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Add(PostFormViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			try
			{
				await postService.AddPostAsync(model);
			}
			catch (Exception)
			{
				ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add your post!");

				return View(model);
			}

			return RedirectToAction("All", "Post");
		}

		[HttpPost]
		public async Task<IActionResult> Delete(string id)
		{
			try
			{
				await postService.DeleteByIdAsync(id);
			}
			catch (Exception)
			{

			}

			return RedirectToAction("All", "Post");
		}

		public async Task<IActionResult> Edit(string id)
		{
			try
			{
				PostFormViewModel postModel =
					await postService.GetForEditOrDeleteByIdAsync(id);

				return View(postModel);
			}
			catch (Exception)
			{
				return RedirectToAction("All", "Post");
			}

		}

		[HttpPost]
		public async Task<IActionResult> Edit(string id, PostFormViewModel postModel)
		{
			if (!ModelState.IsValid)
			{
				return View(postModel);
			}

			try
			{
				await postService.EditByIdAsync(id, postModel);
			}
			catch (Exception)
			{
				ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to update your post!");

				return View(postModel);
			}

			return RedirectToAction("All", "Post");
		}
	}
}