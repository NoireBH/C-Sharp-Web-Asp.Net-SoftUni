using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskBoardApp.Extensions;
using TaskBoardApp.Services.Interfaces;
using TaskBoardApp.ViewModels.Task;
using Task = TaskBoardApp.Data.Models.Task;

namespace TaskBoardApp.Controllers
{
    public class TaskController : Controller
    {
        private readonly IBoardService boardService;
		private readonly ITaskService taskService;

		public TaskController(IBoardService boardService, ITaskService taskService)
        {
            this.boardService = boardService;
			this.taskService = taskService;
		}

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            TaskFormModel taskFormModel = new TaskFormModel()
            {
                Boards = await boardService.GetBoardsAsync()
            };

            return View(taskFormModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Boards = await boardService.GetBoardsAsync();

                return View(model);
            }

            bool boardExists = await boardService.ExistsBoardWithIdAsync(model.BoardId);

			if (!boardExists)
            {
				model.Boards = await boardService.GetBoardsAsync();
				ModelState.AddModelError(nameof(model.BoardId), "This board does not exist!");

                return View(model);
            }

            string currentUserId = User.GetId();

            await taskService.AddAsync(currentUserId, model);

            return RedirectToAction("All", "Board");

        }

		[HttpGet]
		public async Task<IActionResult> Details(int id)
        {
            var task = await taskService.GetTaskDetails(id);

            if (task == null)
            {
                return BadRequest();
            }

            return View(task);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var task = await taskService.FindTaskAsync(id);

            if (task == null)
            {
                return BadRequest();
            }

			string currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);

			if (currentUser != task.OwnerId)
			{
				return Unauthorized();
			}

			TaskFormModel taskFormModel = new TaskFormModel()
			{
				Title = task.Title,
				Description = task.Description,
				BoardId = task.BoardId,
				Boards = await boardService.GetBoardsAsync()
			};

			return View(taskFormModel);


		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, TaskFormModel taskModel)
		{
			var task = await taskService.FindTaskAsync(id);

			if (task == null)
			{
				return BadRequest();
			}

            string currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (currentUser != task.OwnerId)
            {
                return Unauthorized();
            }

            var boards = await boardService.GetBoardsAsync();

            if (!boards.Any(b => b.Id == taskModel.BoardId))
            {
                ModelState.AddModelError(nameof(taskModel.BoardId), "Board does not exist.");
            }

            if (!ModelState.IsValid)
            {
                taskModel.Boards = await boardService.GetBoardsAsync();

                return View(taskModel);
            }

            await taskService.EditTaskAsync(task, taskModel);

            return RedirectToAction("All", "Board");
		}

        public async Task<IActionResult> Delete(int id)
        {
            var task = await taskService.FindTaskAsync(id);

            if (task == null)
            {
                return BadRequest();
            }

			string currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);

			if (currentUser != task.OwnerId)
			{
				return Unauthorized();
			}

            TaskViewModel taskModel = new TaskViewModel()
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description
            };

            return View(taskModel);

		}

        [HttpPost]
		public async Task<IActionResult> Delete(TaskViewModel taskModel)
		{
			var task = await taskService.FindTaskAsync(taskModel.Id);

			if (task == null)
			{
				return BadRequest();
			}

			string currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);

			if (currentUser != task.OwnerId)
			{
				return Unauthorized();
			}

            await taskService.Delete(task);

			return RedirectToAction("All", "Board");

		}
	}
}
