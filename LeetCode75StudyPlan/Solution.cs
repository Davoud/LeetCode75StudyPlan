
using BenchmarkDotNet.Attributes;
using System.Numerics;
using System.Reflection;

namespace LeetCode75StudyPlan;
public abstract class Solution<T, R> : ITestable
{
    protected abstract string Title { get; }
    protected abstract R Solve(T input);
    protected abstract IEnumerable<(T, R)> TestCases { get; }
    protected virtual bool IsEqual(R actual, R expected)
    {                
        if(actual is not null && expected is not null)
        {
            if (expected is IEnumerable<int> lb)
            {
                if(actual is ISet<int> intSet)
                {
                    return intSet.SetEquals(lb);
                }
                else if(actual is IEnumerable<int> la)
                {
                    return la.SequenceEqual(lb);
                }
                else
                {
                    return false;
                }
            }
            else if (expected is IEnumerable<R> b)
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
            else if(actual is IList<IList<int>> lla && expected is IList<IList<int>> llb)
            {                
                if (lla.Count == llb.Count)
                {
                    var set = llb.Select(i => i.ToHashSet()).ToHashSet();
                    foreach (var item in lla)
                    {
                        if (!set.Any(i => i.SetEquals(item)))
                        {
                            return false;
                        }
                    }
                    return true;
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
        Console.WriteLine(Environment.NewLine + Title);
        TestCases.RunTests(Solve, IsEqual);
    }

}
