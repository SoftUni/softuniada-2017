using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06ShopKeeperMediocre
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] stockInput = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int[] ordersInput = Console.ReadLine().Split().Select(int.Parse).ToArray();
            HashSet<int> stock = new HashSet<int>();
            Dictionary<int, int> typesCount = new Dictionary<int, int>();
            for (int i = 0; i < stockInput.Length; i++)
            {
                stock.Add(stockInput[i]);
            }

            for (int i = 0; i < ordersInput.Length; i++)
            {
                if (!typesCount.ContainsKey(ordersInput[i]))
                {
                    typesCount.Add(ordersInput[i], 0);
                }
                typesCount[ordersInput[i]]++;
            }

            bool solution = true;
            int changes = 0;

            for (int i = 0; i < ordersInput.Length - 1; i++)
            {
                if (!stock.Contains(ordersInput[i]))
                {
                    solution = false;
                    break;
                }

                if (!stock.Contains(ordersInput[i + 1]))
                {
                    int item = FindNext(ordersInput, stock, i);
                    stock.Remove(item);
                    stock.Add(ordersInput[i + 1]);
                    //Console.WriteLine("{0} -> {1} <-> {2}",i,item,ordersInput[i+1]);
                    //var list = stock.ToList();
                    //list.Sort();
                    //Console.WriteLine(string.Join(" ",list));
                    changes++;
                }
            }

            if (!stock.Contains(ordersInput[ordersInput.Length - 1]))
            {
                solution = false;
            }

            Console.WriteLine(solution ? changes.ToString() : "impossible");
        }

        static int FindNext(int[] orders, HashSet<int> stock, int currentPosition)
        {
            HashSet<int> usedTypes = new HashSet<int>();
            List<int> typesByPosition = new List<int>();
            for (int i = currentPosition + 1; i < orders.Length; i++)
            {
                if (stock.Contains(orders[i]) && !usedTypes.Contains(orders[i]))
                {
                    typesByPosition.Add(orders[i]);
                    usedTypes.Add(orders[i]);
                }
            }
            foreach (var item in stock)
            {
                if (!usedTypes.Contains(item))
                {
                    typesByPosition.Add(item);
                }
            }
            return typesByPosition[typesByPosition.Count - 1];
        }
    }
}
