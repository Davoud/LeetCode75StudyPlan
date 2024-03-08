
using System.Collections;
using System.Xml.Schema;

namespace LeetCode75StudyPlan.Graphs;

internal class SwimInRisingWater : Solution<int[][], int>
{
    public int SwimInWaterBS(int[][] grid)
    {
        int n = grid.Length;
        int l = Math.Min(grid[0][0], grid[n - 1][n - 1]);
        int r = (n * n) - 1;
        int mid;        
        BitArray met = new(n * n);

        while (l <= r)
        {
            mid = (l + r) / 2;
            if (dfs(0, 0)) r = mid - 1; else l = mid + 1;
            met.SetAll(false);
        }
        return l;

        bool dfs(int i, int j)
        {
            if (i < 0 || j < 0 || i >= n || j >= n || met[(i * n) + j] || grid[i][j] > mid) return false;
            if (i == n - 1 && j == n - 1) return true;
            
            met[(i * n) + j] = true;
            
            if (dfs(i + 1, j)) return true;
            if (dfs(i - 1, j)) return true;
            if (dfs(i, j + 1)) return true;
            if (dfs(i, j - 1)) return true;
            
            return false;
        }
    }


    // correct but not efficient enough
    public int SwimInWaterUnfionFind(int[][] grid)
    {
        int N = grid.Length;
        UnionFind uf = new(N * N);
        int time = 0;
        while (!uf.Connected(0, (N * N) - 1))
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (grid[i][j] <= time)
                    {
                        if (i < N - 1 && grid[i + 1][j] <= time)
                            uf.Connect((i * N) + j, (i * N) + j + N);

                        if (j < N - 1 && grid[i][j + 1] <= time)
                            uf.Connect((i * N) + j, (i * N) + j + 1);
                    }
                }
            }
            time++;
        }
        return time - 1;
    }

    class UnionFind
    {
        private readonly int[] id;
        private readonly int[] sz;
        public UnionFind(int n)
        {
            id = new int[n];
            sz = new int[n];
            for (int i = 0; i < n; i++)
                id[i] = i;
        }

        //public int Find(int x) => parent[x] == x ? x : Find(parent[x]);
        public int Find(int x)
        {
            while (x != id[x])
            {
                id[x] = id[id[x]];
                x = id[x];
            }
            return x;
        }
        public bool Connected(int x, int y) => Find(x) == Find(y);
        public void Connect(int x, int y)
        {
            (int r1, int r2) = (Find(x), Find(y));
            if (r1 != r2)
            {
                if (sz[r1] >= sz[r2])
                {
                    sz[r1] += sz[r2];
                    id[r2] = r1;
                }
                else
                {
                    sz[r2] += sz[r1];
                    id[r1] = r2;
                }
            }
        }


    }
    protected override string Title => "778. Swim in Rising Water";

    protected override IEnumerable<(int[][], int)> TestCases
    {
        get
        {
            yield return ([
                [3, 2],
                [0, 1]], 3);

            yield return ([
                [0, 2],
                [1, 3]], 3);

            yield return ([
                [00, 01, 02, 03, 04],
                [24, 23, 22, 21, 05],
                [12, 13, 14, 15, 16],
                [11, 17, 18, 19, 20],
                [10, 09, 08, 07, 06]], 16);
        }
    }

    protected override int Solve(int[][] input) => SwimInWaterBS(input);
    //protected override int Solve(int[][] input) => SwimInWaterUnfionFind(input);
}
