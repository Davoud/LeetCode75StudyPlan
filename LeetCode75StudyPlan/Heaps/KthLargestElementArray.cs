﻿namespace LeetCode75StudyPlan.Heaps;

internal class KthLargestElementArray : Solution<(int[] nums, int k), int>
{
    protected override string Title => "215.Kth Largest Element in an Array";
    
    public int FindKthLargest(int[] nums, int k)
    {
        var pq = new MaxPq(nums);       
        pq.Dequeue(k).Count();        
        return pq.Peek;
    }

    protected override IEnumerable<((int[] nums, int k), int)> TestCases
    {
        get
        {
            yield return (([3, 2, 1, 5, 6, 4], 2), 5);
            yield return (([3, 2, 3, 1, 2, 4, 5, 5, 6], 4), 4);
        }
    }

    protected override int Solve((int[] nums, int k) input) => FindKthLargest(input.nums, input.k);


}
