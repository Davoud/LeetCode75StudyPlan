namespace LeetCode75StudyPlan.Stack;

public class ReversePolishEvaluator : ITestable
{
    void ITestable.RunTests()
    {
        "150. Evaluate Reverse Polish Notation".WriteLine();

        var cases = new[]
        {
            (Arr("2","1","+","3","*"), 9),
            (Arr("4","13","5","/","+"), 6),
            (Arr("10","6","9","3","+","-11","*","/","*","17","+","5","+"), 22)
        };

        cases.RunTests(EvalRPN);
    }
    
    private IReadOnlyDictionary<string, Func<int, int, int>> ops =
        new Dictionary<string, Func<int, int, int>>
        {
           { "+", (a, b) => a + b },
           { "-", (a, b) => b - a },
           { "*", (a, b) => a * b },
           { "/", (a, b) => b / a },
        };

    public int EvalRPN1(string[] tokens)
    {
        var acc = new Stack<int>();
        foreach (var token in tokens)
        {
            if (ops.TryGetValue(token, out var @operator))
            {               
                acc.Push(@operator(acc.Pop(), acc.Pop()));
            }
            else
            {
                acc.Push(int.Parse(token));
            }
        }
        return acc.Peek();
    }

    public int EvalRPN(string[] tokens)
    {
        var acc = new Stack<int>();
        int temp;
        foreach (var token in tokens)
        {
            if (token.Length == 1)
            {
                switch (token)
                {
                    case "+":
                        acc.Push(acc.Pop() + acc.Pop());
                        continue;

                    case "*":
                        acc.Push(acc.Pop() * acc.Pop());
                        continue;

                    case "-":
                        temp = acc.Pop();
                        acc.Push(acc.Pop() - temp);
                        continue;

                    case "/":
                        temp = acc.Pop();
                        acc.Push(acc.Pop() / temp);
                        continue;

                    default:
                        acc.Push(int.Parse(token));
                        break;
                }
            }
            else
            {
                acc.Push(int.Parse(token));
            }
        }
        return acc.Peek();
    }
}
