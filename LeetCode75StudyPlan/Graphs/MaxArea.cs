namespace LeetCode75StudyPlan.Graphs;

internal class MaxArea : Solution<int[][], int>
{
    public int MaxAreaOfIslandDfs(int[][] grid)
    {        
        int max = 0, N = grid.Length, M = grid[0].Length;
        
        for(int i = 0; i < N; i++)         
            for(int j = 0; j < M; j++)            
                if (grid[i][j] != 0)                
                    max = Math.Max(max, Dfs(i, j));                                    

        return max;

        int Dfs(int i, int j)
        {
            if (0 <= i && i < N && 0 <= j && j < M && grid[i][j] == 1)
            {
                grid[i][j] = -1;
                return 1 + Dfs(i - 1, j) + Dfs(i + 1, j) + Dfs(i, j - 1) + Dfs(i, j + 1);
            }
            return 0;     
        }
    }

    

    protected override string Title => "695. Max Area of Island";

    protected override IEnumerable<(int[][], int)> TestCases
    {
        get
        {
            int[][] grid = [
                [0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0],
                [0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0],
                [0, 1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0],
                [0, 1, 0, 0, 1, 1, 0, 0, 1, 0, 1, 0, 0],
                [0, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 0, 0],
                [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0],
                [0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0],
                [0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0]];

            yield return (grid, 6);
        }
    }

    protected override int Solve(int[][] input) => MaxAreaOfIslandDfs(input);
    
}
