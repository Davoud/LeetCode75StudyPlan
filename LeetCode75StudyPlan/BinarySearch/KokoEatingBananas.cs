namespace LeetCode75StudyPlan.BinarySearch;

internal class KokoEatingBananas : Solution<(int[] piles, int h), int>
{

    public int MinEatingSpeed(int[] piles, int h)
    {
        BinarySearcher bs = new(k =>
        {
            // can eat all bananase in K per h?
            return false;
        });

        return bs.SearchIn(piles.Min(), piles.Max());
    }

    protected override string Title => "875. Koko Eating Bananas";

    protected override IEnumerable<((int[] piles, int h), int)> TestCases
    {
        get
        {
            yield return ( (@int[3, 6, 7, 11],        8),  4);
            yield return ( (@int[30, 11, 23, 4, 20],  5), 30);
            yield return ( (@int[30, 11, 23, 4, 20],  6), 23);
        }
    }

    protected override int Solve((int[] piles, int h) input) => MinEatingSpeed(input.piles, input.h);
    
}
