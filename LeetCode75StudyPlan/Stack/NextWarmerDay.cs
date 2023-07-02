namespace LeetCode75StudyPlan.Stack;
public class NextWarmerDay : ITestable
{
    // unnecessary extra memory
    public int[] DailyTemperatures1(int[] temperatures)
    {
        var result = new int[temperatures.Length];

        var s = new Stack<(int value, int index)>();

        for (int index = 0; index < temperatures.Length; index++)
        {
            int value = temperatures[index];
            while (s.TryPeek(out var peek) && value > peek.value)
            {
                s.Pop();
                result[peek.index] = index - peek.index;
            }

            s.Push((value, index));
        }

        return result;
    }

    // better memory
    public int[] DailyTemperatures2(int[] temperatures)
    {
        var result = new int[temperatures.Length];

        var s = new Stack<int>();

        for (int index = 0; index < temperatures.Length; index++)
        {
            int value = temperatures[index];
            while (s.TryPeek(out var peek) && value > temperatures[peek])
            {
                s.Pop();
                result[peek] = index - peek;
            }

            s.Push(index);
        }

        return result;
    }

    public int[] DailyTemperatures(int[] temperatures)
    {
        var result = new int[temperatures.Length];

        Span<int> stack = stackalloc int[temperatures.Length];
        int stackIndex = -1;

        for (int index = 0; index < temperatures.Length; index++)
        {
            while (stackIndex != -1 && temperatures[index] > temperatures[stack[stackIndex]])
            {
                int peek = stack[stackIndex--]; // pop                
                result[peek] = index - peek;
            }

            stack[++stackIndex] = index; //push           
        }

        return result;
    }



    void ITestable.RunTests()
    {
        "739. Daily Temperatures".WriteLine();

        var cases = new[]
        {
            (@int[73, 74,75,71,69,72,76,73], @int[1,1,4,2,1,1,0,0]),
            (@int[30,40,50,60], @int[1,1,1,0]),
            (@int[30,60,90], @int[1,1,0]),
            (@int[55,38,53,81,61,93,97,32,43,78], @int[3,1,1,2,1,1,0,1,1,0]),
        };

        cases.RunTests(DailyTemperatures, (a, b) => a.SequenceEqual(b));

    }
}
