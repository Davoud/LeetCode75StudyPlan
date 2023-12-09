namespace LeetCode75StudyPlan.Heaps;

internal class KClosestPoints2Origin : Solution<(int[][] points, int k), int[][]>
{
    public int[][] KClosest(int[][] points, int k)
    {
        var pq = new PriorityQueue<int, double>();
        int[][] closest = new int[k][];

        for (int i = 0; i < points.Length; i++)
        {
            (int x, int y) = (points[i][0], points[i][1]);            
            pq.Enqueue(i, Math.Sqrt((x * x) + (y * y)));
        }

        while (k-- > 0 && pq.Count > 0)
            closest[k] = points[pq.Dequeue()];            
        
        return closest;
    }

    protected override string Title => "973. K Closest Points to Origin";

    protected override IEnumerable<((int[][] points, int k), int[][])> TestCases
    {
        get
        {
            yield return (([[3, 3], [5, -1], [-2, 4]], 2), [[-2, 4], [3, 3]]);
            yield return (([[1, 3], [-2, 2]], 1), [[-2, 2]]);
        }
    }

    protected override bool IsEqual(int[][] actual, int[][] expected)
    {        
        bool? trueForAll = null;
        for (int i = 0, j = 0; i < actual.Length && j < expected.Length; i++, j++)
        {
            trueForAll = (trueForAll ?? true) && actual[i].SequenceEqual(expected[j]);
        }
        return trueForAll ?? false;
    }

    protected override int[][] Solve((int[][] points, int k) input) => KClosest(input.points, input.k);
    
}
