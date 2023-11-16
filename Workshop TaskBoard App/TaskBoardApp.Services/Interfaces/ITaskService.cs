using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskBoardApp.ViewModels.Task;

namespace TaskBoardApp.Services.Interfaces
{
	public interface ITaskService
	{
		public Task AddAsync(string ownerId, TaskFormModel viewModel);

		public Task<TaskDetailsViewModel> GetTaskDetails(int id);

		public Task<TaskFormModel> ReturnTaskFormModelAsync(int id);

		public Task<Data.Models.Task> FindTaskAsync(int id);

		public System.Threading.Tasks.Task EditTaskAsync(Data.Models.Task task, TaskFormModel taskModel);

		public Task Delete(Data.Models.Task task);
	}
}
