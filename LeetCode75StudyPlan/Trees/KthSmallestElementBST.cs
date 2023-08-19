namespace LeetCode75StudyPlan.Trees;

using System.ComponentModel;
using static TreeNodeExtensions;
internal class KthSmallestElementBST : Solution<(TreeNode?, int), int>
{
    private readonly IList<int> values = new List<int>();

    public static int KthSmallestSimplified(TreeNode root, int k)
    {
        BTreeStack nodes = new(root);

        while (!nodes.IsEmpty)
        {
            if (nodes.TryPeekLeft(out TreeNode left))
                nodes.Push(left);

            else if (nodes.TryPeekRight(out TreeNode right))
                nodes.Push(right);

            else
                nodes.Pop();
        }
        return nodes.KthSmallesst(k);
    }

    public int KthSmallest(TreeNode root, int k)
    {        
        Traverse(root, 0);
        return values[k - 1];    
    }

    public int KthSmallestNonRec(TreeNode root, int k)
    {
        var stack = new Stack<TreeNode>();
        var visited = new SortedSet<TreeNode>(Comparer<TreeNode>.Create((a, b) => a.val - b.val));
        
        stack.Push(root);
        visited.Add(root);

        while (visited.Count <= k + 2 && stack.Count > 0)
        {
            if (stack.Peek().left is TreeNode left && !visited.Contains(left))
            {
                stack.Push(left);
                visited.Add(left);
            }
            else if (stack.Peek().right is TreeNode right && !visited.Contains(right))
            {
                stack.Push(right);
                visited.Add(right);
            }
            else
            {
                stack.Pop();
            }
        }                
        return visited.ElementAt(k - 1).val;
    }

    

    private class BTreeStack
    {
        private readonly Stack<TreeNode> stack = new();
        private readonly SortedSet<TreeNode> visited = new(Comparer<TreeNode>.Create((a, b) => a.val - b.val));

        public BTreeStack(params TreeNode[] nodes)
        {
            foreach(var node in nodes) Push(node);
        }

        public void Push(TreeNode node)
        {
            stack.Push(node);
            visited.Add(node);
        }

        public bool IsEmpty => stack.Count == 0;

        public bool TryPeekLeft(out TreeNode left)
        {
            if(stack.Peek().left is TreeNode l && !visited.Contains(l))
            {
                left = l;
                return true;
            }
            {
                left = new TreeNode();
                return false;
            }
        }

        public bool TryPeekRight(out TreeNode right)
        {
            if (stack.Peek().right is TreeNode r && !visited.Contains(r))
            {
                right = r;
                return true;
            }
            else
            {
                right = new TreeNode();
                return false;
            }
        }

        public void Pop()
        {
            stack.Pop();
        }

        public int KthSmallesst(int k) => visited.ElementAt(k - 1).val;
    }

    private void Traverse(TreeNode root, int l = 0)
    {              
        if (root.left is TreeNode left) Traverse(left, l + 2);
        values.Add(root.val);
        if (root.right is TreeNode right) Traverse(right, l + 2);        
    }
    

    protected override string Title => "230. Kth Smallest Element in a BST";

    protected override IEnumerable<((TreeNode?, int), int)> TestCases
    {
        get
        {
            yield return ((Tree(5, 3, 6, 2, 4, null, null, 1), 1), 1);
            yield return ((Tree(3, 2, 4, 1), 1), 1);
            yield return ((Tree(1, null, 2), 1), 1);
            yield return ((Tree(1), 1), 1);
            yield return ((Tree(3, 1, 4, null, 2), 1), 1);
            yield return ((Tree(5, 3, 6, 2, 4, null, null, 1), 3), 3);            
        }
    }

    protected override int Solve((TreeNode?, int) input) => KthSmallestSimplified(input.Item1!, input.Item2);

}


