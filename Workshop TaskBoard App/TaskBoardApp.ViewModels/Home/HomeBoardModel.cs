﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskBoardApp.ViewModels.Home
{
	public class HomeBoardModel
	{
		public string BoardName { get; set; } = null!;

		public int TasksCount { get; set; }
	}
}
