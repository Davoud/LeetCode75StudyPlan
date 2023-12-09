using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode75StudyPlan.Graphs
{
    internal class NumberOfIslands : Solution<char[][], int>
    {        
        public int NumIslands(char[][] grid)
        {            
            int N = grid.Length;
            int M = grid[0].Length;            
            int count = 0;
            for(int i = 0; i < N; i++)
            {
                for(int j = 0; j < M; j++)
                {
                    if (grid[i][j] == '1')
                    {
                        Dfs(i, j);
                        count++;
                    }
                }
            }
            return count;

            void Dfs(int i, int j)
            {
                if (0 <= i && i < N && 0 <= j && j < M && grid[i][j] == '1')
                {
                    grid[i][j] = 'X';
                    Dfs(i - 1, j);
                    Dfs(i + 1, j);
                    Dfs(i, j - 1);
                    Dfs(i, j + 1);
                }
            }
        }

       
        protected override string Title => "200. Number of Islands";

        protected override IEnumerable<(char[][], int)> TestCases
        {
            get
            {
                char[][] grid = [
                    ['1', '1', '1'],
                    ['0', '1', '0'],
                    ['1', '1', '1']];
                yield return (grid, 1);
                //yield break;

                grid = [
                    ['1', '1', '1', '1', '0'],
                    ['1', '1', '0', '1', '0'],
                    ['1', '1', '0', '0', '0'],
                    ['0', '0', '0', '0', '0']];

                yield return (grid, 1);

                grid = [
                    ['1', '1', '0', '0', '0'],
                    ['1', '1', '0', '0', '0'],
                    ['0', '0', '1', '0', '0'],
                    ['0', '0', '0', '1', '1']];

                yield return (grid, 3);
            }
        }

        protected override int Solve(char[][] input) => NumIslands(input);
    }
    internal class NumberOfIslands1 : Solution<char[][], int>
    {
        public int NumIslands(char[][] grid)
        {
            List<ISet<(int x, int y)>> collections = new();
            int N = grid.Length;
            int M = grid[0].Length;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    if (grid[i][j] == '1')
                    {
                        var set = FindAndAdd((i, j));
                        (int x, int y) = (i - 1, j);
                        if (x >= 0) set.Add((x, y));

                        (x, y) = (i, j - 1);
                        if (y >= 0) set.Add((x, y));

                        (x, y) = (i + 1, j);
                        if (x < N) set.Add((x, y));

                        (x, y) = (i, j + 1);
                        if (y < M) set.Add((x, y));
                    }
                }
            }
            return collections.Count;

            ISet<(int, int)> FindAndAdd((int, int) xy)
            {

                foreach (var set in collections)
                {
                    if (set.Contains(xy))
                        return set;
                }

                var s = new HashSet<(int, int)> { xy };
                collections.Add(s);
                return s;

            }

        }

        protected override string Title => "200. Number of Islands";

        protected override IEnumerable<(char[][], int)> TestCases
        {
            get
            {
                char[][] grid = [
                    ['1', '1', '1', '1', '0'],
                    ['1', '1', '0', '1', '0'],
                    ['1', '1', '0', '0', '0'],
                    ['0', '0', '0', '0', '0']];

                yield return (grid, 1);

                grid = [
                    ['1', '1', '0', '0', '0'],
                    ['1', '1', '0', '0', '0'],
                    ['0', '0', '1', '0', '0'],
                    ['0', '0', '0', '1', '1']];

                yield return (grid, 3);
            }
        }

        protected override int Solve(char[][] input) => NumIslands(input);

    }


}
