using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace KingdomConnectivity
{
    class Program
    {
        const long Mod = 1000000000;
        const int White = 0;
        const int Grey = 1;
        const int Black = 2;

        static List<int>[] graph;
        static List<int>[] reverseGraph;
        static bool[] cycles;
        static bool cycle;

        static int[] visited;
        static BigInteger[] pathsToNode;

        static void Main(string[] args)
        {
            var data = Console.ReadLine().Split(' ');
            var n = int.Parse(data[0]);
            var m = int.Parse(data[1]);

            graph = new List<int>[n + 1];
            reverseGraph = new List<int>[n + 1];
            for (int i = 0; i < m; i++)
            {
                var road = Console.ReadLine().Split(' ');
                var u = int.Parse(road[0]);
                var v = int.Parse(road[1]);

                if (graph[u] == null)
                    graph[u] = new List<int>();

                if (reverseGraph[v] == null)
                    reverseGraph[v] = new List<int>();

                graph[u].Add(v);
                reverseGraph[v].Add(u);
            }

            visited = new int[n + 1];
            cycles = new bool[n + 1];

            // Find all cycles from src
            FindCycles(graph, 1);
            for (int i = 0; i < visited.Length; i++)
                visited[i] = White;

            // Find all ancestor nodes of dest
            Dfs(reverseGraph, n);
            for (int i = 1; i < cycles.Length; i++)
            {
                // If node is part of cycle AND is ancestor of dest
                // -> a cycle lies in the path src -> dest
                if (cycles[i] && visited[i] == Black)
                {
                    cycle = true;
                    break;
                }
            }

            if (cycle)
            {
                Console.WriteLine("infinite");
            }
            else
            {
                pathsToNode = new BigInteger[n + 1];
                pathsToNode[1] = 1;
                pathsToNode[n] = 0;
                for (int i = 0; i < visited.Length; i++)
                    visited[i] = White;

                FindPaths(n);
                Console.WriteLine("{0} {1}", pathsToNode[n] % Mod, cycles.Any(c => c) ? "yes" : "no");
            }
        }

        static void FindCycles(List<int>[] g, int node)
        {
            visited[node] = Grey;
            if (g[node] != null)
            {
                foreach (var child in g[node])
                {
                    if (visited[child] == White)
                    {
                        FindCycles(g, child);
                    }
                    else if (visited[child] == Grey)
                    {
                        cycles[child] = true;
                    }
                }
            }

            visited[node] = Black;
        }

        static void Dfs(List<int>[] g, int node)
        {
            visited[node] = Grey;
            if (g[node] != null)
            {
                foreach (var child in g[node])
                {
                    if (visited[child] == White)
                    {
                        Dfs(g, child);
                    }
                }
            }

            visited[node] = Black;
        }

        static void FindPaths(int node)
        {
            visited[node] = Grey;
            if (reverseGraph[node] != null)
            {
                foreach (var parent in reverseGraph[node])
                {
                    if (visited[parent] == White)
                    {
                        FindPaths(parent);
                    }

                    pathsToNode[node] += pathsToNode[parent];
                }
            }

            visited[node] = Black;
        }
    }
}
