namespace LeetCode75StudyPlan.ArraysAndHashing;

public class ArrayProductExceptSelf
{
    public static class Tests
    {
        public static void Run()
        {
            Console.WriteLine("- - - - Case 1 - - - -");
            var input = new int[] { 1, 2, 3, 4 };
            input.Dump();
            new ArrayProductExceptSelf().ProductExceptSelf(input).Dump();

            Console.WriteLine("- - - - Case 2 - - - -");
            input = new int[] { -1, 1, 0, -3, 3 };
            input.Dump();
            new ArrayProductExceptSelf().ProductExceptSelf(input).Dump();
        }
    }

    public int[] ProductExceptSelf(int[] nums)
    {
        int n = nums.Length;       
        var forward = new int[n];
        var backward = new int[n];

        forward[0] = 1;
        backward[n - 1] = 1;

        for (int f = 1; f < n; f++)
        {
            forward[f] = forward[f - 1] * nums[f - 1];           
            backward[n - f - 1] = backward[n - f] * nums[n - f];
        }
        
        for (int i = 0; i < n; i++)
            forward[i] *= backward[i];

        return forward;
    }

    public int[] _ProductExceptSelf(int[] nums)
    {
        int n = nums.Length;
        var result = new int[n];
        var forward = new int[n];
        var backward = new int[n];
        
        forward[0] = 1;
        backward[n - 1] = 1;

        for (int i = 1; i < n; i++)
            forward[i] = forward[i - 1] * nums[i - 1]; 
                
        for (int i = n - 1; i > 0; i--)
            backward[i - 1] = backward[i] * nums[i];
                        
        for(int i = 0; i < n; i++)
            result[i] = forward[i] * backward[i];
        
        return result;
    }
}