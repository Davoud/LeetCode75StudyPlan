namespace LeetCode75StudyPlan.Stack;

internal class LargestRectangleInHistogram : ITestable
{

    public int LargestRectangleArea(int[] heights)
    {
        return heights.Length;
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
