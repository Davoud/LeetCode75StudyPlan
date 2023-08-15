using static LeetCode75StudyPlan.Trees.TreeNodeExtensions;
namespace LeetCode75StudyPlan.Trees;

internal class CountGoodNodes : Solution<TreeNode?, int>
{
    protected override string Title => "1448. Count Good Nodes in Binary Tree";

    public static int GoodNodes(TreeNode? root)
    {
        return 0;
    }

    protected override IEnumerable<(TreeNode?, int)> TestCases
    {
        get
        {
            yield return (Tree(3, 1, 4, 3, null, 1, 5), 4);
            yield return (Tree(3, 3, null, 4, 2), 5);
            yield return (Tree(1), 1);
        }
    }

    protected override int Solve(TreeNode? input) => GoodNodes(input);
    
}
