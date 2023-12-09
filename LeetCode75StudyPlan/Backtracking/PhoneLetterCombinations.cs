namespace LeetCode75StudyPlan.Backtracking;

internal class PhoneLetterCombinations : Solution<string, IList<string>>
{
    private readonly string[] map = new string[]
    {
        "", 
        "", "abc", "def", 
        "ghi", "jkl", "mno", 
        "pqrs", "tuv", "wxyz"
    };
    private IList<string> res;

    public IList<string> LetterCombinations(string digits)
    {
        res = new List<string>();        
        if(digits.Length == 0)  return res; 
        Backtrack(0, digits, stackalloc byte[digits.Length]);
        return res;
    }

    private void Backtrack(int k, string digits, Span<byte> a)
    {                        
        if (k == digits.Length)
        {
            string r = "";
            foreach (byte c in a) r += (char)c;
            res.Add(r);
        }
        else
        {
            foreach (byte c in map[digits[k] - 48])
            {
                a[k] =  c;
                Backtrack(k + 1, digits, a);
            }
        }
    }

    private void Backtrack(int k, string digits, string a)
    {
        if(k == digits.Length)
        {
            if(a.Length > 0) res.Add(a);    
        }
        else
        {
            foreach(char c in map[digits[k] - 48]) 
            {
                Backtrack(k + 1, digits, a + c);
            }
        }
    }

    protected override bool IsEqual(IList<string> actual, IList<string> expected)
    {
        if(actual.Count != expected.Count) return false;
        var set = expected.ToHashSet();
        foreach (string s in actual)
        {
            if(!set.Contains(s)) return false;
        }
        return true;
    }

    protected override string Title => "17. Letter combinatins of a phone number";

    protected override IEnumerable<(string, IList<string>)> TestCases
    {
        get
        {
            yield return ("23", ["ad", "ae", "af", "bd", "be", "bf", "cd", "ce", "cf"]);
            yield return ("", []);
            yield return ("2", ["a", "b", "c"]);
            yield return ("22", ["aa", "ab", "ac", "ba", "bb", "bc", "ca", "cb" , "ca"]);

        }
    }

    protected override IList<string> Solve(string input) => LetterCombinations(input);
}


