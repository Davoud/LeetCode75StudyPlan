using LeetCode75StudyPlan.TwoPointers;
using System.Collections;
using System.Collections.Immutable;

namespace LeetCode75StudyPlan.Backtracking;

internal class PalindromePartitioning : Solution<string, IList<IList<string>>>
{
    private IList<IList<string>> result;
    private int len;
    //O(N*N)
    public IList<IList<string>> Partition(string s)
    {
        result = new List<IList<string>>();
        len = s.Length;        
        //Backtrack(s.AsSpan(), 0, ImmutableList<string>.Empty, new bool[len,len]);
        //Backtrack(s, 0, new bool[len,len]);
        Backtrack(s, 0, new BitArray(len * len));
        return result;
    }


    public void Backtrack(ReadOnlySpan<char> s, int start, ImmutableList<string> a, bool[,] dp)
    {
        if (start >= len)
        {
            result.Add(a);
        }
        else
        {
            for (int end = start; end < len; end++)
            {
                if (s[start] == s[end] && (end - start <= 2 || dp[start + 1, end - 1]))
                {
                    dp[start, end] = true;
                    int rest = end + 1;
                    Backtrack(s, rest, a.Add(new(s[start..rest])), dp);
                }
            }
        }
    }

    public void Backtrack(string s, int start, bool[,] dp, Node? a = null)
    {
        if (start >= len && a != null)
        {            
            result.Add(a.ToList());
        }
        else
        {
            for (int end = start; end < len; end++)
            {
                if (s[start] == s[end] && (end - start <= 2 || dp[start + 1, end - 1]))
                {
                    dp[start, end] = true;
                    int rest = end + 1;                    
                    Backtrack(s, rest, dp, new Node(s[start..rest], a));
                }
            }
        }
    }

    public void Backtrack(string s, int start, BitArray dp, Node? a = null)
    {
        if (start >= len && a != null)
        {
            result.Add(a.ToList());
        }
        else
        {
            for (int end = start; end < len; end++)
            {
                if (s[start] == s[end] && isPalindrome(start, end))
                {
                    dp[(start * len) + end] = true;                    
                    Backtrack(s, end + 1, dp, new Node(s[start..(end + 1)], a));
                }
            }
        }

        bool isPalindrome(int f, int l) => l - f <= 2 || dp[((f + 1) * len) + l - 1];        
    }

    public record Node(string Value, Node? Next)
    {                        
        public IList<string> ToList()
        {
            var node = this;
            List<string> list = new();
            while(node != null)
            {
                list.Add(node.Value);
                node = node.Next;
            }
            list.Reverse();
            return list;
        }
    }

    //O(N*(2**N))
    public IList<IList<string>> _Partition(string s)
    {
        result = new List<IList<string>>();
        
        Backtrack(0, ImmutableList<string>.Empty, s.AsSpan());

        return result;
    }

    public void Backtrack(int start, ImmutableList<string> a, ReadOnlySpan<char> s)
    {
        if (start >= s.Length)
        {
            result.Add(a);            
        }
        else
        {
            for (int end = start; end < s.Length; end++)
            {                
                if (isPalindrome(s, start, end))
                {
                    int rest = end + 1;
                    Backtrack(rest, a.Add(new(s[start..rest])), s);                    
                }
            }
        }
    }

    public void Backtrack(int start, List<string> a, ReadOnlySpan<char> s)
    {
        
        if (start >= s.Length)
        {
            result.Add(a.ToList());
            Console.WriteLine($"{start}: {{{string.Join(",", a)}}} ■");
        }
        else
        {
            Console.WriteLine($"{start}: {{{string.Join(",", a)}}}");
            for (int end = start; end < s.Length; end++)
            {
                if (isPalindrome(s, start, end))
                {
                    a.Add(s[start..(end+1)].ToString());                    
                    Backtrack(end+1, a, s);
                    a.RemoveAt(a.Count - 1);
                }
            }
        }
       
    }

    bool isPalindrome(ReadOnlySpan<char> s, int low, int high)
    {
        while (low < high)
        {
            if (s[low++] != s[high--]) return false;
        }
        return true;
    }



    protected override string Title => "131. Palindrome Partitioning";

    protected override IEnumerable<(string, IList<IList<string>>)> TestCases
    {
        get
        {
            yield return ("AABB",  [["A", "A", "B", "B"], ["A", "A", "BB"], ["AA", "B", "B"], ["AA", "BB"]]);
            yield return ("abcde", [["a", "b", "c", "d", "e"]]);
            yield return ("aab",   [["a", "a", "b"], ["aa", "b"]]);
            yield return ("a",     [["a"]]);
        }
    }

    protected override bool IsEqual(IList<IList<string>> actual, IList<IList<string>> expected)
    {
        if (actual.Count != expected.Count) return false;

        var set = expected.Select(i => i.ToHashSet()).ToHashSet();
        foreach (IList<string> item in actual)
            if (!set.Any(subset => subset.SetEquals(item))) return false;
        
        return true;
    }

    protected override IList<IList<string>> Solve(string input) => Partition(input);

}
