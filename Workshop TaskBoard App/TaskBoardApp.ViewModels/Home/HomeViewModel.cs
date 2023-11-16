using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskBoardApp.ViewModels.Home
{
	public class HomeViewModel
	{
		public int AllTasksCount {  get; set; }

		public List<HomeBoardModel> BoardsWithTasksCount { get; set; } = null!;

		public int UserTasksCount { get; set; }
	}
}
