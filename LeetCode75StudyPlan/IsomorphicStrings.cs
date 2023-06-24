using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode75StudyPlan;
static class IsomorphicStrings
{
    public static bool Check(string s, string t)
    {
        if (s.Length != t.Length) { return false; }

        var map = new Dictionary<char, char>();
        var set = new HashSet<char>();
        for (int i = 0; i < s.Length; i++)
        {
            if (map.TryGetValue(s[i], out char c))
            {
                if (c != t[i]) return false;
            }
            else if (set.Contains(t[i]))
            {
                return false;
            }
            else
            {
                map[s[i]] = t[i];
                set.Add(t[i]);
            }
        }
        return true;
    }

    // b->b, a->a, d->b
    public static IEnumerable<(string s, string t, bool expected)> TestCases = new[]
    {
        ("BADC", "BABA", false),
        ("egg", "tee", true),
        ("foo", "bar", false),
        ("paper", "title", true),
        ("all", "see", true),
        ("some", "som", false),
        ("123 45 321", "abc_ef_cba", true)
    };

    public static void RunTests()
    {
        foreach (var (s, t, expected) in TestCases)
        {
            var actuall = Check(s, t);
            if (actuall != expected)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Exptected: {expected}, Actual: {actuall} \t {s}|{t}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Exptected: {expected}, Actual: {actuall} \t {s}|{t}");
            }
            Console.ResetColor();
        }
    }
}
