namespace LeetCode75StudyPlan.Trees;
using static TreeNodeExtensions;
internal class BTreeMaxPathSum : Solution<TreeNode?, int>
{
    public int MaxPathSum(TreeNode root)
    {
        return 0;
    }

    protected override string Title => "124. Binary Tree Maximum Path Sum";

    protected override IEnumerable<(TreeNode?, int)> TestCases
    {
        get
        {
            yield return (Tree(1, 2, 3), 6);
            yield return (Tree(-10, 9, 20, null, null, 15, 7), 42);
        }
    }


    protected override int Solve(TreeNode? input) => MaxPathSum(input!);
    
}
