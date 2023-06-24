namespace LeetCode75StudyPlan.ArraysAndHashing
{
    static class PivotIndex
    {

        public static int Of(int[] nums)
        {
            int left = 0, right = 0;
            foreach (int value in nums) right += value;

            for (int i = 0; i < nums.Length; i++)
            {
                right -= nums[i];
                left += i > 0 ? nums[i - 1] : 0;
                if (right == left) return i;
            }

            return -1;
        }

        public static IEnumerable<(int[] input, int expected)> Samples = new List<(int[] input, int expected)>
        {
            (new[] { 1, 7, 3, 6, 5, 6 }, 3),
            (new[] { -1, -1, -1, -1, -1, 0 }, 2),
            (new[] { 1, 2, 3 }, -1),
            (new[] { 2, -1, 1 }, 0),
            (new[] { 1, 2, 3, 4, 3, 3 }, 3),
            (new[] { 5, 4, 3, 2, 1, 0, 15 }, 5),
            (new[] { -10, 1, 4, 5, 10 }, 4),
            (new[] { 1, 1, -10, 1, 1, 0 }, 2),
        };

        public static void RunTests()
        {
            foreach (var (input, expected) in Samples)
            {
                var index = Of(input);
                if (index != expected)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Exptected: {expected}, Actual: {index} \t {input.AsStr()}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"Exptected: {expected}, Actual: {index} \t {input.AsStr()}");
                }
                Console.ResetColor();
            }

        }
    }
}
