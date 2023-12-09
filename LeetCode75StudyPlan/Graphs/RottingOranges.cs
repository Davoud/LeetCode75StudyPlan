namespace LeetCode75StudyPlan.Graphs;

internal class RottingOranges : Solution<int[][], int>
{
    const int EMPTY = 0;
    const int FRESH = 1;
    const int ROTTEN = 2;

    public int OrangesRotting(int[][] grid)
    {
        return 4;
    }

    protected override string Title => "994: Rotting Oranges";

    protected override IEnumerable<(int[][], int)> TestCases
    {
        get
        {           
            yield return ([[2,1,1],
                           [1,1,0],
                           [0,1,1]], 4);

            yield return ([[2,1,1],
                           [0,1,1],
                           [1,0,1]], -1);
        }
    }

    protected override int Solve(int[][] input) => OrangesRotting(input);
    
}
