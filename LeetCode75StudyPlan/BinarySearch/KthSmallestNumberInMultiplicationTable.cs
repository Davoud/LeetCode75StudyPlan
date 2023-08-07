namespace LeetCode75StudyPlan.BinarySearch;

internal class KthSmallestNumberInMultiplicationTable : Solution<(int m, int n, int k), int>
{
    public int FindKthNumber1(int m, int n, int k)
    {
        return BinSearcher(1, m * n, ThereAreKValuesLessThanOrEqualTo);
                
        bool ThereAreKValuesLessThanOrEqualTo(int num) 
        {
            int count = 0;
            for(int val = 1; val < m + 1; val++)
            {
                int add = Math.Min(num / val, n);
                if (add == 0) break;
                count += add;
            }
            return count >= k;
        }
    }

    public int FindKthNumber(int m, int n, int k)
    {
        int left = 1, right = m * n;
        while (left < right)
        {
            int mid = (left + right) >> 1;
            if(ThereAreKValuesLessThanOrEqualTo(mid)) 
                right = mid;
            else
                left = mid + 1;
        }

        return left;

        bool ThereAreKValuesLessThanOrEqualTo(int num)
        {
            int count = 0;
            for (int val = 1; val < m + 1; val++)
            {
                int add = Math.Min(num / val, n);
                if (add == 0) break;
                count += add;
            }
            return count >= k;
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
