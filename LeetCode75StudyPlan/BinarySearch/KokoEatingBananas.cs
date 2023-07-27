namespace LeetCode75StudyPlan.BinarySearch;

internal class KokoEatingBananas : Solution<(int[] piles, int h), int>
{
    public int MinEatingSpeedGen(int[] piles, int h)
    {        
        return BinSearcher(
            1, 
            piles.Max(),
            speed => piles.Sum(pile => ((pile - 1) / speed) + 1) <= h);

    }
    
    public int MinEatingSpeed(int[] piles, int h) // efficcent
    {

        int slowest = 1, fastest = piles.Max();
        while (slowest < fastest)
        {
            //int speed = slowest + (fastest - slowest) / 2;
            int speed = (slowest + fastest) >> 1;
            int total = 0;
            foreach (int pile in piles)
                total += ((pile - 1) / speed) + 1;

            if (total <= h)
            {
                fastest = speed;
            }
            else
            {
                slowest = speed + 1;
            }
        }
        return slowest;

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
