
using System.Collections.Immutable;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace LeetCode75StudyPlan.SlidingWindow;

internal class PermutationInString : ITestable
{
    void ITestable.RunTests()
    {
        Console.WriteLine("567. Permutation in String");

        ((string s1, string s2), bool)[] cases = new[]
        {
            (("hello", "xyzabclolehooleh"),   true),
            (("hello", "xyzlleoooleh"), false),
            (("adc", "dcda"),           true ),
            (("ab", "eidbaooo"),        true ),
            (("ab", "eidboaoo"),        false),
        };

        cases.RunTests(input => CheckInclusion(input.s1, input.s2), (a, b) => a == b);

    }

    public bool CheckInclusionVerbose(string s1, string s2)
    {
        if (s1.Length > s2.Length)
            return false;

        Span<byte> s1Map = stackalloc byte[26];
        Span<byte> s2Map = stackalloc byte[26];

        int s1Len = s1.Length;

        Console.WriteLine(s1);

        foreach (char c in s1)
            s1Map[(byte)(c - 'a')]++;

        s1Map.Dump();

        Console.WriteLine("\n" + s2);
        for (int i = 0; i < s2.Length; i++)
        {
            s2Map[s2[i] - 'a']++;
            Console.Write($"{i}: {s2[i]}↑ ");
            if (i >= s1Len)
            {
                s2Map[s2[i - s1Len] - 'a']--;
                Console.Write($"{s2[i - s1Len]}↓ ");
            }
            Console.Write("\n");
            s2Map.Dump();

            if (s1Map.SequenceEqual(s2Map))
                return true;
        }

        return false;
    }

    public bool CheckInclusion(string s1, string s2)
    {
        if (s1.Length > s2.Length)
            return false;

        Span<byte> s1Map = stackalloc byte[26];
        Span<byte> s2Map = stackalloc byte[26];

        int s1Len = s1.Length;

        foreach (char c in s1)
            s1Map[(byte)(c - 'a')]++;
       
        for (int i = 0; i < s2.Length; i++)
        {
            s2Map[s2[i] - 'a']++;            
            if (i >= s1Len)
                s2Map[s2[i - s1Len] - 'a']--;                
                                  
            if (s1Map.SequenceEqual(s2Map))
                return true;
        }

        return false;
    }


    public bool CheckInclusion3(string s1, string s2)
    {
        int len = s1.Length;

        Span<byte> s2byte = stackalloc byte[s2.Length];
        for (int i = 0; i < s2.Length; i++)
            s2byte[i] = (byte)(s2[i] - 'a');

        Span<byte> s1Seq = stackalloc byte[26];
        foreach (char c in s1)
            s1Seq[(byte)(c - 'a')]++;

        Span<byte> subSeq = stackalloc byte[26];

        for (int i = 0; i <= s2.Length - len; i++)
        {
            foreach (byte b in s2byte[i..(len + i)])
                subSeq[b]++;

            if (s1Seq.SequenceEqual(subSeq))
                return true;

            subSeq.Clear();
        }

        return false;
    }

}
