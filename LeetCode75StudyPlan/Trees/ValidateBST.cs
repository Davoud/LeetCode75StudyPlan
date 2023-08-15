using System.Windows.Markup;
using static LeetCode75StudyPlan.Trees.TreeNodeExtensions;

namespace LeetCode75StudyPlan.Trees
{
    internal class ValidateBST : Solution<TreeNode?, bool>
    {
        private bool _isValid = true;
        public bool IsValidBST(TreeNode root)
        {
            TreeRange(root);
            return _isValid;
        }

        private (int min, int max) TreeRange(TreeNode node)
        {
            (int min, int max) = (node.val, node.val);
            if(!_isValid) { return  (min, max); }

            if(node.left != null)
            {                
                (int lMin, int lMax) = TreeRange(node.left);
                if(min < lMax) _isValid = false;                                
                min = lMin;
            }
            
            if (node.right != null)
            {                
                (int rMin, int rMax) = TreeRange(node.right);
                if (max > rMin) _isValid = false;                
                max = rMax;                
            }

            return (min, max);
        }

        protected override string Title => "98. Validate Binary Search Tree";

        protected override IEnumerable<(TreeNode?, bool)> TestCases
        {
            get
            {                
                yield return (Tree(2, 1, 3), true);
                yield return (Tree(5, 4, 6, null, null, 3, 7), false);
                yield return (Tree(5, 1, 4, null, null, 3, 6), false);
            }
        }

        protected override bool Solve(TreeNode? input) => IsValidBST(input!);

        
    }
}
