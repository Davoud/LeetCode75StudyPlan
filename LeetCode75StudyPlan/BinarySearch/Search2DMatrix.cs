namespace LeetCode75StudyPlan.BinarySearch;

internal class Search2DMatrix : Solution<(int[][], int), bool>
{

    public bool SearchMatrix(int[][] matrix, int target)
    {
        int m = matrix.Length;
        if (m == 0) return false;
        
        int n = matrix[0].Length;
        if (n == 0) return false;

        (int lo, int hi) = (0, (n * m) - 1);
        while (lo <= hi)
        {
            int mid = (lo + hi) / 2;
            int value = matrix[mid / n][mid % n];            
            if (value > target)
            {
                hi = mid - 1;
            }
            else if (value < target)
            {
                lo = mid + 1;
            }
            else
            {
                return true;
            }
        }
        return false;
    }

    public bool SearchMatrix1(int[][] matrix, int target)
    {
        (int lo, int hi) = (0, matrix.Length - 1);
        while (lo <= hi)
        {
            int mid = (lo + hi) / 2;
            int[] row = matrix[mid];
            (int a, int b) = (row[0], row[^1]);
            if (target == a || target == b) return true;
            
            if (target < a)
            {
                hi = mid - 1;
            }
            else if (target > b)
            {
                lo = mid + 1;
            }
            else
            {
                return BSearch(row, target);
            }
        }

        return false;
    }
    private bool BSearch(int[] a, int target)
    {
        (int lo, int hi) = (0, a.Length - 1);
        while (lo <= hi)
        {
            int mid = (lo + hi) / 2;
            if (a[mid] == target) return true;
            if (a[mid] < target) lo = mid + 1; else hi = mid - 1;
        }
        return false;
    }

    protected override string Title => "74. Search a 2D Matrix (https://leetcode.com/problems/search-a-2d-matrix/)";

    protected override IEnumerable<((int[][], int), bool)> TestCases
    {
        get
        {
            yield return (([[1], [3]], 3), true);

            int[][] matrix = [
                [1, 3, 5, 7],
                [10, 11, 16, 20],
                [23, 30, 34, 60]];
                    
            yield return ((matrix, 3), true);
            yield return ((matrix, 1), true);
            yield return ((matrix, 7), true);
            yield return ((matrix, 10), true);
            yield return ((matrix, 16), true);
            yield return ((matrix, 60), true);
            yield return ((matrix, 13), false);
            yield return ((matrix, 9), false);
            yield return ((matrix, 0), false);
            yield return ((matrix, 61), false);
            yield return ((matrix, 21), false);
            yield return ((matrix, 22), false);
        }
    }

    protected override bool Solve((int[][], int) input) => SearchMatrix(input.Item1, input.Item2);

}
