namespace LeetCode75StudyPlan.ArraysAndHashing;

internal static class Anagrams
{

    public static IList<IList<string>> GroupAnagrams3(string[] strs)
    {
        var result = new List<IList<string>>();
        var condidate = new List<byte[]>();
        foreach (string str in strs)
        {
            Span<byte> byteSpan = new(new byte[26]);
            foreach (char c in str)
                byteSpan[(byte)(c - 'a')]++;

            bool found = false;
            for (int i = 0; i < condidate.Count; i++)
            {
                if (byteSpan.SequenceEqual(condidate[i]))
                {
                    result[i].Add(str);
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                condidate.Add(byteSpan.ToArray());
                result.Add(new List<string> { str });
            }

        }
        return result;
    }

    public static IList<IList<string>> GroupAnagrams(string[] strs)
    {
        var result = new List<IList<string>>();
        var condidate = new List<ReadOnlyMemory<byte>>();
        foreach (string str in strs)
        {
            Span<byte> byteSpan = new(new byte[26]);
            foreach (char c in str)
                byteSpan[(byte)(c - 'a')]++;

            bool found = false;
            for (int i = 0; i < condidate.Count; i++)
            {
                if (condidate[i].Span.SequenceEqual(byteSpan))
                {
                    result[i].Add(str);
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                condidate.Add(new ReadOnlyMemory<byte>(byteSpan.ToArray()));
                result.Add(new List<string> { str });
            }

        }


        return result;
    }


    public static IList<IList<string>> GroupAnagrams2(string[] strs)
    {
        Dictionary<string, IList<string>> hm = new();
        foreach (string str in strs)
        {
            string key = ToNormalStr(str);
            if (hm.TryGetValue(key, out var list))
            {
                list.Add(str);
            }
            else
            {
                hm[key] = new List<string> { str };
            }
        }
        return hm.Values.ToList();
    }

    private static ReadOnlySpan<byte> ToByteSpan(string str)
    {
        Span<byte> span = new(new byte[26]);
        foreach (char c in str)
        {
            byte index = (byte)(c - 'a');
            span[index]++;
        }
        return span;
    }

    public static void TestToNormalStr(params string[] str)
    {
        foreach (var item in str)
        {
            Console.WriteLine($"{item}: {ToNormalStr(item)}");
        }
    }

    private static string ToNormalStr(string str)
    {
        Span<byte> span = new(new byte[26]);
        foreach (char c in str)
        {
            byte index = (byte)(c - 'a');
            span[index]++;
        }

        StringBuilder b = new();
        for (int i = 0; i < 26; i++)
        {
            if (span[i] != 0)
            {
                b.Append((char)(i + 'a')).Append(span[i]);
            }
        }
        return b.ToString();
    }

    public static void TestToByteSpan(params string[] input)
    {

        foreach (string str in input)
        {
            ReadOnlySpan<byte> output = ToByteSpan(str);
            StringBuilder s = new();
            foreach (byte b in output)
            {
                s.Append($"{b,2} ");
            }
            Console.WriteLine(str + ": " + s.ToString());
        }


    }
}
