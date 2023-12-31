﻿namespace LeetCode75StudyPlan.Tries;

public static class TrieHelpers
{
    public class TrieTestRuner<T> : ITestable where T : ITrie, new()
    {
        void ITestable.RunTests()
        {
            Console.WriteLine($"\n208. Implement Trie (Prefix Tree): {typeof(T).Name}");

            Console.WriteLine("Test Case 1:");
            RunTest(
                ["insert", "search", "search", "search", "startsWith", "startsWith", "startsWith"],
                ["hello", "hell", "helloa", "hello", "hell", "helloa", "hello"],
                [null, false, false, true, true, false, true]);

            Console.WriteLine("Test Case 2:");
            RunTest(
                ["insert", "search", "search", "startsWith", "insert", "search"],
                ["apple", "apple", "app", "app", "app", "app"],
                [null, true, false, true, null, true]);

            Console.WriteLine("Test Case 3:");
            RunTest(
                ["insert", "insert", "insert", "search", "search", "search", "search"],
                ["bad", "dad", "mad", "pad", "bad", ".ad", "b.."],
                [null, null, null, false, true, true, true]);

            Console.WriteLine("Test Case 4:");
            RunTest(
                ["insert", "insert",  "insert", "search", "search", "search", "search", "search"],
                ["abcdefg", "abcxefg", "abcyzfg", "abc.efg", "abc..fg", "abcde..", "..cxefg", "..cxefh"],
                [null, null, null, true, true, true, true, false]);

        }

        private static void RunTest(string[] operations, string[] argumments, bool?[] results)
        {
            ITrie trie = new T();

            bool actual;
            for (int i = 0; i < operations.Length; i++)
            {
                string op = operations[i];
                string arg = argumments[i];
                bool? expected = results[i];
                switch (op)
                {
                    case "insert":
                        trie.Insert(arg);
                        WriteResult(true, "void", "void" + $"\t\t({op} '{arg}')");
                        break;

                    case "search":
                        actual = trie.Search(arg);
                        WriteResult(expected == actual, expected?.ToString() ?? "NULL", actual + $"\t\t({op} '{arg}')");
                        break;

                    case "startsWith":
                        actual = trie.StartsWith(arg);
                        WriteResult(expected == actual, expected?.ToString() ?? "NULL", actual + $"\t\t({op} '{arg}')");
                        break;
                }
            }
        }
    }
}
