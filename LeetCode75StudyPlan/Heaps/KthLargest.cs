using System.Xml.Linq;

namespace LeetCode75StudyPlan.Heaps;

internal class KthLargest : ITestable
{
    private int _k;
    private MaxPQ pq;

    private class MaxPQ
    {
        private readonly List<int> _q;
        public MaxPQ(int[] init)
        {
            _q = new List<int>(init.Length);
            if (init.Length > 1)
            {
                _q.AddRange(init);
                for (int i = _q.Count / 2; i >= 0; i--)
                    Down(i);
            }
        }

        public int Length => _q.Count;        

        public void EnQ(int value)
        {
            _q.Add(value);
            Up(_q.Count - 1);
        }

        public int DeQ()
        {
            Index last = _q.Count - 1;
            if (last.Value >= 0)
            {
                int min = _q[0];
                _q[0] = _q[last];
                _q.RemoveAt(last.Value);
                Down(0);
                return min;
            }
            else
            {
                throw new InvalidOperationException("Empty Queue!");
            }
        }
        
        private static Index? ParentOf(Index index)
        {
            if (index.Value == 0) return null;
            return index.Value / 2;
        }

        private Index? LeftChildOf(Index index)
        {
            int c = index.Value * 2;
            return c < _q.Count ? (Index?)c : null;
        }

        private void Up(Index current)
        {
            if (ParentOf(current) is Index parent && _q[parent] < _q[current])
            {
                (_q[parent], _q[current]) = (_q[current], _q[parent]);
                Up(parent);
            }
        }

        private void Down(Index current)
        {
            if (LeftChildOf(current) is Index child)
            {
                Index minIndex = current;

                if (child.Value < _q.Count && _q[minIndex] < _q[child])
                    minIndex = child;

                if (child.Value + 1 < _q.Count && _q[minIndex] < _q[child.Value + 1])
                    minIndex = child.Value + 1;

                if (minIndex.Value != current.Value)
                {
                    (_q[current], _q[minIndex]) = (_q[minIndex], _q[current]);
                    Down(minIndex);
                }
            }
        }
    }

    public KthLargest(int k, int[] nums)
    {
        _k = k;
        pq = new(nums);
    }

    public int Add(int val)
    {
        pq.EnQ(val);
        
        var extracted = new List<int>();
        for(int i = _k; i > 0 && pq.Length > 0; i--)
        {
            extracted.Add(pq.DeQ());
        }
        
        int result = extracted[^1];
        
        foreach(var item in extracted)
        {
            pq.EnQ(item);
        }
        
        return result;
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
    }
}
