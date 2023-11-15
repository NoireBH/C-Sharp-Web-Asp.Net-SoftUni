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
	}
}
