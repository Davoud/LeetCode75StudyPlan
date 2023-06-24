using System.Collections;

namespace LeetCode75StudyPlan.Stack;

public class MinStack : ITestable, IEnumerable<int>
{
    private Stack<int> _stack;
    private Stack<int> _mins;

    public MinStack()
    {
        _stack = new Stack<int>();
        _mins = new Stack<int>();
    }

    public void Push(int val)
    {
        _stack.Push(val);
        if (_mins.Count == 0)
        {
            _mins.Push(val);
        }
        else if (val <= _mins.Peek())
        {            
            _mins.Push(val);
        }
    }

    public void Pop()
    {
        int value = _stack.Pop();
        if (_mins.TryPeek(out var head) && head == value)
        {
            _mins.Pop();
        }
    }

    public int Top()
    {
        return _stack.Peek();
    }

    public int GetMin()
    {
        return _mins.Peek();
    }



    void ITestable.RunTests()
    {
        "155. Min Stack".WriteLine();

        var cases = new Ops[]
        {
            new Ops(new[] { 
                (StackOp.Push, -2),
                (StackOp.Push, 0),
                (StackOp.Push, -3),
                (StackOp.GetMin, -3),
                (StackOp.Pop, 0),
                (StackOp.Top, 0),
                (StackOp.GetMin, -2),
            }),

            new Ops(new[]
            {
                (StackOp.Push, 0),
                (StackOp.Push, 1),
                (StackOp.Push, 0),
                (StackOp.GetMin, 0), 
                (StackOp.Pop, 0),
                (StackOp.GetMin, 0),
            })
        };

        foreach (var op in cases)
        {
            Console.WriteLine(op.ToString());
            var m = new MinStack();
            foreach (var result in op.RunOn(m))
            {
                Console.WriteLine(result);
            }

        }

    }

    public IEnumerator<int> GetEnumerator()
    {
        return _mins.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
