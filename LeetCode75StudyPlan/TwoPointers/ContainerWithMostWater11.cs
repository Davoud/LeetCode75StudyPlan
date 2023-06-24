using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode75StudyPlan.TwoPointers;

internal static class ContainerWithMostWater11
{

    public static void RunTests()
    {
        var testCases = new[]
        {
            (Arr(1, 8, 6, 2, 5, 4, 8, 3, 7), 49),
            (Arr(1, 2, 3, 1000, 900, 2, 2, 5), 900),
            (Arr(1, 1, 1, 1, 1000, 2, 1500, 5), 2000),
            (Arr(1, 1), 1),
        };

        testCases.RunTests(MaxArea, (a, b) => a == b);
    }

    public static int MaxArea(int[] height)
    {
        (int p, int q) = (0, height.Length - 1);
        int max = 0;

        while (p < q)
        {
            int area = Math.Min(height[p], height[q]) * (q - p);
            
            if (height[p] > height[q])
                q--;
            else
                p++;
            
            if (area > max) max = area;
        }

        return max;
    }

    public static int _MaxArea(int[] height)
    {
        int max = 0;
        for(int i = 0; i < height.Length - 1; i++)
        {
            for(int j = i + 1; j < height.Length; j++)
            {                
                int result = Math.Min(height[i], height[j]) * (j - i);
                if(result > max)
                {
                    max = result;
                }
            }
        }
        return max;
    }
}

