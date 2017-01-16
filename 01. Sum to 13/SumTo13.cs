using System;

namespace _01SumTo13
{
    class SumTo13
    {
        static void Main(string[] args)
        {
            string[] nums = Console.ReadLine().Split();
            int a = int.Parse(nums[0]);
            int b = int.Parse(nums[1]);
            int c = int.Parse(nums[2]);

            if (a + b + c == 13 ||
                -a + b + c == 13 ||
                a - b + c == 13 ||
                a + b - c == 13 ||
                -a - b + c == 13 ||
                -a + b - c == 13 ||
                a - b - c == 13 ||
                -a - b - c == 13)
            {
                Console.WriteLine("Yes");
            }
            else
            {
                Console.WriteLine("No");
            }
        }
    }
}
