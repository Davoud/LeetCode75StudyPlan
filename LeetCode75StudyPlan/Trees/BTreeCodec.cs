using System;
using static LeetCode75StudyPlan.Trees.TreeNodeExtensions;
namespace LeetCode75StudyPlan.Trees;

internal class BTreeCodec : ITestable
{
    const char OPEN = '[';
    const char CLOSE = ']';
    const char SEP = '`';

    public static string Serialize(TreeNode? root)
    {
        if (root == null) 
            return string.Empty;

        if (root.left == null && root.right == null) 
            return $"{root.val}";

        return $"{root.val}{OPEN}{Serialize(root.left)}{SEP}{Serialize(root.right)}{CLOSE}";        
    }


    public static TreeNode? Deserialize(string data)
    {
        if (string.IsNullOrEmpty(data)) return null;
        return From(data.AsSpan());
    }

    private static TreeNode? From(ReadOnlySpan<char> input)
    {
        if (input.IsEmpty) return null;

        int indexOfOpen = input.IndexOf(OPEN);

        if (indexOfOpen > 0)
        {           
            var node = new TreeNode(int.Parse(input[..indexOfOpen]));
         
            var children = input[(indexOfOpen + 1)..^1];
            if (IndexOfSeperator(children) is int i)
            {
                node.left = From(children[..i]);
                node.right = From(children[(i + 1)..]);
            }
            return node;
        }
        else
        {
            return new TreeNode(int.Parse(input));
        }
    }


    private static TreeNode? BuildTreeFrom(ReadOnlySpan<char> input)
    {
        
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

                if (IndexOfSeperator(children) is int i)
                {
                    node.left = BuildTreeFrom(children[..i]);
                    node.right = BuildTreeFrom(children[(i + 1)..]);
                }
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

    private static int? IndexOfSeperator(ReadOnlySpan<char> input)
    {
        int index = 0;
        int p = 0;
        foreach (char item in input)
        {
            switch (item)
            {
                case SEP: if (p == 0) return index;
                    break;
                case OPEN: p++;
                    break;
                case CLOSE: p--;
                    break;
            }
            index++;
        }

        if (index >= input.Length) return null;
        return index;
    }

    void ITestable.RunTests()
    {
        Console.WriteLine("297. Serialize and Deserialize Binary Tree");
        var testCases = List
        (
            (Tree(1, 2, 3, null, null, 4, 5), Tree(1, 2, 3, null, null, 4, 5)), // 1(2,3(4,5))
            (Tree(3, 1, 4, 3, null, 1, 5), Tree(3, 1, 4, 3, null, 1, 5)),       // 3(1(3,),4(1,5)) 
            (Tree(1), Tree(1)),
            (Tree(1, 2), Tree(1, 2)),
            (Tree(2, null, 3), Tree(2, null, 3)),
            (Tree(1, 2, 3, 4, 5, 67, 8, 9, 10, 11, 12, 13, 15), Tree(1, 2, 3, 4, 5, 67, 8, 9, 10, 11, 12, 13, 15))
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
