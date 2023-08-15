namespace LeetCode75StudyPlan.Trees;
using static LeetCode75StudyPlan.Trees.TreeNodeExtensions;
internal class BinaryTreeLevelOrderTraversal : Solution<TreeNode, IList<IList<int>>>
{
    public static IList<IList<int>> LevelOrder(TreeNode root)
    {
        var stack = new Stack<(TreeNode node, int level)>();
        stack.Push((root, 0));
        
        var levels = new List<IList<int>>();
        while (stack.Count > 0)
        {
            var (node, level) = stack.Pop();
            
            if (levels.Count <= level) 
                levels.Add(new List<int>());            
            
            levels[level].Add(node.val);
            
            if (node.right is TreeNode right) 
                stack.Push((right, level + 1));
            
            if (node.left is TreeNode left) 
                stack.Push((left, level + 1));
        }
        return levels;
    }
       
    protected override string Title => "102. Binary Tree Level Order Traversal";

    protected override IEnumerable<(TreeNode, IList<IList<int>>)> TestCases
    {
        get
        {
            var c1 = Tree(3, 9, 20, null, null, 15, 7)!;
            yield return (c1, List(List(3), List(9, 20), List(15, 7)));
            yield return (Tree(1)!, List(List(1)));            
        }
    }

    protected override IList<IList<int>> Solve(TreeNode input) => LevelOrder(input);
    
}
