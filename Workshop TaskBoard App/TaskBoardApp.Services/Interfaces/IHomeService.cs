using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskBoardApp.Services.Interfaces
{
	public interface IHomeService
	{
		public Task<List<string>> GetBoardNames();
	}
}
