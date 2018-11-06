using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RottenOrangesSolution
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = new int[3, 5]
            {
                {2, 1, 0, 2, 1},
                {1, 0, 1, 2, 1},
                {1, 0, 0, 2, 1}
                //{0, 0, 0, 0, 0},
                //{0, 0, 0, 0, 0},
                //{0, 0, 0, 0, 0}
            };

            var time = RotOranges(input);

            Console.WriteLine($"Time to rot all oranges: {time}");
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        private static int RotOranges(int[,] input)
        {
            var rows = input.GetLength(0);
            var cols = input.GetLength(1);

            var queue = new Queue<OrangePosition>();
            
            // put all rotten
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    if (input[i, j] != 2) continue;
                    queue.Enqueue(new OrangePosition(i, j));
                }
            }

            var time = 0;
            var maxTime = 0;

            while (queue.Count > 0) // not empty
            {
                var pos = queue.Dequeue();
                time = pos.Time;
                if (time > maxTime) maxTime = time;

                // left
                MakeRotten(input, queue, pos.Row, pos.Col - 1, time+1);
                // right
                MakeRotten(input, queue, pos.Row, pos.Col + 1, time+1);
                // top
                MakeRotten(input, queue, pos.Row - 1, pos.Col, time+1);
                // bottom
                MakeRotten(input, queue, pos.Row + 1, pos.Col, time+1);
            }

            return WasFullyRotten(input) ? -1 : maxTime;
        }

        private static bool WasFullyRotten(int[,] input)
        {
            var rows = input.GetLength(0);
            var cols = input.GetLength(1);

            var sum = 0;
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    sum += input[i, j];
                    if (input[i, j] == 1)
                    {
                        return true;
                    }
                }
            }

            return sum == 0; // sum == 0 if all slots are 0, so should return true because it's impossible to rot all oranges
        }

        private static bool MakeRotten(int[,] input, Queue<OrangePosition> stack, int row, int col, int time)
        {
            var rows = input.GetLength(0);
            var cols = input.GetLength(1);

            if (!IsValid(rows, cols, row, col) || input[row, col] != 1) return false;

            input[row, col] = 2;
            var pos = new OrangePosition(row, col, time);
            stack.Enqueue(pos);
            return true;
        }

        [DebuggerDisplay("({Row},{Col},{Time})")]
        struct OrangePosition
        {
            public int Row;
            public int Col;
            public int Time;

            public OrangePosition(int row, int col, int time)
            {
                Row = row;
                Col = col;
                Time = time;
            }

            public OrangePosition(int row, int col)
            {
                Row = row;
                Col = col;
                Time = 0;
            }
        }

        static bool IsValid(int rows, int cols, int row, int col)
        {
            return col >= 0 && row >= 0 && row < rows && col < cols;
        }
    }
}
