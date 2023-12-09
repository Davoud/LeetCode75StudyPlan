using System;

namespace LeetCode75StudyPlan.Stack;

internal class CarFleeter : ITestable
{
    public int CarFleet(int target, int[] position, int[] speed)
    {
        var stack = new Stack<double>();
        Array.Sort(position, speed);
        for (int i = 0; i < position.Length; i++)
        {
            var current = (double)(target - position[i]) / speed[i];
            while (stack.TryPeek(out double peek) && peek <= current)
                stack.Pop();
            stack.Push(current);
        }
        return stack.Count;
    }

    public int CarFleetSpan(int target, int[] position, int[] speed)
    {        
        Span<double> stack = stackalloc double[position.Length];
        int stackIndex = -1;
            
        Array.Sort(position, speed);
        for (int i = 0; i < position.Length; i++)
        {
            var current = (double)(target - position[i]) / speed[i];
            while (stackIndex != -1 &&  stack[stackIndex] <= current)            
                stackIndex--; // pop                                           
            stack[++stackIndex] = current; //push              
        }
        return stackIndex+1;
    }

    private record struct TestCase(int Target, int[] Position, int[] Speed);

    void ITestable.RunTests()
    {
        "853. Car Fleet".WriteLine();
               
        var cases = new (TestCase input, int output)[]
        {
            (new(12,  [10,8,0,5,3], [2,4,1,1,3]),   3),
            (new(10,  [3],          [3]),           1),
            (new(100, [0,2,4],      [4,2,1]),       1),
        };

        cases.RunTests(i => CarFleet(i.Target, i.Position, i.Speed));
    }
}
