using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskBoardApp.Data;
using TaskBoardApp.Services.Interfaces;

namespace TaskBoardApp.Services
{
	public class HomeService : IHomeService
    {
		private readonly TaskBoardDbContext context;

		public HomeService(TaskBoardDbContext context)
		{
			this.context = context;
		}

		public async Task<List<string>> GetBoardNames()
		{
			var taskBoards = await context
				.Boards
				.Select(b => b.Name)
				.Distinct()
				.ToListAsync();

			return taskBoards;
		}
	}
}
