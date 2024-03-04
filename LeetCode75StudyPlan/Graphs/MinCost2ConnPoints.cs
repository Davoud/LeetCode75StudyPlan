
using System.Collections;
using System.Net.NetworkInformation;

namespace LeetCode75StudyPlan.Graphs;

internal class MinCost2ConnPoints : Solution<int[][], int>
{
    public int MinCostConnectPoints(int[][] points)
    {
        var len = points.Length;
        var intree = new BitArray(len);
        var distance = new int[len];
        Array.Fill(distance, int.MaxValue);
        
        (int v, int dist) = (0, int.MaxValue);
        
        distance[0] = 0;

        while (!intree[v])
        {
            intree[v] = true;
            for(int y = 0; y < len; y++)
            {
                if(y != v && !intree[y])
                {
                    int[] p1 = points[v];
                    int[] p2 = points[y];
                    int w = Math.Abs(p1[0] - p2[0]) + Math.Abs(p1[1] - p2[1]);
                    if (distance[y] > w)
                    {
                        distance[y] = w;                       
                    }
                }
            }

            (v, dist) = (0, int.MaxValue);
            for (int i = 0; i < len; i++)
            {
                if (!intree[i] && dist > distance[i])
                {
                    dist = distance[i];
                    v = i;
                }
            }
        }

        return distance.Sum();

    }

    protected override string Title => "1584. Min Cost to Connect All Points";

    protected override IEnumerable<(int[][], int)> TestCases
    {
        get
        {
            yield return ([[0, 0], [2, 2], [3, 10], [5, 2], [7, 0]], 20);
            yield return ([[3, 12], [-2, 5], [-4, 1]], 18);
        }
    }

    protected override int Solve(int[][] input) => MinCostConnectPoints(input);
}
