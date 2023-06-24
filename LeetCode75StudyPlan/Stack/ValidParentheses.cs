namespace LeetCode75StudyPlan.Stack;

internal class ValidParentheses : ITestable
{
    void ITestable.RunTests()
    {
        "20. Valid Parentheses".WriteLine();
        
        var cases = new[]
        {
            ("(])",         false),
            (")",           false),
            ("()",          true),
            ("(){}[]",      true),
            ("{]",          false),
            ("(({[()]}))",  true),
            ("(((]]]",      false),
            ("(({[(]}))",  false),            
        };

        cases.RunTests(IsValid);
    }

    private Dictionary<char, int> map = new Dictionary<char, int>
    {
        { '(', 1 },
        { '{', 2 },
        { '[', 3 },
        { ')', -1 },
        { '}', -2 },
        { ']', -3 },
    };

    public bool IsValid(string s)
    {
        Stack<int> stack = new();
        
        foreach (char c in s)
        {
            int value = map[c];
            if (value > 0)            
                stack.Push(value);
            
            else if (stack.TryPeek(out int top) && top == -value)            
                stack.Pop();
            
            else
                return false;            
        }
        return stack.Count == 0;
    }

    public bool IsValid1(string s)
    {
        Stack<int> stack = new();
        int stackOps = 0;
        foreach(char c in s)
        {
            int value = map[c];
            if (value > 0)
            {
                stack.Push(value);
                stackOps++;
            }
            else
            {
                if (stack.TryPeek(out int top) && top == -value)  stack.Pop();                
                stackOps--;
            }
        }
        return stackOps == 0 && stack.Count == 0;
    }
}
