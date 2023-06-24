﻿using System;
using System.Globalization;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace LeetCode75StudyPlan.SlidingWindow;

internal class SlidingWindowMaximum : ITestable
{
    void ITestable.RunTests()
    {
        "239. Sliding Window Maximum".WriteLine();

        ((int[] nums, int k), int[])[] cases = new[]
        {
            ((nums: Arr(10, 10, 10, 9, 9, 9, 8, 8, 8, 7, 7, 7, 5, 5, 5, 4, 4, 4, 3, 3, 3, 2, 2, 2, 1, 1, 1),
                k: 15), Arr(10, 10, 10, 9, 9, 9, 8, 8, 8, 7, 7, 7, 5)),
            
            ((nums: Arr(-7,-8,7,5,7,1,6,0), k: 4), Arr(7,7,7,7,7)),
            ((nums: Arr(1,3,1,2,0,5),       k: 3), Arr(3,3,2,5)),
            ((nums: Arr(9,10,9,-7,-4,-8,2,-6), k: 5), Arr(10, 10, 9, 2)),
            ((nums: Arr(1,-1),              k: 1), Arr(1,-1)),
            ((nums: Arr(8,3,-1,-3,5,3,6,7), k: 3), Arr(8,3,5,5,6,7)),
            ((nums: Arr(1,3,-1,-3,5,3,6,7), k: 3), Arr(3,3,5,5,6,7)),
            ((nums: Arr(1, 0),              k: 2), Arr(1)),
            ((nums: Arr(4,3,3,1),           k: 4), Arr(4)),
            ((nums: Arr(-1,4,3,2,1),        k: 5), Arr(4)),
            ((nums: Arr(1),                 k: 1), Arr(1)),
            
        };

        cases.RunTests(i => MaxSlidingWindow(i.nums, i.k), (a, b) => a.SequenceEqual(b));
    }

    

    public int[] MaxSlidingWindow(int[] nums, int k)
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
