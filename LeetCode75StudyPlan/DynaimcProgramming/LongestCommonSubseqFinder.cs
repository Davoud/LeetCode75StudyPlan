


using BenchmarkDotNet.Attributes;
using static System.Runtime.CompilerServices.Unsafe;
using static System.Runtime.InteropServices.MemoryMarshal;

namespace LeetCode75StudyPlan.DynaimcProgramming;

public class LongestCommonSubseqFinder : Solution<(string text1, string text2), int>
{

    public unsafe int LongestCommonSubsequenceUnsafe(string tx, string ty)
    {
        int n = tx.Length;
        int m = ty.Length;

        Span<int> even = m < 64 ? stackalloc int[m + 1] : new int[m + 1];
        Span<int> odd = m < 64 ? stackalloc int[m + 1] : new int[m + 1];
        
        fixed (int* a = even)
        {
            fixed (int* b = odd)
            {
                for (int i = 0; i < n; i++)
                {
                    if (i % 2 == 0) 
                    {
                        for (int j = 0; j < m; j++)
                            *(b + j + 1) = tx[i] == ty[j] ? *(a + j) + 1 : Math.Max(*(a + j + 1), *(b + j));
                        
                    }
                    else
                    {
                        for (int j = 0; j < m; j++)
                            *(a + j + 1) = tx[i] == ty[j] ? *(b + j) + 1 : Math.Max(*(b + j + 1), *(a + j));                        
                    }
                }
            }
        }

        return n % 2 == 0 ? even[m] : odd[m];

    }

    public int LongestCommonSubsequencePointer(string tx, string ty)
    {
        int n = tx.Length;
        int m = ty.Length;
        
        ref int a = ref GetReference(m < 64 ? stackalloc int[m + 1] : new int[m + 1]);
        ref int b = ref GetReference(m < 64 ? stackalloc int[m + 1] : new int[m + 1]);

        for (int i = 0; i < n; i++)
        {
            
            if (i % 2 == 0)
            {
                for (int j = 0; j < m; j++)
                    Add(ref b, j + 1) = tx[i] == ty[j] ? Add(ref a, j) + 1 : Math.Max(Add(ref a, j + 1), Add(ref b, j));
                
            }
            else
            {
                for (int j = 0; j < m; j++)
                    Add(ref a, j + 1) = tx[i] == ty[j] ? Add(ref b, j) + 1 : Math.Max(Add(ref b, j + 1), Add(ref a, j));                
            }
        }
        return n % 2 == 0 ? Add(ref a, m) : Add(ref b, m);
    }
    public int LongestCommonSubsequence(string tx, string ty)
    {
        int n = tx.Length;
        int m = ty.Length;

        Span<int> a = m < 64 ? stackalloc int[m + 1] : new int[m + 1];
        Span<int> b = m < 64 ? stackalloc int[m + 1] : new int[m + 1];

        for (int i = 0; i < n; i++)
        {            
            for (int j = 0; j < m; j++)
            {
                b[j + 1] = tx[i] == ty[j] ? a[j] + 1 : Math.Max(a[j + 1], b[j]);
            }
            Span<int> t = a; a = b; b = t;            
        }
        return a[m];
    }

    
    public int LongestCommonSubsequenceDP2(string text1, string text2) 
    {
        int n = text1.Length;
        int m = text2.Length;
        int[,] dp = new int[2, m + 1];
        
        for (int i = 0; i < n; i++)
        {
            int self = i % 2, next = (i + 1) % 2;
            
            for (int j = 0; j < m; j++)
            {                
                dp[next, j + 1] = text1[i] == text2[j] 
                    ? dp[self, j] + 1 
                    : Math.Max(dp[self, j + 1], dp[next, j]);
            }
        }

        return dp[n % 2, m];
    }
    
