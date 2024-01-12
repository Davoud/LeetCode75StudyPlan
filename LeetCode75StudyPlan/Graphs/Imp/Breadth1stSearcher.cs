using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode75StudyPlan.Graphs.Imp;

abstract class Breadth1stSearcher
{
    protected readonly BitArray _processed;
    protected readonly BitArray _discovared;
    protected readonly int[] _parent;
    protected IGraph<int> _graph;
    protected Breadth1stSearcher(IGraph<int> graph)
    {
        _graph = graph;
        _processed = new(graph.VertexCount);
        _discovared = new(graph.VertexCount);
        _parent = new int[graph.VertexCount];        
    }

    protected void InitSeach()
    {
        _processed.SetAll(false);
        _discovared.SetAll(false);
        for (int i = 0; i < _parent.Length; i++) 
            _parent[i] = -1;
    }

    protected virtual void Run()
    {
        InitSeach();
        BfsFrom(0);
    }    

    protected void BfsFrom(int start)
    {
        Queue<int> q = new();
        q.Enqueue(start);
        _discovared[start] = true;

        while (q.Count > 0)
        {
            int v = q.Dequeue();
            ProcessVertexEarly(v);
            _processed[v] = true;
            foreach (int y in _graph[v])
            {
                if (!_processed[y] || _graph.Type == GraphType.Directed)
                {
                    ProcessEdge(v, y);
                }

                if (!_discovared[y])
                {
                    q.Enqueue(y);
                    _discovared[y] = true;
                    _parent[y] = v;
                }
            }
            ProcessVertexLate(v);
        }
    }

    protected virtual void ProcessVertexEarly(int v) { }
        
    protected virtual void ProcessEdge(int v, int y) { }    

    protected virtual void ProcessVertexLate(int v) { }
    
}
