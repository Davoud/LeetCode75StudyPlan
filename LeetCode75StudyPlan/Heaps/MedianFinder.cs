using System;
using System.Globalization;

namespace LeetCode75StudyPlan.Heaps;


internal class MedianFinder3 : ITestable
{
    readonly MinPq minq = new();
    readonly MaxPq maxq = new();    

    public void AddNum(int n)
    {
        if (minq.Length == 0)
            minq.Enqueue(n);

        else if (minq.Length > maxq.Length)
            maxq.Enqueue(minq[n]);
        
        else
            minq.Enqueue(maxq[n]);
    }

    public double FindMedian() => minq.Length > maxq.Length ? minq.Peek : 0.5 * (minq.Peek + maxq.Peek);

    public void RunTests()
    {
        "295. Find Median from Data Stream".WriteLine();

        MedianFinder3 medianFinder = new();

        medianFinder.AddNum(1);    // arr = [1]
        medianFinder.AddNum(2);    // arr = [1, 2]        

        double actual = medianFinder.FindMedian();
        double expected = 1.5;
        WriteResult(actual == expected, expected, actual);

        medianFinder.AddNum(3);    // arr[1, 2, 3]

        actual = medianFinder.FindMedian();
        expected = 2;
        WriteResult(actual == expected, expected, actual);
    }
}

internal class MedianFinder2 : ITestable
{
    readonly Heap minq = new();
    readonly Heap maxq = new();
    bool odd;

    public void AddNum(int n)
    {
        odd = !odd;
        minq.Enqueue(-maxq.EnqueueDequeue(-n));
        if (minq.Count - 1 > maxq.Count)
            maxq.Enqueue(-minq.Dequeue());
    }

    public double FindMedian() => odd ? minq.Peek : (minq.Peek - maxq.Peek) / 2.0;

    public void RunTests()
    {
        "295. Find Median from Data Stream".WriteLine();

        MedianFinder2 medianFinder = new();

        medianFinder.AddNum(1);    // arr = [1]
        medianFinder.AddNum(2);    // arr = [1, 2]        

        double actual = medianFinder.FindMedian();
        double expected = 1.5;
        WriteResult(actual == expected, expected, actual);

        medianFinder.AddNum(3);    // arr[1, 2, 3]

        actual = medianFinder.FindMedian();
        expected = 2;
        WriteResult(actual == expected, expected, actual);
    }
}


internal class MedianFinder : ITestable
{
    readonly PriorityQueue<int, int> left = new();
    readonly PriorityQueue<int, int> right = new();
    bool odd;

    public void AddNum(int n)
    {
        odd = !odd;
        int m = right.EnqueueDequeue(n, -n);
        left.Enqueue(m, m);

        if (left.Count - 1 > right.Count)
        {
            m = left.Dequeue();
            right.Enqueue(m, -m);
        }
    }

    public double FindMedian() => odd ? left.Peek() : (left.Peek() + right.Peek()) / 2.0;

    public void RunTests()
    {
        "295. Find Median from Data Stream".WriteLine();

        MedianFinder medianFinder = new();
        
       medianFinder.AddNum(1);    // arr = [1]
       medianFinder.AddNum(2);    // arr = [1, 2]        

        double actual = medianFinder.FindMedian();
        double expected = 1.5;
        WriteResult(actual == expected, expected, actual);

        medianFinder.AddNum(3);    // arr[1, 2, 3]

        actual = medianFinder.FindMedian();
        expected = 2;
        WriteResult(actual == expected, expected, actual);


    }
}
