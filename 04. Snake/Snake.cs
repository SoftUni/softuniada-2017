using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04Snake
{
    class Snake
    {
        private static int n;
        private static char[][][] cube;

        static void Main(string[] args)
        {
            n = int.Parse(Console.ReadLine());
            cube = new char[n][][];
            int startDepth = -1;
            int startRow = -1;
            int startCol = -1;
            int apples = 0;

            for (int depth = 0; depth < n; depth++)
            {
                cube[depth] = new char[n][];
            }
            for (int row = 0; row < n; row++)
            {
                string[] rows = Console.ReadLine()
                    .Split(new string[] { " | " }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();
                for (int depth = 0; depth < n; depth++)
                {
                    cube[depth][row] = new char[n];
                    for (int col = 0; col < n; col++)
                    {
                        char letter = rows[depth][col];
                        cube[depth][row][col] = letter;
                        if (cube[depth][row][col] == 's')
                        {
                            startDepth = depth;
                            startRow = row;
                            startCol = col;
                        }
                    }
                   
                }
            }
            bool isDead = false;

            string direction = Console.ReadLine();
            string line = Console.ReadLine();
            while (!line.StartsWith("end"))
            {
                string[] tokens = line.Split();
                string nextDirection = tokens[0];
                int steps = int.Parse(tokens[2]);
                
                MoveSnake(ref startDepth, ref startRow, ref startCol, ref apples, ref isDead, steps, direction);
                if (isDead)
                {
                    break;
                }
                direction = nextDirection;
                line = Console.ReadLine();
            }

            if (!isDead)
            {
                string[] tokens = line.Split();
                int steps = int.Parse(tokens[2]);
                MoveSnake(ref startDepth, ref startRow, ref startCol, ref apples, ref isDead, steps, direction);

            }

            Console.WriteLine("Points collected: {0}",apples);
            if (isDead)
            {
                Console.WriteLine("The snake dies.");
            }
        }

        private static bool IsInside(int depth, int row, int col)
        {
            if (depth < 0 || depth >= n ||
                row < 0 || row >= n ||
                col < 0 || col >= n)
            {
                return false;
            }

            return true;
        }

        private static void MoveSnake(ref int depth, ref int row, ref int col, ref int apples, ref bool isDead, int steps, string direction)
        {
            for (int i = 0; i < steps; i++)
            {
                ModifyDirection(ref depth,ref row, ref col,direction);
                if (IsInside(depth, row, col))
                {
                    if (cube[depth][row][col] == 'a')
                    {
                        cube[depth][row][col] = 'o';
                        apples++;
                    }
                }
                else
                {
                    isDead = true;
                    break;
                }
            }
        }

        private static void ModifyDirection(ref int depth,ref int row,ref int col, string direction)
        {
            switch (direction)
            {
                case "up":
                    depth -= 1;
                    break;
                case "down":
                    depth += 1;
                    break;
                case "forward":
                    row -= 1;
                    break;
                case "backward":
                    row += 1;
                    break;
                case "left":
                    col -= 1;
                    break;
                case "right":
                    col += 1;
                    break;
            }
        }
    }
}