    public int LongestCommonSubsequenceDP(string text1, string text2)
    {
        int n = text1.Length;
        int m = text2.Length;
                  
        int[,] dp = new int[n + 1, m + 1];
       
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                dp[i + 1, j + 1] = text1[i] == text2[j] ? dp[i, j] + 1 : Math.Max(dp[i, j + 1], dp[i + 1, j]);
            }
        }

        //Dump(dp, text1, text2);
        //Console.WriteLine($"{x},{y} = {dp[x, y]} <-------");

        return dp[n, m];
    }

    private void Dump(int[,] dp, string text1, string text2)
    {
        Console.WriteLine("  " + text2.Aggregate("", (a,c) =>  $"{a} {c}"));
        for (int i = 0; i < text1.Length; i++)
        {
            Console.Write(text1[i] + " ");
            for (int j = 0; j < text2.Length; j++)
            {
                Console.Write($"{dp[i, j],2}");
            }
            Console.WriteLine();
        }
    }
    
    public int LongestCommonSubsequenceRec(string text1, string text2)
    {
        int max = 0;

        if (text1.Length < text2.Length)
            LCS(text1, text2, 0);
        else
            LCS(text2, text1, 0);

        return max;

        void LCS(ReadOnlySpan<char> t1, ReadOnlySpan<char> t2, int len)
        {
            //Console.WriteLine($"{t1,6}|{t2,6}|{len,5}");
          
            for (int i = 0; i < t1.Length; i++)
            {
                char c = t1[i];
                for (int j = 0; j < t2.Length; j++)
                {
                    if (c == t2[j])
                    {
                        max = Math.Max(max, len + 1);
                        LCS(t1[(i+1)..], t2[(j+1)..], len + 1);
                    }
                }
            }
        }
    }



    protected override string Title => "1143 Longest Common Subsequence";

    protected override IEnumerable<((string text1, string text2), int)> TestCases
    {
        get
        {
            yield return ((text1: "bsbininm", text2: "jmjkbkjkv"), 1); 
            yield return ((text1: "oxcpqrsvwf", text2: "shmtulqrypy"), 2);
            yield return ((text1: "oxcpqrsvwfbsbininm", text2: "shmtulqrypybsbininm"), 10);
            yield return ((text1: "baaaaaaaaaaaaaaaaaac", text2: "daaaaaaaaaaaaaaaaaaae"), 18);
            yield break;
            yield return ((text1: "bl", text2: "yby"), 1);
            yield return ((text1: "afce", text2: "abcdef"), 3);
            yield return ((text1: "abcdef", text2: "afce"), 3);
            yield return ((text1: "abc", text2: "abc"), 3);
            yield return ((text1: "abcde", text2: "ace"), 3);
            yield return ((text1: "abc", text2: "def"), 0);
        }
    }

    protected override int Solve((string text1, string text2) input) => LongestCommonSubsequenceUnsafe(input.text1, input.text2);

    [Benchmark]
    public int Test_DP2Array()
    {
        int sum = 0;
        foreach (var (input, _) in TestCases)
            sum += LongestCommonSubsequence(input.text1, input.text2);        
        return sum;
    }

    [Benchmark]
    public int Test_DPMatrix()
    {
        int sum = 0;
        foreach (var (input, _) in TestCases)
            sum += LongestCommonSubsequenceDP(input.text1, input.text2);
        return sum;
    }

    
    public int Test_Recursive()
    {
        int sum = 0;
        foreach (var (input, _) in TestCases)
            sum += LongestCommonSubsequenceRec(input.text1, input.text2);
        return sum;
    }

    [Benchmark]
    public int Test_2ArrayPointers()
    {
        int sum = 0;
        foreach (var (input, _) in TestCases)
            sum += LongestCommonSubsequencePointer(input.text1, input.text2);
        return sum;
    }

    [Benchmark]
    public int Test_2ArrayUnsafe()
    {
        int sum = 0;
        foreach (var (input, _) in TestCases)
            sum += LongestCommonSubsequenceUnsafe(input.text1, input.text2);
        return sum;
    }
}
