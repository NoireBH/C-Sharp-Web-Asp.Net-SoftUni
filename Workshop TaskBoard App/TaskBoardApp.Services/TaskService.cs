using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskBoardApp.Data;
using TaskBoardApp.Data.Models;
using TaskBoardApp.Services.Interfaces;
using TaskBoardApp.ViewModels.Board;
using TaskBoardApp.ViewModels.Task;

namespace TaskBoardApp.Services
{
	public class TaskService : ITaskService
	{
		private readonly TaskBoardDbContext context;

		public TaskService(TaskBoardDbContext context)
		{
			this.context = context;
		}

		public async System.Threading.Tasks.Task AddAsync(string ownerId, TaskFormModel viewModel)
		{
			TaskBoardApp.Data.Models.Task task = new Data.Models.Task()
			{
				Title = viewModel.Title,
				Description = viewModel.Description,
				CreatedOn = DateTime.Now,
				BoardId = viewModel.BoardId,
				OwnerId = ownerId
			};

			await context.Tasks.AddAsync(task);
			await context.SaveChangesAsync();
		}

		public async Task<TaskFormModel> ReturnTaskFormModelAsync(int id)
		{
			var task = await context.Tasks.FindAsync(id);

			if (task == null)
			{
				return null;
			}

			TaskFormModel taskForm = new TaskFormModel()
			{
				Title = task.Title,
				Description = task.Description,
				BoardId = task.BoardId,
				Boards = await context
				.Boards
				.Select(b => new TaskBoardModel()
				{
					Id = b.Id,
					Name = b.Name
				})
				.ToArrayAsync()
			};					

			return taskForm;
		}

		public async Task<TaskDetailsViewModel> GetTaskDetails(int id)
		{
			var task = await context
				.Tasks
				.Where(t => t.Id == id)
				.Select(t => new TaskDetailsViewModel()
				{
					Id = t.Id,
					Title = t.Title,
					Description = t.Description,
					CreatedOn = t.CreatedOn.ToString("dd/MM/yyyy HH:mm"),
					Board = t.Board.Name,
					Owner = t.Owner.UserName
				})
				.FirstOrDefaultAsync();

			return task;
		}

		public async Task<Data.Models.Task> FindTaskAsync(int id)
		{
			var task = await context.Tasks.FindAsync(id);
			
			return task;
		}

		public async System.Threading.Tasks.Task EditTaskAsync(Data.Models.Task task, TaskFormModel taskModel)
		{

			task.Title = taskModel.Title;
			task.Description = taskModel.Description;
			task.BoardId = taskModel.BoardId;

			await context.SaveChangesAsync();
		}

		public async System.Threading.Tasks.Task Delete(Data.Models.Task task)
		{
			context.Tasks.Remove(task);
			await context.SaveChangesAsync();
		}

		public int GetTaskInBoardCount(object boardName)
		{
			var tasksInBoard = context
				.Tasks
				.Where(t => t.Board.Name == boardName).Count();

			return tasksInBoard;
		}

		public int GetUserTaskCount(string userId)
		{
			var taskCount = context
				.Tasks
				.Where(t => t.OwnerId == userId).Count();

			return taskCount;
		}

		public int GetAllTasksCount()
		{
			return context.Tasks.Count();
		}
	}
}
