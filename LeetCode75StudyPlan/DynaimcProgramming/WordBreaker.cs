using Microsoft.VisualBasic;
using System.Collections;

namespace LeetCode75StudyPlan.DynaimcProgramming;

internal class WordBreaker : Solution<(string s, IList<string> wordDict), bool>
{

    public static bool WordBreakDP(string s, IList<string> wordDict)
    {
        int n = s.Length;
        bool?[] dp = new bool?[n];

        return WordBreak(0);

        bool WordBreak(int index)
        {            
            if (index >= n) return true;            
            if (dp[index] is bool value) return value;

            bool match = false;
            foreach (string word in wordDict)
            {                
                int i = index;
                bool startWith = true; //s[index..].StartsWith(word)
                foreach (char w in word)
                {
                    if (i == n || s[i] != w)
                    {
                        startWith = false;
                        break;
                    }
                    i++;
                }
                
                if (startWith && WordBreak(index + word.Length))
                {
                    match = true;
                    break;
                }
            }

            dp[index] = match;    
            return match;
        }
    }

    public static bool WordBreakRec(string s, IList<string> wordDict)
    {
        int n = s.Length;
        HashSet<string> dict = [.. wordDict];
        return WordBreak(0);
        
        bool WordBreak(int p)
        {
            if (p == n) return true;
            for(int i = p + 1; i <= n; i++)
                if (dict.Contains(s[p..i]) && WordBreak(i)) return true;            
            return false;
        }
    }
    public static bool WordBreakMem2(string s, IList<string> wordDict)
    {
        int n = s.Length;
        HashSet<string> dict = [.. wordDict];
        bool?[] cache = new bool?[n];
        
        return CanBreak(0);

        bool CanBreak(int p)
        {
            if (p == n) return true;
            if (cache[p] is bool value) return value;
            for (int i = p + 1; i <= n; i++)
            {
                if (dict.Contains(s[p..i]) && CanBreak(i))
                {
                    cache[p] = true;
                    return true;
                }
            }
            cache[p] = false;
            return false;
        }
    }
    public static bool WordBreakMem(string s, IList<string> wordDict)
    {
        int n = s.Length;
        HashSet<string> dict = [.. wordDict];
        Dictionary<string, bool> cache = [];
        return CanBreak(0);

        bool InDict(string sub)
        {
            if(cache.TryGetValue(sub, out bool value)) return value;
            value = dict.Contains(sub);
            cache.Add(sub, value);
            return value;
        }

        bool CanBreak(int p)
        {
            if (p == n) return true;
            for (int i = p + 1; i <= n; i++)
                if (InDict(s[p..i]) && CanBreak(i)) return true;
            return false;
        }
    }


    protected override string Title => "139 Word Break";

    protected override IEnumerable<((string s, IList<string> wordDict), bool)> TestCases
    {
        get
        {
            yield return (("catsandog", ["cats", "dog", "sand", "and", "cat"]), false);
            yield return (("ab", ["a", "b"]), true);
            yield return (("ccbb", ["bc", "cb"]), false);
            yield return (("bb", ["a", "b", "bbb", "bbbb"]), true);         
            yield return (("leetcode", ["leet", "code"]), true);
            yield return (("applepenapple", ["apple", "pen"]), true);
            yield return (("catsanddog", ["cats", "dog", "sand", "and", "cat"]), true);
        }
    }

    protected override bool Solve((string s, IList<string> wordDict) input)
    {
        //return WordBreakRec(input.s, input.wordDict);
        //return WordBreakMem(input.s, input.wordDict);
        return WordBreakDP(input.s, input.wordDict);
    }
}
