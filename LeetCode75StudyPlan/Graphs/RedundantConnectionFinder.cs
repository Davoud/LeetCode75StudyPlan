using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;

namespace LeetCode75StudyPlan.Graphs;

internal class RedundantConnectionFinder : Solution<int[][], int[]>
{
    //union find (no ranking)
    public int[] FindRedundantConnection(int[][] edges) 
    {
        int[] p = new int[edges.Length + 1];

        foreach (int[] e in edges)
        {
            int x = f(e[0]), y = f(e[1]);            
            if (x == y) return e;
            p[x] = y;
        }

        return Array.Empty<int>();        
       
        int f(int c)
        {
            if (p[c] == 0) return c;            
            p[c] = f(p[c]);
            return p[c];
        }
    }

    public int[] _FindRedundantConnection(int[][] edges)
    {
        int[] g = new int[edges.Length + 2];
        int c = 1;
        foreach (int[] edge in edges)
        {
            (int a, int b) = (edge[0], edge[1]);
            (int x, int y) = (g[a], g[b]);
            if (x == y)
            {
                if (x == 0) 
                {
                    g[a] = g[b] = c++;
                } 
                else 
                {
                    return new int[] { a, b };
                }
            }
            else if (x == 0)
            {
                g[a] = y;
            }
            else if (y == 0)
            {
                g[b] = x;
            }
            else
            {
                for (int i = 1; i <= edges.Length; i++)
                    if (g[i] == x) g[i] = y;
            }

        }

        return Array.Empty<int>();
       
    }

    
    protected override string Title => "684. Redundant Connection";

    protected override IEnumerable<(int[][], int[])> TestCases
    {
        get
        {
            yield return ([[3, 7], [1, 4], [2, 8], [1, 6], [7, 9], [6, 10], [1, 7], [2, 3], [8, 9], [5, 9]], [8, 9]);
            yield return ([[1, 2], [1, 3], [2, 3]], [2, 3]);
            yield return ([[1, 2], [2, 3], [3, 4], [1, 4], [1, 5]], [1, 4]);
        }
    }

    protected override int[] Solve(int[][] input) => FindRedundantConnection(input);

}
