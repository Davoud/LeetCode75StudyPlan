using System.Collections;


namespace LeetCode75StudyPlan.Heaps;

public interface IPriorityQueue<T> where T : IComparable<T>
{
    int Length { get; }
    void Enqueue(T value);
    T Dequeue();
    T Peak { get; }
}

internal class MinPQueue : IPriorityQueue<int>, IEnumerable<int>
{
    private readonly List<int> _heap;
    public MinPQueue(params int[] init)
    {
        _heap = new List<int>(init.Length);
        if (init.Length > 1)
        {
            _heap.AddRange(init);
            for (int i = _heap.Count / 2; i >= 0; i--)
                BobbleDown(i);
        }
    }

    public int Length => _heap.Count;

    public int Peak => _heap[0];

    public void Enqueue(int value)
    {
        _heap.Add(value);
        BobbleUp(_heap.Count - 1);
    }

    public int Dequeue()
    {
        Index last = _heap.Count - 1;
        if(last.Value >= 0)
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
        if (ParentOf(current) is Index parent && _heap[parent] > _heap[current])
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

            if (child.Value < _heap.Count && _heap[minIndex] > _heap[child])
            {
                minIndex = child;
            }

            if (child.Value + 1 < _heap.Count && _heap[minIndex] > _heap[child.Value + 1])
            {
                minIndex = child.Value + 1;
            }

            //for (int i = 0; i < 2; i++)
            //{
            //    if (child.Value + i < _heap.Count)
            //    {
            //        if (_heap[minIndex] > _heap[child.Value + i])
            //            minIndex = child.Value + i;
            //    }
            //}
            
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


class MinQueueTest : ITestable 
{
    public void RunTests()
    {
        Console.WriteLine("-----");
        TestCase1();
        Console.WriteLine("-----");
        TestCase2();
        Console.WriteLine("-----");
        TestCase3();
    }

    private void TestCase3()
    {        
        var input = Arr(1..100);
        var expecteds = Arr(1..100);
        Shuffle(input);
        var pq = new MinPQueue(input);
        for (int i = 0; i < expecteds.Length; i++)
        {
            int actual = pq.Dequeue();
            int expected = expecteds[i];
            WriteResult(actual == expected, expected, actual);
        }
    }

    private void TestCase2()
    {
        var input = Arr(1..100);
        var expecteds = Arr(1..100);                
        Shuffle(input);


        var pq = new MinPQueue();
        foreach(int item in input)
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
        for(int i = 0; i < items.Length; i++)
        {
            int next = r.Next(0, items.Length - 1);
            (items[i], items[next]) = (items[next], items[i]);
        }        
    }

    private static void TestCase1()
    {
        var pq = new MinPQueue(@int[4, 5, 8, 2]);
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