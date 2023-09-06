using System.Xml.Linq;

namespace LeetCode75StudyPlan.Heaps;



internal class KthLargest : ITestable
{
    private int _k;
    private PriorityQueue<int, int> pq;

    public KthLargest(int k, int[] nums)
    {
        _k = k;
        pq = new(nums.Select(i => (i,i)));        
        while (pq.Count > k) pq.Dequeue();
    }

    public int Add(int val)
    {
        pq.Enqueue(val,val);
        if(pq.Count > _k) pq.Dequeue();
        return pq.Peek();
    }
    


    void ITestable.RunTests()
    {
        $"\n703.Kth Largest Element in a Stream".WriteLine();

        var k = this;

        var cases = Arr((3, 4), (5, 5), (10, 5), (9, 8), (4, 8));
        foreach((int input, int expected) in cases)
        {
            int actual = k.Add(input);
            WriteResult(actual == expected, expected, actual);
        }

        $"\n703.Kth Largest Element in a Stream".WriteLine();
        k = new KthLargest(1, @int[-2]);        
        cases = Arr((-3, -2), (0, 0), (2, 2), (-1, 2), (4, 4));
        foreach ((int input, int expected) in cases)
        {
            int actual = k.Add(input);
            WriteResult(actual == expected, expected, actual);
        }
    }
}
