namespace LeetCode75StudyPlan.BinarySearch;
/*
 * Given an integer array bloomDay, an integer m and an integer k. 
 * We need to make m bouquets. To make a bouquet, you need to use k 
 * adjacent flowers from the garden. The garden consists of n flowers, 
 * the ith flower will bloom in the bloomDay[i] and then can be used 
 * in exactly one bouquet. Return the minimum number of days you need 
 * to wait to be able to make m bouquets from the garden. 
 * If it is impossible to make m bouquets return -1.
 */

internal class MinimumNumberOfDaysToMakeBouquets : Solution<(int[] bloomDay, int m, int k), int>
{
    // submitable
    public int MinDays(int[] bloomDay, int m, int k) 
    {
        if (bloomDay.Length < m * k) return -1;

        int min = 1, max = bloomDay.Max();
        while (min < max)
        {
            int mid = (max + min) >> 1; 
            if (CanMakeBouquetsAfter(mid)) max = mid; else min = mid + 1;            
        }

        return min;

        bool CanMakeBouquetsAfter(int days)
        {
            int bouguets = 0, flowers = 0;
            foreach (int bloom in bloomDay)
            {
                if (bloom > days)
                {
                    flowers = 0;
                }
                else
                {
                    bouguets += (flowers + 1) / k;
                    flowers = (flowers + 1) % k;
                }
            }
            return bouguets >= m;
        }
    }

    public int MinDaysGen(int[] bloomDay, int m, int k)
    {

        if (bloomDay.Length < m * k) return -1;

        return BinSearcher(1, bloomDay.Max(), CanMakeBouquetsAtDay);
        
        bool CanMakeBouquetsAtDay(int days)
        {            
            int bouguets = 0, count = 0;

            foreach (int bloom in bloomDay)
            {
                if (bloom <= days)
                {
                    count++;
                    if (count >= k)
                    {
                        bouguets++;
                        count = 0;
                        if (bouguets >= m) return true;
                    }
                }
                else
                {
                    count = 0;
                }
            }
            return false;
        }
    }

    protected override string Title => "1482. Minimum Number of Days to Make m Bouquets";

    protected override IEnumerable<((int[] bloomDay, int m, int k), int)> TestCases
    {
        get
        {
            yield return ((@int[7, 7, 7, 7, 12, 7, 7], 2, 3), 12);
            yield return ((@int[1, 10, 3, 10, 2], 3, 1), 3);
            yield return ((@int[1, 10, 3, 10, 2], 3, 2), -1);
        }
    }

    protected override int Solve((int[] bloomDay, int m, int k) input)
        => MinDays(input.bloomDay, input.m, input.k);
    
}
