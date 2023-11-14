using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskBoardApp.ViewModels.Board;

namespace TaskBoardApp.Services.Interfaces
{
    public interface IBoardService
    {
        public Task<IEnumerable<BoardViewModel>> GetAllAsync();
    }
}
