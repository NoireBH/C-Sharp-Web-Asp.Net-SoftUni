﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskBoardApp.Data;
using TaskBoardApp.Services.Interfaces;
using TaskBoardApp.ViewModels.Board;
using TaskBoardApp.ViewModels.Task;

namespace TaskBoardApp.Services
{
    public class BoardService : IBoardService
    {
        private readonly TaskBoardDbContext context;

        public BoardService(TaskBoardDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<BoardViewModel>> GetAllAsync()
        {
            var allBoards = await context
                .Boards
                .Select(b => new BoardViewModel()
                {
                    Name = b.Name,
                    Tasks = b.Tasks.Select(t => new TaskViewModel()
                    {
                        Id = t.Id,
                        Title = t.Title,
                        Description = t.Description,
                        Owner = t.Owner.UserName
                    })
                    .ToArray()

                })
                .ToArrayAsync();

            return allBoards;
        }
    }
}
