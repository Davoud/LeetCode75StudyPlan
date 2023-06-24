
namespace LeetCode75StudyPlan.SlidingWindow;

internal class MinimumWindowSubstring : ITestable
{
    void ITestable.RunTests()
    {
        "76. Minimum Window Substring".WriteLine();

        var cases = new[]
        {
            (("ADOBECODEBANC","ABC"), "BANC"),
            (("abc", "b"), "b"),
            (("ab", "b"), "b"),
            (("a", "a"), "a"),
            (("a", "aa"), ""),
        };

        cases.RunTests(input => MinWindow(input.Item1, input.Item2), (a, b) => a == b);

    }
    
    public string MinWindow(string s, string t)
    {
        var count = new Dictionary<char, int>();
       
        foreach (char c in t)
            if (!count.TryAdd(c, 1)) count[c]++;

        int counter = 0;
        (int right, int left) = (0, 0);        
        (int head, int len) = (0, s.Length + 1);
        
        while (right < s.Length)
        {
            char r = s[right];
            if (count.ContainsKey(r) && --count[r] == 0) counter++;            
            right++;

            while (counter == count.Count)
            {
                if (right - left < len)
                    (head, len) = (left, right - left);

                char l = s[left];
                if (count.ContainsKey(l) && ++count[l] > 0) counter--;                
                left++;
            }
        }

        return len > s.Length ? string.Empty : s.Substring(head, len);

    }


    

}