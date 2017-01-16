using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06ShopKeeper
{
    class Order : IComparable<Order>
    {
        public Order(int type)
        {
            this.ItemType = type;
            this.OrderPositions = new List<int>();
        }

        public int ItemType { get; set; }

        public List<int> OrderPositions { get; set; }

        public int CompareTo(Order other)
        {
            if (this.ItemType == other.ItemType)
            {
                return 0;
            }

            if(!this.OrderPositions.Any())
            {
                return 1;
            }
            else if(!other.OrderPositions.Any())
            {
                return -1;
            }

            return this.OrderPositions[0].CompareTo(other.OrderPositions[0]);
        }
    }

    public class ShopKeeper
    {
        public static void Main()
        {
            int[] stockInput = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int[] ordersInput = Console.ReadLine().Split().Select(int.Parse).ToArray();
            HashSet<int> stock = new HashSet<int>();
            HashSet<int> stockNotCovered = new HashSet<int>();
            for (int i = 0; i < stockInput.Length; i++)
            {
                stock.Add(stockInput[i]);
                stockNotCovered.Add(stockInput[i]);
            }

            SortedSet<Order> ordersByOrderTime = new SortedSet<Order>();
            SortedSet<Order> stockByOrderTime = new SortedSet<Order>();

            Dictionary<int,Order> orderItemTypes = new Dictionary<int, Order>();
            for (int i = 0; i < ordersInput.Length; i++)
            {
                if (!orderItemTypes.ContainsKey(ordersInput[i]))
                {
                    orderItemTypes.Add(ordersInput[i],new Order(ordersInput[i]));
                }
                orderItemTypes[ordersInput[i]].OrderPositions.Add(i);
            }

            foreach (var orderItemType in orderItemTypes)
            {
                ordersByOrderTime.Add(orderItemType.Value);
                if (stock.Contains(orderItemType.Key))
                {
                    stockByOrderTime.Add(orderItemType.Value);
                    stockNotCovered.Remove(orderItemType.Key);
                }
            }

            foreach (var itemType in stockNotCovered)
            {
                stockByOrderTime.Add(new Order(itemType));
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

                Order currentOrder = ordersByOrderTime.Min;
                ordersByOrderTime.Remove(ordersByOrderTime.Min);
                stockByOrderTime.Remove(stockByOrderTime.Min);
                currentOrder.OrderPositions.RemoveAt(0);
                ordersByOrderTime.Add(currentOrder);
                stockByOrderTime.Add(currentOrder);

                Order nextOrder = ordersByOrderTime.Min;

                if (!stock.Contains(nextOrder.ItemType))
                {
                    //var a = stockByOrderTime.Max;
                    stock.Remove(stockByOrderTime.Max.ItemType);
                    stockByOrderTime.Remove(stockByOrderTime.Max);
                    stockByOrderTime.Add(nextOrder);
                    stock.Add(nextOrder.ItemType);
                    //Console.WriteLine("{0} -> {1} <-> {2}", i, a.ItemType, ordersInput[i + 1]);
                    //var list = stock.ToList();
                    //list.Sort();
                    //Console.WriteLine(string.Join(" ", list));
                    changes++;
                }
            }

            if (!stock.Contains(ordersInput.Last()))
            {
                solution = false;
            }

            Console.WriteLine(solution ? changes.ToString() : "impossible");
        }
    }
}
