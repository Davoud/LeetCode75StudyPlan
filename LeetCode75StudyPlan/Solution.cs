
using System.Numerics;
using System.Reflection;

namespace LeetCode75StudyPlan;

internal abstract class Solution<T, R> : ITestable
{
    protected abstract string Title { get; }
    protected abstract R Solve(T input);
    protected abstract IEnumerable<(T, R)> TestCases { get; }
    protected virtual bool IsEqual(R actual, R expected)
    {                
        if(actual is not null && expected is not null)
        {
            if (expected is IEnumerable<R> b)
            {
                if (actual is ISet<R> set)
                {
                    return set.SetEquals(b);
                }
                else if (actual is IEnumerable<R> a)
                {
                    return a.SequenceEqual(b);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return actual.Equals(expected);
            }
        }
        else
        {
            return actual is null && expected is null;
        }
    }

    void ITestable.RunTests()
    {
        Console.WriteLine(Title);
        TestCases.RunTests(Solve, IsEqual);
    }

}
