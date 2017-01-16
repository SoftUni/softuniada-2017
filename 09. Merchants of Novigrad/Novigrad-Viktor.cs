using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _09Novigrad
{
    class Novigrad
    {
        private static Dictionary<int, List<int>> graph;
        private static Dictionary<int, List<int>> reverseGraph;
        private static HashSet<int> cycleNodes = new HashSet<int>();
        private static bool[] reachableFromEnd;
        private static bool[] visited;
        private static HashSet<int> onPath = new HashSet<int>();
        private static int[] paths;
        private static bool hasInfinityCycle = false;
        private static bool hasCycle = false;

        static void Main(string[] args)
        {
            graph = new Dictionary<int, List<int>>();
            reverseGraph = new Dictionary<int, List<int>>();
            string[] tokens = Console.ReadLine().Split();
            int vertices = int.Parse(tokens[0]);
            int roads = int.Parse(tokens[1]);
            paths = new int[vertices+ 1];
            reachableFromEnd = new bool[vertices + 1];
            visited = new bool[vertices + 1];
            for (int i = 1; i <= vertices; i++)
            {
                graph.Add(i,new List<int>());
                reverseGraph.Add(i,new List<int>());
            }
            for (int i = 0; i < roads; i++)
            {
                string[] edgeTokens = Console.ReadLine().Split();
                int parent = int.Parse(edgeTokens[0]);
                int child = int.Parse(edgeTokens[1]);
                graph[parent].Add(child);
                reverseGraph[child].Add(parent);
            }
            FindCoverage(vertices);
            paths[vertices] = 1;
            DFS(1);

            if (hasInfinityCycle)
            {
                Console.WriteLine("infinite");
            }
            else
            {
                Console.WriteLine("{0} {1}", paths[1], hasCycle ? "yes" : "no");
            }
        }

        private static void FindCoverage(int start)
        {
            reachableFromEnd[start] = true;
            foreach (var child in reverseGraph[start])
            {
                if (!reachableFromEnd[child])
                {
                    FindCoverage(child);
                }
            }
        }

        private static void DFS(int start)
        {
            if (hasInfinityCycle)
            {
                return;
            }
            onPath.Add(start);
            visited[start] = true;
            foreach (var child in graph[start])
            {
                if (onPath.Contains(child))
                {
                    hasCycle = true;
                    if (reachableFromEnd[child])
                    {
                        hasInfinityCycle = true;
                        return;
                    }
                }
                if (!visited[child])
                {
                    DFS(child);
                }

                paths[start] = (paths[start] + paths[child]) % 1000000000;
            }
            onPath.Remove(start);
        }
    }
}
