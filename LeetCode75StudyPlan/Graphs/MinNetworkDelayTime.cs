using System.Collections;

namespace LeetCode75StudyPlan.Graphs;

internal class MinNetworkDelayTime : Solution<(int[][] times, int n, int k), int>
{
    public int NetworkDelayTime(int[][] times, int n, int k)
    {
        var graph = new List<(int u, int w)>[n + 1];
        foreach (int[] time in times)
            (graph[time[0]] ??= new()).Add((time[1], time[2]));
        
        int?[] distance = DijkstraShortestPath(graph, k);

        int max = -1;
        for (int i = 1; i < graph.Length; i++)
        {
            if (distance[i] is int dist)
            {
                if (dist > max) max = dist;
            }
            else
            {
                return -1;
            }                        
        }

        return max;
    }

    private int?[] DijkstraShortestPath(List<(int u, int w)>[] graph, int start)
    {
        var intree = new BitArray(graph.Length);
        var distance = new int?[graph.Length];

        (int v, int dist) = (start, int.MaxValue);

        distance[start] = 0;

        while (!intree[v])
        {
            intree[v] = true;
            if (graph[v] != null)
            {
                foreach ((int u, int weight) in graph[v])
                {
                    if (!distance[u].HasValue || distance[u] > distance[v] + weight)
                    {
                        distance[u] = distance[v] + weight;
                    }
                }
            }

            (v, dist) = (1, int.MaxValue);
            for (int i = 1; i < graph.Length; i++)
            {                
                if (!intree[i] && distance[i] is int minDist && dist > minDist)
                {
                    dist = minDist;
                    v = i;
                }
            }
        }

        return distance;        
    }

    protected override string Title => "743. Network Delay Time";

    protected override IEnumerable<((int[][] times, int n, int k), int)> TestCases
    {
        get
        {
            yield return (([[1, 2, 1], [2, 3, 2], [1, 3, 1]], 3, 2), -1);
            yield return (([[2, 1, 1], [2, 3, 1], [3, 4, 1]], 4, 2), 2);
            yield return (([[1, 2, 1]], 2, 1), 1);
            yield return (([[1, 2, 1]], 2, 2), -1);
        }
    }

    protected override int Solve((int[][] times, int n, int k) input)
        => NetworkDelayTime(input.times, input.n, input.k);

}
