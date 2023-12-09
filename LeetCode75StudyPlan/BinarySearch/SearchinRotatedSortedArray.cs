using System.ComponentModel.Design;

namespace LeetCode75StudyPlan.BinarySearch;

internal class SearchinRotatedSortedArray : Solution<(int[] nums, int target), int>
{
    public int Search1(int[] nums, int target) // fails some tests
    {
        (int l, int h) = (0, nums.Length - 1);
        int n0 = nums[0];
        while(l < h)
        {
            int m = l + ((h - l) / 2);
            int n = nums[m];
            if(target < n0 && n0 < n)
            {
                l = m + 1;
            }
            else if(target >= n0 && n0 > n)
            {
                h = m;
            }
            else if (n < target)
            {
                l = m + 1;
            }
            else if (n > target) 
            {
                h = m;
            }
            else
            {
                return m;
            }
            
        }
        return -1;
    }

    public int Search(int[] nums, int target)
    {
        (int min, int hi) = (0, nums.Length - 1);
        while (nums[min] > nums[hi])
        {
            int mid = min + ((hi - min) / 2);
            if (nums[mid] == target)
            {
                return mid;
            }
            else if (nums[mid] < nums[min])
            {
                hi = mid;
            }
            else
            {
                min = mid + 1;
            }
        }
          
        if(target <= nums[^1])
        {
            return BinSearch(min, nums.Length - 1);
        }
        else
        {
            return BinSearch(0, min);
        }

        int BinSearch(int left, int right)
        {
            while(left <= right)
            {
                int m = left + ((right - left) / 2);
                int value = nums[m];
                if(value < target)
                {
                    left = m + 1;
                } 
                else if (value > target)
                {
                    right = m - 1;
                }
                else
                {
                    return m;
                }
            }
            return -1;
        }
    }

    protected override string Title => "33. Search in Rotated Sorted Array";

    protected override IEnumerable<((int[] nums, int target), int)> TestCases
    {
        get
        {
            yield return (([4, 5, 6, 7, 0, 1, 2], 0), 4);
            yield return (([0, 1, 2, 5, 10, 11, 12], 10), 4);
            yield return (([-1, 1, 2, 5, 10, 11, 12], -1), 0);
            yield return (([4, 5, 6, 7, 0, 1, 2], 3), -1);
            yield return (([1], 0), -1);
            yield return (([3, 1], 1), 1);
        }
    }

    protected override int Solve((int[] nums, int target) input) => Search(input.nums, input.target);
    
}
