using LeetCode75StudyPlan.LinkLists;
using System.Numerics;

namespace LeetCode75StudyPlan.Trees;

public class TreeNode : IEqualityOperators<TreeNode?, TreeNode?, bool>, IEquatable<TreeNode?>
{
    public int val;
    public TreeNode? left;
    public TreeNode? right;
    public TreeNode(int val = 0, TreeNode? left = null, TreeNode? right = null)
    {
        this.val = val;
        this.left = left;
        this.right = right;
    }
    
    public static bool operator ==(TreeNode? left, TreeNode? right)
    {
        return IsSameTree(left, right);
    }

    public static bool operator !=(TreeNode? left, TreeNode? right)
    {
        return !(left == right);
    }

    private static bool IsSameTree(TreeNode? p, TreeNode? q)
    {
        if (p != null && q != null)
        {
            return p.val == q.val &&
                IsSameTree(p.right, q.right) &&
                IsSameTree(p.left, q.left);
        }
        if (p == null && q == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool Equals(TreeNode? other)
    {
        return IsSameTree(this, other);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as TreeNode);
    }

    public override int GetHashCode()
    {
        return val.GetHashCode();        
    }
}

public static class TreeNodeExtensions
{
    public static IEnumerable<TreeNode> Nodes(this TreeNode root)
    {        
        var stack = new Stack<TreeNode>();
        stack.Push(root);
        while (stack.Count > 0)
        {
            var node = stack.Pop();
            if(node.left != null) stack.Push(node.left);
            if(node.right != null) stack.Push(node.right);
            yield return node;
        }
    }

    public static IEnumerable<int> Values(this TreeNode root)
    {
        var stack = new Stack<TreeNode>();
        stack.Push(root);
        while (stack.Count > 0)
        {
            var node = stack.Pop();
            yield return node.val;
            if (node.left is TreeNode left) stack.Push(left);            
            if (node.right is TreeNode right) stack.Push(right);            
        }
    }

    public static TreeNode? ToTree(this int?[] values)
    {
        if (values.Length == 0 || values[0] == null) return null;
        
        var nodes = new TreeNode?[values.Length];

        for(int i = 0; i < values.Length; i++)
        {
            if (values[i] is int value)
            {
                var node = new TreeNode(value);
                nodes[i] = node;
                if (i > 0 && nodes[i / 2] is TreeNode parren)
                {
                    if (i % 2 == 0) 
                        parren.left = node; 
                    else 
                        parren.right = node;
                }
            }            
        }

        return nodes[0];
    }

    public static string AsString(this TreeNode? node)
    {
        if (node == null) return "[]";
;       return new StringBuilder("[")
            .AppendJoin(", ", node.Values())
            .Append(']')
            .ToString();        
    }
}