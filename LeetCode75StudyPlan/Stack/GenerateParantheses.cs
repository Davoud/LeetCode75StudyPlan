using System.Runtime.InteropServices;
using System;
using System.Runtime.CompilerServices;
using System.Net.Http.Headers;
using StringSet = System.Collections.Generic.HashSet<string>;
namespace LeetCode75StudyPlan.Stack;

public class GenerateParantheses : ITestable
{
    private StringSet _result;
    private int _n;

    public StringSet GenerateParenthesisRecursive(int n)
    {
        _result = new StringSet();
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

    public StringSet GenerateParenthesisStack(int n)
    {
        var s = new Stack<Term>();
        var result = new StringSet();
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

    public StringSet GenerateParenthesisStack2(int n)
    {
        var s = new Stack<(string value, int left, int right)>();
        var result = new StringSet();
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
        (int, StringSet)[] cases =
        [
            (1, ["()"]),
            (2, ["()()", "(())"]),
            (3, ["((()))","(()())","(())()","()(())","()()()"]),
            (4, ["(((())))","((()()))","((())())","((()))()","(()(()))",
                    "(()()())","(()())()","(())(())","(())()()","()((()))",
                    "()(()())","()(())()","()()(())","()()()()"])
        ];

        "22. Generate Parentheses (Recursive)".WriteLine();
        cases.RunTests(GenerateParenthesisRecursive, (a, b) => a.SetEquals(b));

        "22. Generate Parentheses (Stack, record)".WriteLine();
        cases.RunTests(GenerateParenthesisStack, (a, b) => a.SetEquals(b));

        "22. Generate Parentheses (Stack, tuple)".WriteLine();
        cases.RunTests(GenerateParenthesisStack2, (a, b) => a.SetEquals(b));
    }
}

