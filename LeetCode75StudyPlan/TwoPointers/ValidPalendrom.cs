using System.Globalization;

namespace LeetCode75StudyPlan.TwoPointers;

internal static class ValidPalendrom
{
    public static void RunTests()
    {
        var testCases = new List<(string input, bool result)>
        {
            (",; W;:GlG:;l ;,", false),
            ("abb", false),
            ("ab", false),
            ("aabaa", true),
            ("cc", true),           
            ("A man, a plan, a canal: Panama", true),
            ("race a car", false),
            (" ", true),
            ("", true),
            ("a", true),

        };

        testCases.RunTests(IsPalindrome, (a, b) => a == b);
    }
    public static bool IsPalindrome(string s)
    {
        if (s.Length <= 1) return true;

        StringBuilder sb = new();
        foreach(char c in s)            
            if(char.IsLetterOrDigit(c)) sb.Append(char.ToLower(c));

        s = sb.ToString();
        //Console.WriteLine(s);
        int i = 0, j = s.Length - 1;
        while (i <= j && i < s.Length && j > 0)
        {
            if (s[i] != s[j]) return false;
            i++;
            j--;
        }
        return true;
    }

    public static bool IsPalindrome(ReadOnlySpan<char> s)
    {
        if (s.Length <= 1)
        {
            return true;
        }
        else
        {
            int i = 0, j = s.Length - 1;
            while (i <= j && i < s.Length && j > 0)
            {
                if (s[i] != s[j]) return false;
                i++;
                j--;
            }
            return true;
        }
    }
}
