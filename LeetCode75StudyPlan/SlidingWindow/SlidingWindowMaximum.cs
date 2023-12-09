using System;
using System.Globalization;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace LeetCode75StudyPlan.SlidingWindow;

internal class SlidingWindowMaximum : ITestable
{
    void ITestable.RunTests()
    {
        "239. Sliding Window Maximum".WriteLine();

        ((int[] nums, int k), int[])[] cases =
        [
            ((nums: [-7,-8,7,5,7,1,6,0], k: 4), [7,7,7,7,7]),
            ((nums: [1,3,1,2,0,5],       k: 3), [3,3,2,5]),
            ((nums: [9,10,9,-7,-4,-8,2,-6], k: 5), [10, 10, 9, 2]),
            ((nums: [1,-1],              k: 1), [1,-1]),
            ((nums: [8,3,-1,-3,5,3,6,7], k: 3), [8,3,5,5,6,7]),
            ((nums: [1,3,-1,-3,5,3,6,7], k: 3), [3,3,5,5,6,7]),
            ((nums: [1, 0],              k: 2), [1]),
            ((nums: [4,3,3,1],           k: 4), [4]),
            ((nums: [-1,4,3,2,1],        k: 5), [4]),
            ((nums: [1],                 k: 1), [1]),
            ((nums: [10, 10, 10, 9, 9, 9, 8, 8, 8, 7, 7, 7, 5, 5, 5, 4, 4, 4, 3, 3, 3, 2, 2, 2, 1, 1, 1],
                                            k: 15), [10, 10, 10, 9, 9, 9, 8, 8, 8, 7, 7, 7, 5]),

        ];

        cases.RunTests(i => MaxSlidingWindow(i.nums, i.k), (a, b) => a.SequenceEqual(b));
    }

#pragma warning disable CS8602 // Dereference of a possibly null reference.
    public int[] MaxSlidingWindow(int[] nums, int k)
    {
        LinkedList<int> q = new();        
        var result = new int[nums.Length - k + 1];

        for (int i = 0; i < nums.Length; i++)
        {
            var index = i - k + 1;

            while (q.Count > 0 && q.First.Value < index)
                q.RemoveFirst();
                      
            while (q.Count > 0 && nums[q.Last.Value] < nums[i])
                q.RemoveLast();

            q.AddLast(i);

            if(index >= 0)
                result[index] = nums[q.First.Value];            
        }

        return result;
    }
#pragma warning restore CS8602 // Dereference of a possibly null reference.

    public int[] MaxSlidingWindow2(int[] nums, int k)
    {
        var res = new int[nums.Length - k + 1];
        var deque = new List<int>(nums.Length - 1);

        var first = 0;

        for (var i = 0; i < nums.Length; i++)
        {
            var num = nums[i];

            while (deque.Count > 0 && num > nums[deque[^1]])
                deque.RemoveAt(deque.Count - 1);

            deque.Add(i);

            if (i + 1 < k)
                continue;

            first = Math.Min(first, deque.Count - 1);

            if (deque[^1] - deque[first] == k)
                first++;

            res[i - k + 1] = Math.Max(nums[deque[first]], nums[deque[^1]]);
        }

        return res;
    }

    public int[] MaxSlidingWindow1(int[] nums, int k)
    {
        if (k == 1) return nums;

        int[] maxWin = new int[nums.Length - k + 1];

        (int p, int q) = MaxIndex(nums, 0, k);
        maxWin[0] = nums[p];

        for (int start = 1, end = k; end < nums.Length; start++, end++)
        {
            (int value, int max1, int max2) = (nums[end], nums[p], nums[q]);
            if (p == start - 1)
            {
                if (value < max2)
                    (p, q) = MaxIndex(nums, start, k);
                else
                    p = end;
            }
            else if (q == start - 1)
            {
                if (value < max2)
                    q = SecondMax(nums, start, end + 1, p);
                else if (max2 <= value && value <= max1)
                    q = end;
                else
                    (q, p) = (p, end);
            }
            else
            {
                if (max2 <= value && value <= max1)
                    q = end;
                else if (max1 < value)
                    (q, p) = (p, end);
            }

            maxWin[start] = nums[p];
        }

        return maxWin;
    }

    public (int, int) MaxIndex(int[] nums, int start, int len)
    {
        (int i1st, int max1st) = (-1, int.MinValue);
        (int i2nd, int max2nd) = (-1, int.MinValue);

        for (int i = start; i < start + len; i++)
        {
            if (max1st <= nums[i])
            {
                (i1st, max1st) = (i, nums[i]);
            }
        }

        for (int i = start; i < start + len; i++)
        {
            if (max2nd <= nums[i] && i != i1st)
            {
                (i2nd, max2nd) = (i, nums[i]);
            }
        }

        return (i1st, i2nd);
    }

    public int SecondMax(int[] nums, int start, int end, int except)
    {
        (int i2nd, int max2nd) = (-1, int.MinValue);
        for (int i = start; i < end; i++)
        {
            if (i == except) continue;
            if (max2nd <= nums[i])
            {
                (i2nd, max2nd) = (i, nums[i]);
            }
        }
        return i2nd;
    }

}
