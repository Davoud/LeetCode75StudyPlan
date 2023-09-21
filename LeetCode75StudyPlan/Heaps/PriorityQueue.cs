using System.Collections;
using System.Xml;

namespace LeetCode75StudyPlan.Heaps;

public interface IPriorityQueue<T> where T : IComparable<T>
{
    int Length { get; }
    void Enqueue(T value);
    T Dequeue();

    T this[T value] { get; } // EnqueueDequeue

    T Peek { get; }
}

public abstract class PriorityQueue : IPriorityQueue<int>, IEnumerable<int>
{
    protected readonly List<int> _heap;
    protected PriorityQueue(params int[] init)
    {
        _heap = init.ToList();
        for (int i = _heap.Count / 2; i >= 0; i--) BobbleDown(i);
    }

    public int Length => _heap.Count;

    public int Peek => _heap[0];

    public void Enqueue(int value)
    {
        _heap.Add(value);
        BobbleUp(_heap.Count - 1);
    }

    public int Dequeue()
    {
        Index last = _heap.Count - 1;
        if (last.Value >= 0)
        {
            int min = _heap[0];
            _heap[0] = _heap[last];
            _heap.RemoveAt(last.Value);
            BobbleDown(0);
            return min;
        }
        else
        {
            throw new InvalidOperationException("Empty Queue!");
        }
    }

    public IEnumerable<int> Dequeue(int times)
    {
        while (--times > 0 && _heap.Count > 0)
            yield return Dequeue();
    }

    public int Pop()
    {
        int last = _heap.Count - 1;
        if (_heap.Count > 0)
        {
            int value = _heap[last];
            _heap.RemoveAt(last);
            return value;
        }
        else
        {
            throw new InvalidOperationException("Empty Queue!");
        }
    }
    /// <summary>
    /// Enqueue value and Dequeue afterwards
    /// </summary>
    /// <param name="value">value to enqueue</param>
    /// <returns>Dequeued value</returns>
    public int this[int value]
    {
        get
        {
            Enqueue(value);
            return Dequeue();
        }
    }

    protected abstract bool WrongPriority(Index a, Index b);// => _heap[a] > _heap[b];

    private static Index? ParentOf(Index index)
    {
        if (index.Value == 0) return null;
        return index.Value / 2;
    }

    private Index? LeftChildOf(Index index)
    {
        int c = index.Value * 2;
        return c < _heap.Count ? (Index?)c : null;
    }

    private void BobbleUp(Index current)
    {
        if (ParentOf(current) is Index parent && WrongPriority(parent, current))
        {
            (_heap[parent], _heap[current]) = (_heap[current], _heap[parent]);
            BobbleUp(parent);
        }
    }

    private void BobbleDown(Index current)
    {
        if (LeftChildOf(current) is Index child)
        {
            Index minIndex = current;

            if (child.Value < _heap.Count && WrongPriority(minIndex, child))
                minIndex = child;

            if (child.Value + 1 < _heap.Count && WrongPriority(minIndex, child.Value + 1))
                minIndex = child.Value + 1;

            if (minIndex.Value != current.Value)
            {
                (_heap[current], _heap[minIndex]) = (_heap[minIndex], _heap[current]);
                BobbleDown(minIndex);
            }
        }
    }

    public IEnumerator<int> GetEnumerator() => _heap.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

}

public class MinPq : PriorityQueue
{
    public MinPq(params int[] init) : base(init) { }
    protected override bool WrongPriority(Index a, Index b) => _heap[a] > _heap[b];
}

public class MaxPq : PriorityQueue
{
    public MaxPq(params int[] init) : base(init) { }
    protected override bool WrongPriority(Index a, Index b) => _heap[a] < _heap[b];
}


class MinPqTests : ITestable
{
    public void RunTests()
    {

        Console.WriteLine("MinPq Senario 1");
        TestCase2(new MaxPq());

        Console.WriteLine("MinPq Senario 1");
        TestCase1();
        Console.WriteLine("MinPq Senario 2");
        TestCase2(new MinPq());
        Console.WriteLine("MinPq Senario 3");
        TestCase3();

    }

    private void TestCase3()
    {
        var input = Arr(1..100);
        var expecteds = Arr(1..100);
        Shuffle(input);
        var pq = new MinPq(input);

        for (int i = 0; i < expecteds.Length; i++)
        {
            int actual = pq.Dequeue();
            int expected = expecteds[i];
            WriteResult(actual == expected, expected, actual);
        }
    }

    private void TestCase2(PriorityQueue pq)
    {
        var input = Arr(1..100);
        var expecteds = Arr(1..100);
        Shuffle(input);

        if (pq is MaxPq)
        {
            Array.Reverse(expecteds);
        }


        foreach (int item in input)
        {
            pq.Enqueue(item);
        }

        pq.Dump();

        for (int i = 0; i < expecteds.Length; i++)
        {
            int actual = pq.Dequeue();
            int expected = expecteds[i];
            WriteResult(actual == expected, expected, actual);
        }
    }

    private static void Shuffle(int[] items)
    {
        Random r = new();
        for (int i = 0; i < items.Length; i++)
        {
            int next = r.Next(0, items.Length - 1);
            (items[i], items[next]) = (items[next], items[i]);
        }
    }

    private static void TestCase1()
    {
        var pq = new MinPq(@int[4, 5, 8, 2]);
        pq.Dump();
        int[] expecteds = @int[2, 4, 5, 8];
        for (int i = 0; i < expecteds.Length; i++)
        {
            int actual = pq.Dequeue();
            int expected = expecteds[i];
            WriteResult(actual == expected, expected, actual);
        }
    }
}

