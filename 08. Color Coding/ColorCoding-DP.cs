using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05ColorCodingReal
{
    public class Color
    {
        public Color(string color)
        {
            this.IsFull = true;
            this.ColorText = color;
            if (color.StartsWith("("))
            {
                this.IsFull = false;
                this.ColorText = color.Substring(1, color.Length - 2);
            }
        }

        public string ColorText { get; set; }

        public bool IsFull { get; set; }
    }

    public class ColorCodingReal
    {
        public static void Main()
        {
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                Color[] firstSequence = Console.ReadLine().Split().Select(x => new Color(x)).ToArray();
                Color[] targetSequence = Console.ReadLine().Split().Select(x => new Color(x)).ToArray();
                bool[,] matrix = new bool[firstSequence.Length + 1,targetSequence.Length + 1];
                matrix[0, 0] = true;

                for (int firstIndex = 0; firstIndex < firstSequence.Length; firstIndex++)
                {
                    for (int targetIndex = 0; targetIndex <= targetSequence.Length; targetIndex++)
                    {
                        if (!matrix[firstIndex, targetIndex]) continue;

                        if (!firstSequence[firstIndex].IsFull) matrix[firstIndex + 1, targetIndex] = true;

                        if (firstSequence[firstIndex].IsFull && targetIndex < targetSequence.Length &&
                            firstSequence[firstIndex].ColorText == targetSequence[targetIndex].ColorText)
                        {
                            matrix[firstIndex + 1, targetIndex + 1] = true;
                        }

                        if (!firstSequence[firstIndex].IsFull && targetIndex < targetSequence.Length &&
                            firstSequence[firstIndex].ColorText == targetSequence[targetIndex].ColorText)
                        {
                            matrix[firstIndex + 1, targetIndex + 1] = true;
                        }
                    }
                }

                //for (int j = 0; j <= firstSequence.Length; j++)
                //{
                //    for (int k = 0; k <= targetSequence.Length; k++)
                //    {
                //        Console.Write(matrix[j,k]);
                //        Console.Write(" ");
                //    }
                //    Console.WriteLine();
                //}

                Console.WriteLine(matrix[firstSequence.Length, targetSequence.Length] ? "true" : "false");
            }
        }
    }
}
