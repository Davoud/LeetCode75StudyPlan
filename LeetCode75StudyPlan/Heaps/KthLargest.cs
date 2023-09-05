using System.Xml.Linq;

namespace LeetCode75StudyPlan.Heaps;

internal class KthLargest : ITestable
{
    private int _k;
    private PriorityQueue<int, int> pq;
    public KthLargest(int k, int[] nums)
    {
        _k = k;
        pq = new PriorityQueue<int, int>(nums.Select(i => (i, i)));
    }

    public int Add(int val)
    {
        pq.Enqueue(val, val);
        int r = 0;
        for(int i = _k; i > 0 && pq.Count > 0; i--)
        {
            r = pq.Dequeue();
        }
        return r;
    }

    void ITestable.RunTests()
    {
        $"\n703.Kth Largest Element in a Stream".WriteLine();

        var k = this;

        var cases = Arr((3, 4), (5, 5), (10, 5), (9, 8), (8, 4));
        foreach((int input, int expected) in cases)
        {
            int actual = k.Add(input);
            WriteResult(actual == expected, expected, actual);
        }
    }
}
