using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03DuplicatedLetters
{
    class DuplicatedLetters
    {
        static void Main(string[] args)
        {
            StringBuilder word = new StringBuilder(Console.ReadLine());
            bool change = false;
            int operations = 0;
            do
            {
                change = false;
                for (int i = 1; i < word.Length; i++)
                {
                    if (word[i] == word[i - 1])
                    {
                        change = true;
                        operations++;
                        word.Remove(i - 1, 2);
                        i--;
                    }
                }
            } while (change);
            if (word.Length == 0)
            {
                Console.WriteLine("Empty String");
            }
            else
            {
                Console.WriteLine(word.ToString());
            }

            Console.WriteLine("{0} operations", operations);
        }
    }
}
