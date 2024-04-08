
using LeetCode75StudyPlan.Trees;
using Microsoft.VisualBasic;

namespace LeetCode75StudyPlan.DynaimcProgramming;

internal class DecodeWays : Solution<string, int>
{
    public int NumDecodingsDP(string s)
    {
        int n = s.Length;
        if (n == 0) return 0;
       
        int next = 0, curr = 1, prev = 0;

        for (int i = n - 1; i >= 0; i--)
        {
            if (s[i] == '0')
            {
                next = 0;
            }
            else
            {
                next = curr;
                if (i < n - 1 && (s[i] == '1' || (s[i] == '2' && s[i + 1] < '7')))
                {
                    next += prev;
                }
            }

            prev = curr;
            curr = next;

        }

        return next;
    }

    public int NumDecodingsMem(string s)
    {
        int n = s.Length;
        if (n == 0) return 0;

        int[] mem = new int[n + 1];        
        mem[n] = 1;

        return Dec(0);

        int Dec(int p)
        {
            
            if (p == n) return 1; 
            if (s[p] == '0') return 0; 
            if (mem[p] > 0) return mem[p];
                            
            int r = Dec(p + 1);

            if (p < n - 1 && (s[p] == '1' || (s[p] == '2' && s[p+1] < '7')))
            {
                r += Dec(p + 2);
            }
            
            return mem[p] = r;            
        }
    }

    public int NumDecodingsRec(string s)
    {
        return s.Length == 0 ? 0 : Dec(s.AsSpan());

        static int Dec(ReadOnlySpan<char> c)
        {            
            if (c.Length == 0)
                return 1;
            
            if (c[0] == '0')
                return 0;

            int r = Dec(c[1..]);

            if (c.Length > 1 && (c[0] == '1' || (c[0] == '2' && c[1] < '7')))
            {
                r += Dec(c[2..]);
            }

            return r;
        }
    }
    protected override string Title => "91. Decode Ways";

    protected override IEnumerable<(string, int)> TestCases
    {
        get
        {
            yield return ("1111", 5); 
            yield return ("226", 3);
            yield return ("1102262", 3);
            yield return ("30", 0);            
            yield return ("12", 2);
        }
    }

    protected override int Solve(string input)
    {
        //return NumDecodingsRec(input);
        //return NumDecodingsMem(input);
        return NumDecodingsDP(input);
    }
}
