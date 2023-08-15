using System;
using static LeetCode75StudyPlan.Trees.TreeNodeExtensions;
namespace LeetCode75StudyPlan.Trees;

internal class BTreeCodec : ITestable
{

    public static string Serialize(TreeNode? root)
    {
        if (root == null) return string.Empty;

        if (root.left == null && root.right == null) return $"{root.val}";

        return $"{root.val}({Serialize(root.left)},{Serialize(root.right)})";
    }


    public static TreeNode? Deserialize(string data)
    {
        if (string.IsNullOrEmpty(data)) return null;
        return From(data.AsSpan());
    }

    private static TreeNode? From(ReadOnlySpan<char> input)
    {
        if (input.IsEmpty) return null;

        int indexOfOpen = input.IndexOf('(');

        if (indexOfOpen > 0)
        {           
            var node = new TreeNode(int.Parse(input[..indexOfOpen]));
         
            var children = input[(indexOfOpen + 1)..^1];
            int indexOfSep = IndexOfSeperator(children);

            node.left = From(children[..indexOfSep]);
            node.right = From(children[(indexOfSep + 1)..]);

            return node;
        }
        else
        {
            return new TreeNode(int.Parse(input));
        }
    }


    private static TreeNode? BuildTreeFrom(ReadOnlySpan<char> input)
    {
        Console.WriteLine("297. Serialize and Deserialize Binary Tree");
        Console.WriteLine($"##\t{input}");
        if (input.IsEmpty) return null;

        int indexOfOpen = input.IndexOf('(');

        if (indexOfOpen > 0) // a(P,Q)
        {
            var nodeValue = input[..indexOfOpen];

            if (int.TryParse(nodeValue, out int value))
            {
                var node = new TreeNode(value);
                var children = input[(indexOfOpen + 1)..^1];

                int indexOfSep = IndexOfSeperator(children);

                node.left = BuildTreeFrom(children[..indexOfSep]);
                node.right = BuildTreeFrom(children[(indexOfSep + 1)..]);

                return node;
            }
            else
            {
                throw new InvalidDataException("Invalid Format: " + input[..indexOfOpen].ToString());
            }
        }
        else
        {
            if (int.TryParse(input, out int value))
            {
                return new TreeNode(value);
            }
            else
            {
                throw new InvalidDataException("Invalid Format: " + input.ToString());
            }
        }


    }

    private static int IndexOfSeperator(ReadOnlySpan<char> input)
    {
        int index = 0;
        int p = 0;
        foreach (char item in input)
        {
            switch (item)
            {
                case ',': if (p == 0) return index;
                    break;
                case '(': p++;
                    break;
                case ')': p--;
                    break;
            }
            index++;
        }

        if (index >= input.Length) return -1;
        return index;
    }

    void ITestable.RunTests()
    {
        var testCases = List
        (
            (Tree(1, 2, 3, null, null, 4, 5), Tree(1, 2, 3, null, null, 4, 5)), // 1(2,3(4,5))
            (Tree(3, 1, 4, 3, null, 1, 5), Tree(3, 1, 4, 3, null, 1, 5)),       // 3(1(3,),4(1,5)) 
            (Tree(1), Tree(1)),
            (Tree(1, 2), Tree(1, 2)),
            (Tree(2, null, 3), Tree(2, null, 3))
        );

        foreach ((TreeNode? input, TreeNode? output) in testCases)
        {
            string coded = Serialize(input);

            Console.WriteLine("\nSerilized Value: " + coded);

            if (Deserialize(coded) is TreeNode decoded)
            {
                WriteResult(output == decoded, output!, decoded);
            }
            else
            {
                WriteResult(false, output!, "NULL");
            }
        }
    }
}
