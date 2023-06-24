using System.Collections;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Xml;

namespace System;
public static class Helper
{
    public static void WriteLine(this string value) => Console.WriteLine(value);
    public static void Dump<T>(this T[] array, [CallerLineNumber] int line = 0)
        => Console.WriteLine($"@{line}:  {string.Join(", ", array)}");

    public static string AsStr<T>(this T[] array) => "[" + string.Join(", ", array) + "]";

    public static string AsStr<T>(this IEnumerable<T> source) => "[" + string.Join(", ", source) + "]";
    public static void Dump<T>(this IEnumerable<T> source)
        => Console.WriteLine(string.Join(", ", source));

    public static T[] Arr<T>(params T[] input) => input;

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

    public static IEnumerable<T> Seq<T>(params T[] input) => input;
    public static IList<T> Lst<T>(params T[] input) => input.ToList();
    public static ISet<T> Set<T>(params T[] input) => input.ToHashSet();
    public static ISet<T> Set<T>(IList<T> input) => input.ToHashSet();

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
}