using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using TaskBoardApp.Models;
using TaskBoardApp.Services;
using TaskBoardApp.Services.Interfaces;
using TaskBoardApp.ViewModels.Home;

namespace TaskBoardApp.Controllers
{
    public class HomeController : Controller
    {
		private readonly IBoardService boardService;
		private readonly ITaskService taskService;
        private readonly IHomeService homeService;

		public HomeController(IBoardService boardService, ITaskService taskService, IHomeService homeService)
		{
			this.boardService = boardService;
			this.taskService = taskService;
            this.homeService = homeService;
		}

		[HttpGet]
        public async Task<IActionResult> Index()
        {
            var taskBoardNames = await homeService.GetBoardNames();

            var taskCounts = new List<HomeBoardModel>();

            foreach (var boardName in taskBoardNames)
            {
                var taskInBoard = taskService.GetTaskInBoardCount(boardName);
                taskCounts.Add(new HomeBoardModel()
                {
                    BoardName = boardName,
                    TasksCount = taskInBoard
                    
                });
            }

            var userTaskCount = 0;

            if (User.Identity.IsAuthenticated)
            {
                string currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                userTaskCount = taskService.GetUserTaskCount(currentUserId);
            }

            var homeViewModel = new HomeViewModel()
            {
                AllTasksCount = taskService.GetAllTasksCount(),
                BoardsWithTasksCount = taskCounts,
                UserTasksCount = userTaskCount
            };

            return View(homeViewModel);
        }
      
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}