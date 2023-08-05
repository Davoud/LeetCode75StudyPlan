using static LeetCode75StudyPlan.Trees.TreeNodeExtensions;
namespace LeetCode75StudyPlan.Trees;

/*
 * Given a binary search tree (BST), find the lowest common ancestor (LCA) node of two given nodes in the BST.
 * According to the definition of LCA on Wikipedia: “The lowest common ancestor is defined between two nodes p 
 * and q as the lowest node in T that has both p and q as descendants (where we allow a node to be a descendant
 * of itself).”
 */

internal class LowestCommonAncestorBinarySearchTree : Solution<(TreeNode? root, TreeNode? p, TreeNode? q), TreeNode?>
{
    public TreeNode? LowestCommonAncestor1(TreeNode root, TreeNode p, TreeNode q)
    {
        var path2p = PathTo(root, p);
        var path2q = PathTo(root, q);
        int i = 0;
        TreeNode? lca = null;
        while (i < path2p.Count && i < path2q.Count)
        {
            if (path2p[i].Equals(path2q[i])) lca = path2p[i];            
            i++;
        }
        return lca;
    }

    private IList<TreeNode> PathTo(TreeNode root, TreeNode p)
    {                
        var path = new List<TreeNode>();
        TreeNode? node = root;
        while (node != null && !ReferenceEquals(node, p))
        {
            path.Add(node);
            if(node.val > p.val) 
                node = node.left;
            else 
                node = node.right;            
        }
        if(node != null) { path.Add(node); }
        return path;
    }
    
    // better performance
    public TreeNode? LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
    {
        var pp = Find(root, p).GetEnumerator();
        var qq = Find(root, q).GetEnumerator();

        TreeNode? lca = root;
        while (pp.MoveNext() && qq.MoveNext())
        {
            if(ReferenceEquals(pp.Current, qq.Current))
                lca = pp.Current;
            else
                break;            
        }     
        return lca;
    }

    private IEnumerable<TreeNode> Find(TreeNode root, TreeNode p)
    {        
        TreeNode? node = root;
        while (node != null && !ReferenceEquals(node, p))
        {
            yield return node;
            if (node.val > p.val)
                node = node.left;
            else
                node = node.right;
        }
        if (node != null) { yield return node; }        
    }

    protected override string Title => "235. Lowest Common Ancestor of a Binary Search Tree";

    protected override IEnumerable<((TreeNode? root, TreeNode? p, TreeNode? q), TreeNode?)> TestCases
    {
        get
        {
            TreeNode c1 = Tree(6, 2, 8, 0, 4, 7, 9, null, null, 3, 5)!;
            yield return ((c1, c1.FindNode(2), c1.FindNode(8)), c1.FindNode(6));
            yield return ((c1, c1.FindNode(0), c1.FindNode(5)), c1.FindNode(2));
            yield return ((c1, c1.FindNode(6), c1.FindNode(2)), c1.FindNode(6));
            yield return ((c1, c1.FindNode(6), c1.FindNode(8)), c1.FindNode(6));
        }
    }

    protected override TreeNode? Solve((TreeNode? root, TreeNode? p, TreeNode? q) input)
    {
        if(input.root != null && input.p != null &&  input.q != null)
        {
            return LowestCommonAncestor(input.root, input.p, input.q);
        }
        return null;
    }
}
