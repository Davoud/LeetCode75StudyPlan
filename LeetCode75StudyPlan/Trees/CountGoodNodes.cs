using static LeetCode75StudyPlan.Trees.TreeNodeExtensions;
namespace LeetCode75StudyPlan.Trees;

internal class CountGoodNodes : Solution<TreeNode?, int>
{
    protected override string Title => "1448. Count Good Nodes in Binary Tree";

    private int _count;

    public int GoodNodes(TreeNode? root)
    {
        _count = 1;
        if(root is not null) Traverse(root, root.val);
        return _count;
    }

    private void Traverse(TreeNode? nood, int max)
    {
        
        if(nood is null) return;

        if(nood.left is not null)
        {
            if (nood.left.val >= max) _count++;
            Traverse(nood.left, Math.Max(nood.left.val, max));
        }

        if (nood.right is not null)
        {
            if (nood.right.val >= max) _count++;
            Traverse(nood.right, Math.Max(nood.right.val, max));
        }
    }

    protected override IEnumerable<(TreeNode?, int)> TestCases
    {
        get
        {
            yield return (Tree(3, 1, 4, 3, null, 1, 5), 4);
            yield return (Tree(3, 3, null, 4, 2), 3);
            yield return (Tree(1), 1);
        }
    }

    protected override int Solve(TreeNode? input) => GoodNodes(input);
    
}
