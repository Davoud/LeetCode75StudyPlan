
namespace LeetCode75StudyPlan.Stack;

public record Ops((StackOp op, int value)[] OpSeq)
{
    public IEnumerable<string> RunOn(MinStack m)
    {
        var result = new List<string>();
        int v = 0;
        foreach ((StackOp op, int value) in OpSeq)
        {
            switch (op)
            {
                case StackOp.Push:
                    m.Push(value);
                    break;
                case StackOp.Pop:
                    m.Pop();
                    break;
                case StackOp.Top:
                    v = m.Top();
                    if (v != value)
                    {
                        result.Add($"{op} Expected {value}, receved {v}");
                    }
                    break;
                case StackOp.GetMin:
                    v = m.GetMin();
                    if (v != value)
                    {
                        result.Add($"{op} Expected {value}, receved {v}");
                    }
                    break;
            }
        }

        return result;
    }

    public override string ToString()
    {
        return string.Join(" | ", OpSeq.Select(i => i.op == StackOp.Pop ? $"{i.op}()" : $"{i.op}({i.value})"));
    }
}

public enum StackOp
{
    Push,
    Pop,
    Top,
    GetMin,
}
