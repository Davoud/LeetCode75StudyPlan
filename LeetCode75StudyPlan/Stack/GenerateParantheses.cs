using System.Runtime.InteropServices;
using System;
using System.Runtime.CompilerServices;
using System.Net.Http.Headers;

namespace LeetCode75StudyPlan.Stack;

public class GenerateParantheses : ITestable
{
    private HashSet<string> _result;
    private int _n;
    
    public ISet<string> GenerateParenthesisRecursive(int n)
    {
        _result = new HashSet<string>();
        _n = n;
        Generate("", 0, 0);
        return _result;
    }

    public void Generate(string term, int left, int right)
    {        
        if(term.Length == 2 * _n)
        {
            _result.Add(term);            
            return;
        }

        if(left < _n)
            Generate(term + '(', left + 1, right);
        
        if(right < left)
            Generate(term + ')', left, right + 1);        
    }

    public record struct Term(string Value, int Left, int Right);

    public ISet<string> GenerateParenthesisStack(int n)
    {
        var s = new Stack<Term>();
        var result = new HashSet<string>();
        s.Push(new("", 0, 0));
        
        while(s.Count > 0)
        {
            Term top = s.Pop();

            if(top.Value.Length == 2 * n)
            {
                result.Add(top.Value);
                continue;
            }
            
            if(top.Left < n)
            {
                s.Push(top with { Value = top.Value + '(', Left = top.Left + 1 });
            }

            if(top.Right < top.Left)
            {
                s.Push(top with { Value = top.Value + ')', Right = top.Right + 1 });
            }
        }

        return result;
    }

    public ISet<string> GenerateParenthesisStack2(int n)
    {
        var s = new Stack<(string value, int left, int right)>();
        var result = new HashSet<string>();
        s.Push(("", 0, 0));

        while (s.Count > 0)
        {
            (string value, int left, int right) = s.Pop();

            if (value.Length == 2 * n)
            {
                result.Add(value);
                continue;
            }

            if (left < n)
                s.Push((value + '(', left + 1, right));
            
            if (right < left)
                s.Push((value + ')', left, right + 1));            
        }

        return result;
    }

    void ITestable.RunTests()
    {        
        var cases = new[]
        {
            (1, Set("()")),
            (2, Set("()()", "(())")),
            (3, Set("((()))","(()())","(())()","()(())","()()()")),
            (4, Set("(((())))","((()()))","((())())","((()))()","(()(()))",
                    "(()()())","(()())()","(())(())","(())()()","()((()))",
                    "()(()())","()(())()","()()(())","()()()()"))
        };

        "22. Generate Parentheses (Recursive)".WriteLine();
        cases.RunTests(GenerateParenthesisRecursive, (a, b) => a.SetEquals(b));

        "22. Generate Parentheses (Stack, record)".WriteLine();
        cases.RunTests(GenerateParenthesisStack, (a, b) => a.SetEquals(b));

        "22. Generate Parentheses (Stack, tuple)".WriteLine();
        cases.RunTests(GenerateParenthesisStack2, (a, b) => a.SetEquals(b));
    }
}

