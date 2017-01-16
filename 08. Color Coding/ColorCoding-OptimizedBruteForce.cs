using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05ColorCoding
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
    public class ColorCoding
    {
        public static void Main()
        {
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                Color[] firstSequence = Console.ReadLine().Split().Select(x=>new Color(x)).ToArray();
                Color[] targetSequence = Console.ReadLine().Split().Select(x=>new Color(x)).ToArray();
                if (CanChange(firstSequence, targetSequence))
                {
                    Console.WriteLine("true");
                }
                else
                {
                    Console.WriteLine("false");
                }
            }
        }

        private static bool CanChange(Color[] firstSequence, Color[] targetSequence)
        {
            int val = 0;
            int[] used = new int[targetSequence.Length];
            int fullColors = 0;

            for (int i = 0; i < firstSequence.Length; i++)
            {
                if (firstSequence[i].IsFull)
                {
                    fullColors++;
                }
            }

            if (fullColors > targetSequence.Length)
            {
                return false;
            }

            for (int j = 0; j < firstSequence.Length; j++)
            {
                if (firstSequence[j].IsFull)
                {
                    while (val >= 0)
                    {
                        if (firstSequence[j].ColorText == targetSequence[val].ColorText)
                        {
                            if (used[val] != 0)
                            {
                                //if we find a new Upper letter, we have to go back and check if we can find new positions 
                                //for the other Upper letters we have already used 
                                bool result = FixBackwards(used, j, val, firstSequence, targetSequence);
                                if (!result) return false;
                                break;
                            }
                            else
                            {
                                used[val] = j;
                                break;
                            }
                        }
                        val--;
                    }

                    if (val == -1)
                    {
                        return false;
                    }
                }

                if (firstSequence[j].ColorText == targetSequence[val].ColorText)
                {
                    val++;
                }

                if (val == targetSequence.Length)
                {
                    return true;
                }
            }

            return false;
        }

        static bool FixBackwards(int[] used, int firstColorIndex, int val, Color[] firstSequence, Color[] targetSequence)
        {
            int prev = used[val];
            used[val] = firstColorIndex;
            while (val > 0)
            {
                if (targetSequence[val].ColorText != firstSequence[firstColorIndex].ColorText)
                {
                    break;
                }

                val--;
                for (int i = firstColorIndex - 1; i >= prev; i--)
                {
                    if (targetSequence[val].ColorText == firstSequence[firstColorIndex].ColorText)
                    {
                        if (used[val] == 0)
                        {
                            used[val] = prev;
                            return true;
                        }

                        firstColorIndex = prev;
                        prev = used[val];
                        used[val] = firstColorIndex;
                        break;
                    }

                    if (firstSequence[i].ColorText == targetSequence[val].ColorText) val--;
                    if (val < 0) break;
                }
            }

            return false;
        }
    }
}
