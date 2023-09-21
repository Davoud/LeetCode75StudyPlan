using System;

namespace LeetCode75StudyPlan.Heaps;

public class Heap
{
    private readonly List<int> _heap = new();
    public int Count => _heap.Count;
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

    public int EnqueueDequeue(int value)
    {
        Enqueue(value);
        return Dequeue();
    }

    private void BobbleUp(Index current)
    {
        if (current.Value > 0)
        {
            Index parent = current.Value / 2;
            if (_heap[parent] > _heap[current])
            {
                (_heap[parent], _heap[current]) = (_heap[current], _heap[parent]);
                BobbleUp(parent);
            }
        }
    }

    private void BobbleDown(Index current)
    {
        Index child = current.Value * 2;        
        if (child.Value < _heap.Count)
        {
            Index minIndex = current;

            if (child.Value < _heap.Count && _heap[minIndex] > _heap[child])
                minIndex = child;

            if (child.Value + 1 < _heap.Count && _heap[minIndex] > _heap[child.Value + 1])
                minIndex = child.Value + 1;

            if (minIndex.Value != current.Value)
            {
                (_heap[current], _heap[minIndex]) = (_heap[minIndex], _heap[current]);
                BobbleDown(minIndex);
            }
        }
    }
}
