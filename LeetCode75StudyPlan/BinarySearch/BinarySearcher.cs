

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

