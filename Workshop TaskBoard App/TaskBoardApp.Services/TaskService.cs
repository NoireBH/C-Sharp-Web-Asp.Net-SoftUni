using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskBoardApp.Data;
using TaskBoardApp.Services.Interfaces;
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

		public async Task AddAsync(string ownerId, TaskFormModel viewModel)
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
	}
}
