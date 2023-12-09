using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace LeetCode75StudyPlan.TwoPointers;

internal static class TwoSumSortedArray167
{
    public static void RunTests()
    {
        ((int[] numbers, int target), int[])[] testCases =
        [
            (([-1, 0], -1), [1, 2]),
            (([3, 24, 50, 79, 88, 150, 345], 200), [3, 6]),
            (([-2, -1, 0, 1], 0), [2, 4]),
            (([2, 7, 11, 15], 9), [1, 2]),
            (([2, 3, 4], 6), [1, 3]),
        ];

        testCases.RunTests(i => TwoSum(i.numbers, i.target), Enumerable.SequenceEqual);
    }

    public static int[] TwoSum(int[] numbers, int target)
    {
        for (int i = 0; i < numbers.Length - 1; i++)
        {
            int result = Array.BinarySearch<int>(numbers, i + 1, numbers.Length - i - 1, target - numbers[i]);
            if (result >= 0)
            {
                return new int[] { i + 1, result + 1 };
            }
        }
        return Array.Empty<int>();
    }

    public static int[] TwoSum1(int[] numbers, int target)
    {
        int i = 0;
        while (i < numbers.Length - 1)
        {
            int j = i + 1;
            while (j < numbers.Length)
            {
                int sum = numbers[i] + numbers[j];
                if (sum == target)
                    return [i + 1, j + 1];

                j++;
            }
            i++;
        }
        return Array.Empty<int>();
    }



    public static void Run3SumTests()
    {
        Console.WriteLine("Testing 15. 3Sum");

        (int[], IList<IList<int>>)[] testCases =
        [
            ([-1, 0, 1, 2, -1, -4], List(List(-1, -1, 2), List(-1, 0, 1))),
            ([0, 1, 1], List<IList<int>>()),
            ([0, 0, 0], List(List(0, 0, 0))),
        ];


        testCases.RunTests(ThreeSum, (actual, expected) =>
        {
            var setA = actual.Select(Enumerable.ToHashSet);
            var setB = expected.Select(Enumerable.ToHashSet);

            return
                setB.Aggregate(true, (any, b) => any && setA.Any(a => a.SetEquals(b))) &&
                setA.Aggregate(true, (any, a) => any && setB.Any(b => b.SetEquals(a)));
        });
    }


    public class Comparer : IEqualityComparer<IList<int>>
    {
        public bool Equals(IList<int>? x, IList<int>? y)
        {
            return x is not null && y is not null ? x.SequenceEqual(y) : x is null && y is null;
        }

        public int GetHashCode([DisallowNull] IList<int> obj)
        {
            return string.Join("|", obj).GetHashCode();
        }
    }

    public record Triple
    {
        public readonly int a;
        public readonly int b;
        public readonly int c;
        public Triple(int first, int second, int third)
        {
            (a, b, c) = (first, second, third);
            if (a > b) (a, b) = (b, a);
            if (b > c) (b, c) = (c, b);
            if (a > b) (a, b) = (b, a);
        }
        public IList<int> ToList() => new List<int> { a, b, c };
    }

    public static IList<IList<int>> ThreeSum(int[] nums)
    {
        Array.Sort(nums);
        ISet<Triple> set = new HashSet<Triple>();
        for (int i = 0; i < nums.Length - 2; i++)
        {
            for (int j = i + 1; j < nums.Length - 1; j++)
            {
                var k = Array.BinarySearch<int>(nums, j + 1, nums.Length - j - 1, (-1) * (nums[i] + nums[j]));
                if (k > 0)
                {
                    set.Add(new Triple(nums[i], nums[j], nums[k]));
                }
            }
        }
        return set.Select(t => t.ToList()).ToList();
    }

    public static IList<IList<int>> __ThreeSum(int[] nums)
    {
        ISet<Triple> set = new HashSet<Triple>();
        for (int i = 0; i < nums.Length - 2; i++)
        {
            for (int j = i + 1; j < nums.Length - 1; j++)
            {
                for (int k = j + 1; k < nums.Length; k++)
                {
                    if (nums[i] + nums[j] + nums[k] == 0)
                    {
                        set.Add(new Triple(nums[i], nums[j], nums[k]));
                    }
                }
            }
        }
        return set.Select(t => t.ToList()).ToList();
    }



    public static IList<IList<int>> _ThreeSum(int[] nums)
    {
        ISet<IList<int>> result = new HashSet<IList<int>>(new Comparer());

        var byValue = nums.Select((value, index) => new { value, index })
            .ToLookup(k => k.value, i => i.index);

        for (int i = 0; i < nums.Length - 1; i++)
        {
            for (int j = i + 1; j < nums.Length; j++)
            {
                int key = (-1) * (nums[i] + nums[j]);

                if (byValue.Contains(key))
                {
                    foreach (int index in byValue[key])
                    {
                        if (index > i && index > j)
                        {
                            var list = new List<int> { nums[i], nums[j], key };
                            list.Sort();
                            result.Add(list);
                        }
                    }
                }
            }
        }

        return result.ToList();
    }
}
