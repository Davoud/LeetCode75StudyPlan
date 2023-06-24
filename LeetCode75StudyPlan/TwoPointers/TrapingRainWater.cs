using static System.Net.Mime.MediaTypeNames;

namespace LeetCode75StudyPlan.TwoPointers;

internal static class TrapingRainWater
{

    public static void RunTests()
    {
        Console.WriteLine("42. Trapping Rain Water");

        new[]
        {
           ( Arr(0,1,0,2,1,0,1,3,2,1,2,1), 6 ),
           ( Arr(4,2,0,3,2,5), 9 ),
           ( Arr(4,3,5), 1),
           ( Arr(4,0,1,0,2,0,1,0,4), 24),
           ( Arr(4,1,2,1,4), 8),
        }
        .RunTests(Trap);
    }

    public static int Trap(int[] height)
    {
        (int trap, int @base, int sum) = (0, 0, 0);
        
        for (int i = 0; i < height.Length; i++)
        {
            if (height[i] >= @base)
            {
                trap += sum;
                sum = 0;
                @base = height[i];
            }
            else
            {
                sum += @base - height[i];
            }
        }

        (@base, sum) = (0, 0);        
        for (int i = height.Length - 1; i >= 0; i--)
        {
            if (height[i] > @base)
            {
                trap += sum;
                sum = 0;
                @base = height[i];
            }
            else 
            {
                sum += @base - height[i];
            }
        }

        return trap;
    }

}
