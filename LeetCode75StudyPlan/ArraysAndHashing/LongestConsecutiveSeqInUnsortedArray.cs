namespace LeetCode75StudyPlan.ArraysAndHashing;

internal static class LongestConsecutiveSeqInUnsortedArray
{
    public static class Tests
    {
        public static void Run()
        {
            Console.WriteLine("128. Longest Consecutive Sequence");
            var testCases = new List<(int[] input, int result)>
            {
                (Arr(9, 1, 4, 7, 3, -1, 0, 5, 8, -1, 6), 7),
                (Arr(100, 4, 200, 1, 3, 2), 4),
                (Arr(0, 3, 7, 2, 5, 8, 4, 6, 0, 1), 9),
                (Arr(10, 20, 30), 1),
                (Arr(10, -3, -2, 20, 0, -1, 1), 5),
                (Arr(0), 1),
                (Arr<int>(), 0),                
            };

            testCases.RunTests(LongestConsecutive, (a, b) => a == b);
        }
    }

    public static int LongestConsecutive(int[] nums)
    {
        HashSet<int> set = new(nums);
        int max = 0;
        foreach (var num in nums)
        {
            if (!set.Contains(num - 1))
            {
                int x = num + 1;
                while (set.Contains(x)) x++;                
                max = Math.Max(max, x - num);
            }
        }
        return max;
    }

    public static int _LongestConsecutive(int[] nums)
    {
        if (nums.Length == 0) return 0;
        if (nums.Length == 1) return 1;

        Array.Sort(nums);
        
        int cons = 1;
        int max = 1;
      
        for(int i = 1; i < nums.Length; i++)
        {            
            if (nums[i - 1] == nums[i] - 1)
            {                
                max = Math.Max(max, ++cons);
            }
            else if(nums[i - 1] != nums[i])
            {
                cons = 1;
            }            
        }

        return max;
    }
}
