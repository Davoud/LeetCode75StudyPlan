using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode75StudyPlan.DynaimcProgramming;

internal class LongestPalindromicSub : Solution<string, string>
{
    public string LongestPalindrome(string s) // fails to solve certain cases
    {
        int n = s.Length;

        if (n < 1) return s;
        if (n == 2) return s[0] == s[1] ? s : s[0..1];

        int index = 0, max = 1;
        int[] p = new int[n];
        
        p[^1] = 1;
        for (int i = 0; i < n - 1; i++)
        {
            p[i] = s[i] == s[i + 1] ? 2 : 1;
            if (p[i] > max) (max, index) = (p[i], i);
        }
        
        for(int l = 2; l <= n ; l++)
        {
            for(int i = 0; i <= n - l; i++) 
            {
                if (s[i] == s[i + l - 1] && p[i + 1] == l - 2) p[i] = l;
                if (p[i] > max) (max, index) = (p[i], i);
            }
            
        }
        
        return s.Substring(index, p[index]);
    }

    public string LongestPalindrome2D(string s)
    {
        int n = s.Length;

        if (n < 1) return s;
        if (n == 2) return s[0] == s[1] ? s : s[0..1];
        
        bool[,] p = new bool[n + 1, n];

        int maxLen = 1, maxInd = n - 1;

        for(int i = 0; i < n; i++)
        {
            p[0,i] = true;
            p[1,i] = true;            
            if (i < n - 1 && s[i] == s[i + 1])
            {
                p[2, i] = true;
                maxLen = 2;
                maxInd = i;
            }
        }

        for(int l = 3; l <= n; l++)
        {
            for (int i = 0; i < n; i++)
            {
                if (i + l <= n)
                {
                    if(p[l, i] = p[l - 2, i + 1] && s[i] == s[i + l - 1])
                    {
                        maxLen = Math.Max(maxLen, l);
                        maxInd = i;
                    };                    
                }                
            }
        }

        //Dump(p, 3);
        return s.Substring(maxInd, maxLen);
       
    }

    private void Dump(bool[,] pl, int n)
    {
        Console.Write("   ");
        for (int i = 0; i < n; i++) Console.Write($"{i} ");
        
        Console.WriteLine();
        for (int l = 0; l < n + 1; l++)
        {
            Console.Write($"{l,2} ");
            for(int i = 0; i < n; i++) Console.Write((pl[l,i] ? "T" : "F") + " ");            
            Console.WriteLine();
        }        
    }

    protected override string Title => "5. Longest Palindromic Substring";

    protected override IEnumerable<(string, string)> TestCases
    {
        get
        {
            yield return ("ccc", "ccc");
            //yield break;
            yield return ("aacabdkacaa", "aca");
            yield return ("babad", "bab");
            yield return ("cbbd", "bb");
            yield return ("123bbbbacd", "bbbb");
        }
    }    

    protected override string Solve(string input) => LongestPalindrome2D(input);
    
}
