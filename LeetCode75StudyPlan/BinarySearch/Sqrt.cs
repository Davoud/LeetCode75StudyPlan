namespace LeetCode75StudyPlan.BinarySearch;

internal class Sqrt : Solution<int, int>
{
    protected override string Title => "69. Sqrt(x)";

    protected override IEnumerable<(int, int)> TestCases
    {
        get
        {
            yield return (4, 2);
            yield return (8, 2);
        }
    }

    protected override int Solve(int x)
    {        
        return new BinarySearcher(i => i * i > x).SearchIn(0, x) - 1;
    }
}
