
using System.Reflection.Metadata.Ecma335;

namespace LeetCode75StudyPlan.BinarySearch;

internal record BinarySearcher(Predicate<int> Condition)
{        
    public int SearchIn(int left, int right)
    {        
        while(left < right)
        {
            int mid = left + (right - left) / 2;
            if (Condition(mid))
            {
                right = mid;
            }
            else
            {
                left = mid + 1;
            }
        }
        
        return left;
    }

}

internal record BS(int Min, int Max, IComparable<int> Cmp)
{
    public int? Search()
    {
        int left = Min;
        int right = Max;
        while (left < right)
        {
            int mid = left + (right - left) / 2;
            int cmp = Cmp.CompareTo(mid);
            
            if(cmp < 0)
            {
                right = mid;
            }
            else if(cmp > 0)
            {
                left = mid + 1;
            }
            else
            {
                return mid;
            }
        }

        return null;
    }
}