using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskBoardApp.ViewModels.Board;
using TaskBoardApp.ViewModels.Task;

namespace TaskBoardApp.Services.Interfaces
{
    public interface IBoardService
    {
        public Task<IEnumerable<BoardViewModel>> GetAllAsync();

        public Task<IEnumerable<TaskBoardModel>> GetBoardsAsync();

        public Task<bool> ExistsBoardWithIdAsync(int id);      
    }
}
