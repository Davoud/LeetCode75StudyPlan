using System.Collections.Immutable;
using static  LeetCode75StudyPlan.Trees.TreeNodeExtensions;
namespace LeetCode75StudyPlan.Trees;

internal class BinaryTreeRightSideView : Solution<TreeNode?, IList<int>>
{
    protected override string Title => "199. Binary Tree Right Side View";

    public static IList<int> RightSideView(TreeNode? root)
    {
        var view = new List<int>();
        if (root == null) return view;

        var stack = new Stack<(TreeNode node, int level)>();
        stack.Push((root, 0));

        var levels = new List<int>();
        while (stack.Count > 0)
        {
            var (node, level) = stack.Pop();

            if (levels.Count <= level)
                levels.Add(node.val);
            else
                levels[level] = node.val;

            if (node.right is TreeNode right)
                stack.Push((right, level + 1));

            if (node.left is TreeNode left)
                stack.Push((left, level + 1));
        }
        return levels;
    }

    public static IList<int> RightSideViewUsingLevelOrderTravarsal(TreeNode? root)
    {               
        if (root == null) return new List<int>();
        return BinaryTreeLevelOrderTraversal
            .LevelOrder(root)
            .Aggregate(ImmutableList.Create<int>(), (view, levels) => view.Add(levels[^1]));
    }

    protected override IEnumerable<(TreeNode?, IList<int>)> TestCases
    {
        get
        {
            yield return (Tree(1, 2, 3, null, 5, null, 4), List(1, 3, 4));
            yield return (Tree(1, null, 3), List(1, 3));
            yield return (null, new List<int>());
        }
    }

    protected override IList<int> Solve(TreeNode? input) => RightSideView(input);    
}
