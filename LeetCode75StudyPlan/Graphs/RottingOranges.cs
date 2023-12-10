using System.Diagnostics;

namespace LeetCode75StudyPlan.Graphs;

internal class RottingOranges : Solution<int[][], int>
{   
    const int FRESH = 1;
    const int ROTTEN = 2;

    public int OrangesRotting(int[][] grid)
    {
        int m = grid.Length, n = grid[0].Length;

        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {               
                if (grid[i][j] == ROTTEN)
                {
                    Bfs(i, j);                                       
                }
            }
        }

        int max = -1;
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (grid[i][j] == FRESH) return -1;                                
                max = Math.Max(max, grid[i][j]);
            }
        }

        return max < ROTTEN ? max : max - ROTTEN;

        void Bfs(int i, int j)
        {
            Queue<(int x, int y)> q = new();
            bool[,] visited = new bool[m, n];

            q.Enqueue((i, j));
            visited[i, j] = true;            

            while (q.Count > 0)
            {
                (int a, int b) = q.Dequeue();                
                int depth = grid[a][b];

                foreach((int x, int y) in AdjOf(a,b))
                {
                    if (!visited[x, y] && (grid[x][y] == FRESH || grid[x][y] > depth))
                    {
                        q.Enqueue((x, y));
                        visited[x, y] = true;
                        grid[x][y] = depth + 1;
                    }
                }               
            }            
        }

        IEnumerable<(int x, int y)> AdjOf(int x, int y)
        {
            if (x - 1 >= 0) yield return (x - 1, y);
            if (x + 1 <  m) yield return (x + 1, y);
            if (y - 1 >= 0) yield return (x, y - 1);
            if (y + 1 <  n) yield return (x, y + 1);            
        }

    }

    protected override string Title => "994: Rotting Oranges";

    protected override IEnumerable<(int[][], int)> TestCases
    {
        get
        {
            yield return ([[2, 1, 1],
                           [1, 1, 0],
                           [0, 1, 1]], 4);

            yield return ([[2, 1, 1],
                           [0, 1, 1],
                           [0, 0, 2]], 2);

            yield return ([[2, 1, 1],
                           [0, 1, 1],
                           [1, 0, 1]], -1);
        }
    }

    protected override int Solve(int[][] input) => OrangesRotting(input);

}
