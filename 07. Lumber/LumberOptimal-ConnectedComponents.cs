using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace _07Lumber
{
    public class Log
    {
        public Log(int x1, int y1, int x2, int y2, int logNumber)
        {
            this.X1 = x1;
            this.Y1 = y1;
            this.X2 = x2;
            this.Y2 = y2;
            this.LogNumber = logNumber;
        }

        public int X1 { get; set; }

        public int X2 { get; set; }

        public int Y1 { get; set; }

        public int Y2 { get; set; }

        public int LogNumber { get; set; }
    }
    public class Lumber
    {
        private static Dictionary<int, List<int>> logsGraph;

        public static void Main()
        {
            string[] parameters = Console.ReadLine().Split();
            int logsCount = int.Parse(parameters[0]);
            int queriesCount = int.Parse(parameters[1]);
            List<Log> logs = new List<Log>();
            logsGraph = new Dictionary<int, List<int>>();

            for (int i = 0; i < logsCount; i++)
            {
                string[] tokens = Console.ReadLine().Split();
                int x1 = int.Parse(tokens[0]);
                int y1 = int.Parse(tokens[1]);
                int x2 = int.Parse(tokens[2]);
                int y2 = int.Parse(tokens[3]);
                logs.Add(new Log(x1, y1, x2, y2, i + 1));
            }

            logs = logs.OrderBy(x => x.X1).ThenByDescending(x => x.Y1).ToList();
            for (int i = 0; i < logs.Count; i++)
            {
                int j = i + 1;
                if (!logsGraph.ContainsKey(logs[i].LogNumber)) logsGraph.Add(logs[i].LogNumber, new List<int>());

                while (j < logs.Count && logs[j].X1 <= logs[i].X2)
                {
                    if (logs[j].Y1 >= logs[i].Y2 && logs[j].Y2 <= logs[i].Y1)
                    {
                        if (!logsGraph.ContainsKey(logs[j].LogNumber)) logsGraph.Add(logs[j].LogNumber, new List<int>());
                        logsGraph[logs[i].LogNumber].Add(logs[j].LogNumber);
                        logsGraph[logs[j].LogNumber].Add(logs[i].LogNumber);
                    }
                    j++;
                }
            }

            int[] nodeToComponent = new int[logsCount + 1];
            FindConnectedComponents(nodeToComponent);

            //foreach (var log in logsGraph)
            //{
            //    Console.WriteLine("Log: {0} (Comp: {1}):", log.Key, nodeToComponent[log.Key]);
            //    Console.WriteLine("  {0}", string.Join(", ", log.Value));
            //}

            for (int i = 0; i < queriesCount; i++)
            {
                string[] tokens = Console.ReadLine().Split();
                int startLog = int.Parse(tokens[0]);
                int endLog = int.Parse(tokens[1]);

                Console.WriteLine(nodeToComponent[startLog] == nodeToComponent[endLog] ? "YES" : "NO");
            }
        }

        private static void FindConnectedComponents(int[] nodeToComponent)
        {
            bool[] visited = new bool[nodeToComponent.Length + 1];
            int currentComponent = 0;
            foreach (var pair in logsGraph)
            {
                if (!visited[pair.Key])
                {
                    currentComponent++;
                    DFS(pair.Key, currentComponent, visited, nodeToComponent);
                }
            }
        }

        private static void DFS(int el, int currentComponent, bool[] visited, int[] nodeToComponent)
        {
            if (!visited[el])
            {
                visited[el] = true;
                nodeToComponent[el] = currentComponent;
                foreach (var child in logsGraph[el])
                {
                    DFS(child,currentComponent, visited,nodeToComponent);
                }
            }
        }
    }
}
