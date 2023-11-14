using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskBoardApp.Common.ValidationConstants
{
    public static class ValidationConstants
    {
        public class Task
        {
            public const int TaskMaxTitle = 70;
            public const int TaskMinTitle = 5;

            public const int TaskMaxDescription = 1000;
            public const int TaskMinDescription = 10;
        }

        public class Board
        {
            public const int BoardMaxName = 30;
            public const int BoardMinName = 3;
        }
    }
}
