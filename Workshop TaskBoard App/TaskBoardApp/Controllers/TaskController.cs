using Microsoft.AspNetCore.Mvc;
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
    }
}
