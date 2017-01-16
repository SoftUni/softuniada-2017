using System;

namespace _02HalloweenPumpkin
{
    public class HalloweenPumpkin
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int length = n*2 + 1;
            int midsize = n - 3;
            Console.WriteLine("{0}_/_{0}",new string('.',n-1));
            Console.WriteLine("/{0}^,^{0}\\",new string('.',n-2));
            for (int i = 0; i < midsize; i++)
            {
                Console.WriteLine("|{0}|",new string('.',length-2));
            }

            Console.WriteLine("\\{0}\\_/{0}/", new string('.', n - 2));
        }
    }
}
