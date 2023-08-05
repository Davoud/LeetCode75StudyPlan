using LeetCode75StudyPlan.LinkLists;
using System.Numerics;
using System.Xml.Linq;

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

    public static bool operator ==(TreeNode? left, TreeNode? right) => IsSameTree(left, right);
    public static bool operator !=(TreeNode? left, TreeNode? right) => !(left == right);
    private static bool IsSameTree(TreeNode? p, TreeNode? q)
    {
        if (p is not null && q is not null)
            return p.val == q.val && IsSameTree(p.right, q.right) && IsSameTree(p.left, q.left);        
        
        return p is null && q is null;
    }

    public bool Equals(TreeNode? other) => ReferenceEquals(this, other);
    public override bool Equals(object? obj) => Equals(obj as TreeNode);
    public override int GetHashCode() => val.GetHashCode();
    public override string ToString() => new StringBuilder($"[{val} | ").AppendJoin(" ", this.Values().Skip(1)).Append(']').ToString();
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
            if (node.left is TreeNode left) stack.Push(left);
            if (node.right is TreeNode right) stack.Push(right);
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

    public static TreeNode? Tree(params int?[] values) => values.ToTree();

    public static TreeNode? ToTree(this int?[] values)
    {
        if (values.Length == 0 || values[0] is null) return null;
        
        var nodes = new TreeNode?[values.Length];

        for(int i = 0; i < values.Length; i++)
        {
            if (values[i] is int value)
            {
                var node = new TreeNode(value);
                nodes[i] = node;
                if (i > 0 && nodes[(i-1) / 2] is TreeNode parren)
                {
                    if (i % 2 == 0) 
                        parren.right = node; 
                    else 
                        parren.left = node;
                }
            }            
        }

        return nodes[0];
    }
   
    public static TreeNode? FindNode(this TreeNode root, int value)
    {
        foreach(var node in root.Nodes())
            if(node.val == value) return node;
                    
        return null;
    }
}