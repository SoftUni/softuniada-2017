using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem9
{
    public class Stick
    {
        public Point TopLeft { get; set; }

        public Point BottomRight { get; set; }
    }

    public class Point
    {
        public int X { get; set; }

        public int Y { get; set; }
    }

    public class Test
    {
        static int[] rank;
        static int[] parent;
        static Stick[] sticks;
        static StringBuilder output;

        static void Main(string[] args)
        {
            //int t = int.Parse(Console.ReadLine());
            parent = new int[1001];
            rank = new int[1001];
            sticks = new Stick[1001];
            output = new StringBuilder();
            //for (int i = 0; i < t; i++)
            //{
                Solve();
            //}

            Console.WriteLine(output);
        }

        static void Solve()
        {
            var data = Console.ReadLine().Split(' ');
            var n = int.Parse(data[0]);
            var m = int.Parse(data[1]);

            for (int j = 1; j <= n; j++)
            {
                var logData = Console.ReadLine().Split(' ');
                sticks[j] = new Stick
                {
                    TopLeft = new Point() { X = int.Parse(logData[0]), Y = int.Parse(logData[1]) },
                    BottomRight = new Point() { X = int.Parse(logData[2]), Y = int.Parse(logData[3]) }
                };

                parent[j] = j;
                rank[j] = 0;
                for (int p = 1; p < j; p++)
                {
                    if (!IsSameSet(j, p) && 
                        //doIntersect(sticks[j].A, sticks[j].B, sticks[p].A, sticks[p].B))
                        Intersect(sticks[j], sticks[p]))
                    {
                        Union(j, p);
                    }
                }
            }

            for (int j = 0; j < m; j++)
            {
                var queryData = Console.ReadLine().Split(' ');
                var start = int.Parse(queryData[0]);
                var end = int.Parse(queryData[1]);
                output.AppendLine(IsSameSet(start, end) ? "YES" : "NO");
            }
        }

        static void Union(int a, int b)
        {
            var rootA = FindSet(a);
            var rootB = FindSet(b);

            // Attach the set with lower rank (height) 
            // to the other set
            if (rank[rootA] < rank[rootB])
            {
                parent[rootA] = rootB;
            }
            else if (rank[rootA] > rank[rootB])
            {
                parent[rootB] = rootA;
            }
            else
            {
                parent[rootB] = rootA;
                rank[rootA]++;
            }
        }

        static bool IsSameSet(int a, int b)
        {
            return FindSet(a) == FindSet(b);
        }

        static int FindSet(int node)
        {
            // Find the root parent for the node
            int root = node;
            while (parent[root] != root)
            {
                root = parent[root];
            }

            // Optimize (compress) the path from node to root
            //while (node != root)
            //{
            //    var oldParent = parent[node];
            //    parent[node] = root;
            //    node = oldParent;
            //}

            return root;
        }

        static bool Intersect(Stick a, Stick b)
        {
            return a.TopLeft.X <= b.BottomRight.X &&
                b.TopLeft.X <= a.BottomRight.X &&
                a.TopLeft.Y >= b.BottomRight.Y &&
                b.TopLeft.Y >= a.BottomRight.Y;
        }
    }
}
