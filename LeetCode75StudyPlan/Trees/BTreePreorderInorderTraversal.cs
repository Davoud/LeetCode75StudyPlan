namespace LeetCode75StudyPlan.Trees;

using System.Collections.Generic;
using static TreeNodeExtensions;

internal class BTreePreorderInorderTraversal : Solution<(int[] preorder, int[] inorder), TreeNode?>
{
    public TreeNode BuildTree(int[] preorder, int[] inorder) => TreeOf(preorder.AsSpan(), inorder.AsSpan())!;

    private TreeNode? BuildTree(Span<int> preorder, Span<int> inorder)
    {
        int index = inorder.IndexOf(preorder[0]);
        if (index < 0) return null;

        TreeNode node = new(preorder[0]);
        var leftPre = preorder[1..(index + 1)];
        if (leftPre.Length > 0)
            node.left = BuildTree(leftPre, inorder[..index]);

        var rightPre = preorder[(index + 1)..];
        if (rightPre.Length > 0)
            node.right = BuildTree(rightPre, inorder[(index + 1)..]);

        return node;
    }

    private TreeNode? TreeOf(Span<int> pre, Span<int> ino) => 
        pre.Length > 0 && ino.IndexOf(pre[0]) is int i && i >= 0
            ? new TreeNode(pre[0], TreeOf(pre[1..(i + 1)], ino[..i]), TreeOf(pre[(i + 1)..], ino[(i + 1)..]))
            : null;

    protected override string Title => "105. Construct Binary Tree from Preorder and Inorder Traversal";

    protected override bool IsEqual(TreeNode actual, TreeNode expected)
    {
        return actual == expected;
    }

    protected override IEnumerable<((int[] preorder, int[] inorder), TreeNode?)> TestCases
    {
        get
        {
            yield return ((@int[1, 2, 4, 5, 3, 6, 7], @int[4, 2, 5, 1, 6, 3, 7]), Tree(1, 2, 3, 4, 5, 6, 7));
            yield return ((@int[3, 9, 20, 15, 7], @int[9, 3, 15, 20, 7]), Tree(3, 9, 20, null, null, 15, 7));
        }
    }

    protected override TreeNode? Solve((int[] preorder, int[] inorder) input) => BuildTree(input.preorder, input.inorder)!;
    
}

