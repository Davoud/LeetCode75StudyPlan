namespace LeetCode75StudyPlan.Trees;
using static TreeNodeExtensions;
internal class BTreeMaxPathSum : Solution<TreeNode?, int>
{
   

    public int MaxPathSum(TreeNode? root)
    {
        int maxPath = int.MinValue;
        MaxGain(root);
        return maxPath;

        int MaxGain(TreeNode? node) {
            if (node == null) return 0;

            int leftMax = Math.Max(MaxGain(node.left), 0);
            int rightMax = Math.Max(MaxGain(node.right), 0);
         
            maxPath = Math.Max(maxPath, node.val + leftMax + rightMax);

            return node.val + Math.Max(leftMax, rightMax);
        }
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
