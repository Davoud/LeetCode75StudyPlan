namespace LeetCode75StudyPlan.Stack;

internal class LargestRectangleInHistogram : ITestable
{

    public int LargestRectangleArea(int[] heights)
    {       
        int len = heights.Length;
        if(len == 0) return 0;   

        var left = new int[len];
        left[0] = -1;

        var right = new int[len];
        right[^1] = len;

        for (int i = 1; i < len; i++)
        {
            int p = i - 1;
            while (p >= 0 && heights[p] >= heights[i])
                p = left[p];            
            left[i] = p;
        }

        for (int i = len - 2; i >= 0; i--)
        {
            int p = i + 1;
            while (p < len && heights[p] >= heights[i])
                p = right[p];            
            right[i] = p;
        }
       
        //for(int i = 0; i < len; i++)
        //{
        //    $"left[{i}] = {left[i]} | right[{i}] = {right[i]}".WriteLine();  
        //}

        int maxArea = 0;
        for (int i = 0; i < len; i++)
        {
            maxArea = Math.Max(maxArea, heights[i] * (right[i] - left[i] - 1));
            //$"{maxArea} = {heights[i]} * ({right[i]} - ({left[i]}) - 1)".WriteLine();
        }

        return maxArea;

    }

    void ITestable.RunTests()
    {
        "84. Largest Rectangle in Histogram".WriteLine();

        var cases = new[]
        {
            (@int[2, 1, 5, 6, 2, 3], 10),
            (@int[2, 4],              4),
        };

        cases.RunTests(LargestRectangleArea);
    }
}
