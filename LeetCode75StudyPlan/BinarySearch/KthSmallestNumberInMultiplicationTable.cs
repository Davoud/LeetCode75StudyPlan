namespace LeetCode75StudyPlan.BinarySearch;

internal class KthSmallestNumberInMultiplicationTable : Solution<(int m, int n, int k), int>
{
    public int FindKthNumber(int m, int n, int k)
    {
        return BinSearcher(1, m * n, ThereAreKValuesLessThanOrEqualTo);
                
        bool ThereAreKValuesLessThanOrEqualTo(int num) 
        {
            return n == 0 && k == 0;
        }

    }
    protected override string Title => "668. Kth Smallest Number in Multiplication Table";

    protected override IEnumerable<((int m, int n, int k), int)> TestCases
    {
        get
        {
            yield return ((3, 3, 5), 3);
            yield return ((2, 3, 6), 6);
        }
    }

    protected override int Solve((int m, int n, int k) input) => FindKthNumber(input.m, input.n, input.k);
    
}
