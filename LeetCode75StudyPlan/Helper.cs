
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace System;
public static class Helper
{       
    public readonly static LinkListGenerator lx = new();
    public static void WriteLine(this string value) => Console.WriteLine(value);

    public static void WriteResult(bool succeeded, object expected, object actual)
    {
        Console.ForegroundColor = succeeded ? ConsoleColor.Blue : ConsoleColor.Red;
        Console.WriteLine($"Exptected: {expected}, Actual: {actual}");
        Console.ResetColor();
    }

    public static void Dump<T>(this T[] array, [CallerLineNumber] int line = 0)
        => Console.WriteLine($"@{line}:  {string.Join(", ", array)}");

    public static string AsStr<T>(this T[] array) => "[" + string.Join(", ", array) + "]";

    public static string AsStr<T>(this IEnumerable<T> source) => "[" + string.Join(", ", source) + "]";
    public static string AsStr<T>(this IEnumerable<T> source, string open, string close) => open + string.Join(", ", source) + close;
    public static void Dump<T>(this IEnumerable<T> source)
        => Console.WriteLine(string.Join(", ", source));
   
    public static int[] Arr(Range range)
    {
        int len = range.End.Value - range.Start.Value;
        int[] output = new int[len];
        for (int i = 0; i < len; i++)
        {
            output[i] = range.Start.Value + i;
        }
        return output;
    }
       
    public static void Dump<T>(this Span<T> input, char offset = 'a') where T: INumber<T>
    {
        for(int i = 0; i < input.Length; i++)
        {
            if (input[i] == default) continue;            
            Console.Write($"{(char)(i + offset),2}");
        }
        Console.WriteLine();
        for(int i = 0; i < input.Length; i++)
        {
            if (input[i] == default) continue;
            Console.Write($"{input[i],2}");
        }
        Console.WriteLine("\n");
    }

    public static string Str<T>(this Span<T> input, char offset = 'a') where T : INumber<T>
    {
        var sb = new StringBuilder();
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] == default) continue;
            sb.Append((char)(i + offset));
            sb.Append(input[i]).Append(" | ");            
        }        
        return sb.ToString();
    }

    public static string Str<T>(this T input)
    {
        if(input is string s)
        {
            return $"\"{s}\"";
        }

        if (input is IEnumerable seq)
        {
            StringBuilder sb = new("[");
            foreach (var item in seq) sb.Append(Str(item)).Append(", ");
            if (sb.Length > 2) sb.Remove(sb.Length - 2, 2);
            return sb.Append(']').ToString();
        }

        return input?.ToString() ?? "NULL";
    }


    public static void RunTests<T, R>(this IEnumerable<(T input, R expected)> testCases, Func<T, R> subject) 
        where R : IEqualityOperators<R, R, bool>   
    {
        foreach (var (input, expected) in testCases)
        {
            var actual = subject(input);  
            if (expected == actual)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Exptected: {expected}, Actual: {actual}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Exptected: {expected}, Actual: {actual}");
            }
            Console.ResetColor();
        }
    }

    public static void RunTests<T, R>(Func<T, R> subject, params (T input, R expected)[] testCases)
       where R : IEqualityOperators<R, R, bool>
    {
        testCases.RunTests(subject);        
    }

    public static void RunTests<T>(this IEnumerable<(T input, bool expected)> testCases, Func<T, bool> subject)
    {
        testCases.RunTests(subject, (a, b) => a == b);
    }

    public static void RunTests<T, R>(this IEnumerable<(T input, R expected)> testCases, Func<T, R> test, Func<R, R, bool> match)
    {
        foreach (var (input, expected) in testCases)
        {
            var actual = test(input);

            var (exp, act) = (Str(expected), Str(actual));

            if (match(expected, actual))
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Exptected: {exp}, Actual: {act}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Exptected: {exp}, Actual: {act}");
            }
            Console.ResetColor();
        }

        
    }

    public static int BinSearcher(int min, int max, Predicate<int> stisfises)
    {        
        while (min < max)
        {
            int mid = (max + min) >> 1; // min + (max - min) / 2;
            if (stisfises(mid))
            {
                max = mid;
            }
            else
            {
                min = mid + 1;
            }
        }
        return min;
    }
       

    public static class Mathematics
    {
        public static int Gcd(int p, int q)
        {
            if (p < q) return Gcd(q, p);
            int r = p % q;
            while (r > 0)
            {
                (p, q) = (q, r);
                r = p % q;
            }
            return q;
        }

        public static double Median(int[] n)
        {
            int h = n.Length / 2;
            return h * 2 == n.Length ? (n[h] + n[h - 1]) / 2.0 : n[h];
        }
    }

    public class ListNode: IEnumerable<int>, IEqualityOperators<ListNode?, ListNode?, bool>
    {
        public int val;
        public ListNode? next;

        public ListNode(int val = 0, ListNode? next = null)
        {
            this.val = val;
            this.next = next;
        }

        public IEnumerator<int> GetEnumerator()
        {
            var node = this;
            do
            {
                yield return node.val;
                node = node.next;
            } 
            while (node != null);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return "[" + string.Join(", ", this) + "]";             
        }

        public static bool operator ==(ListNode? left, ListNode? right)
        {
            return (left, right) switch
            {
                (ListNode l, ListNode r) => l.Equals(r),
                (null, null) => true,
                _ => false
            };
        }

        public static bool operator !=(ListNode? left, ListNode? right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            if(obj is ListNode other)
            {
                return this.SequenceEqual(other);
            }

            return false;
        }

        public override int GetHashCode()
        {
            int h = 37;
            foreach(int item in this)
            {
                h += (h * 17) + item.GetHashCode(); 
            }
            return h;
        }
    }

    public class LinkListGenerator
    {
        public ListNode? this[params int[] ints]
        {
            get
            {                
                return AsLinkList(ints.AsSpan());
            }
        }

        public ListNode? Empty => null;

        private ListNode AsLinkList(Span<int> values)
        {            
            if (values.Length == 1)
            {
                return new(values[0], null);
            }
           
            return new(values[0], AsLinkList(values[1..]));
        }
    }
   
    public static char[][] Char2D(params string[] input)
    {
        var chars = new char[input.Length][];
        for(int i = 0; i < input.Length; i++)
        {
            chars[i] = input[i].ToCharArray();
        }
        return chars;
    }

    public class VerboseArray<T>(T[] input, bool disableLogging = false) : IEnumerable<T>
    {
      
        public T this[int i]
        {
            get
            {
                if(!disableLogging) Console.WriteLine($"a[{i}] is {input[i]}");
                return input[i];
            }

            set
            {
                if (!disableLogging) Console.WriteLine($"a[{i}] <- {value}");
                input[i] = value;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return input.Select(i => i).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}