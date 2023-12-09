namespace LeetCode75StudyPlan.LinkLists;

/*
 * Given an array of integers nums containing n + 1 integers where each 
 * integer is in the range [1, n] inclusive. There is only one repeated 
 * number in nums, return this repeated number. 
 * You must solve the problem without modifying the array nums and uses 
 * only constant extra space.
 */

internal class FindDublicateNumber: Solution<int[],int>
{
    protected override string Title => "287. Find the Duplicate Number";

    protected override int Solve(int[] nums)
    {
        return FindDuplicateFastSlow(nums);
    }

    public int FindDuplicateFastSlow(int[] nums)
    {
        (int s, int f) = (0,0);        
        do (s, f) = (nums[s], nums[nums[f]]); while (s != f);
        s = 0;
        while (s != f) (s, f) = (nums[s], nums[f]);        
        return s;
    }
        
    public int FindDuplicateModifyInput(int[] nums)
    {        
        foreach (int num in nums)
        {
            int i = Math.Abs(num);
            if (nums[i] < 0) return i;            
            nums[i] = -nums[i];
        }
        return nums.Length;
    }

    public int FindDuplicateBinarySearch(int[] nums)
    {
        int len = nums.Length;
        int low = 1;
        int high = len - 1;
        while (low < high)
        {
            int mid = low + (high - low) / 2;
            int cnt = 0;
            for (int i = 0; i < len; i++)
            {
                if (nums[i] <= mid)
                {
                    cnt++;
                }
            }

            if (cnt <= mid)
            {
                low = mid + 1;
            }
            else
            {
                high = mid;
            }
        }

        return low;
    }

    protected override IEnumerable<(int[], int)> TestCases
    {
        get
        {
            yield return ([1, 3, 4, 2, 2], 2);
            yield return ([3, 1, 3, 4, 2], 3);
            yield return ([4, 2, 3, 1, 4], 4);
        }
    }        
}
