using LeetCode75StudyPlan.Tries;
using System.Collections;

namespace LeetCode75StudyPlan.Graphs.Imp;

public class GraphBreadthFirst<TVert> where TVert :notnull, IEquatable<TVert>
{
    protected readonly IGraph<TVert> graph;
    protected readonly Dictionary<TVert, VertexInfo> G = [];

    public GraphBreadthFirst(IGraph<TVert> graph, TVert start)
    {
        this.graph = graph;

        foreach (TVert vertext in graph)
            G[vertext] = new();

        Bfs(start);
    }

    protected void Bfs(TVert start)
    {
        Queue<TVert> q = new();
        q.Enqueue(start);
       
        G[start].Discovered = true;

        while (q.Count > 0)
        {
            TVert v = q.Dequeue();
            ProcessVertexEarly(v);
            G[v].Processed = true;
            foreach (TVert adj in graph[v])
            {
                VertexInfo y = G[adj];
                if (!y.Processed || graph.Type == GraphType.Directed)
                {
                    ProcessEdge(v, adj);
                }

                if (!y.Discovered)
                {
                    q.Enqueue(adj);
                    y.Discovered = true;
                    y.Parent = v;
                }
            }
            ProcessVertexLate(v);
        }
    }

    protected virtual void ProcessVertexEarly(TVert v) { }

    protected virtual void ProcessEdge(TVert v, TVert y) { }

    protected virtual void ProcessVertexLate(TVert v) { }

    public TVert? ParentOf(TVert v) => G[v].Parent;

    protected class VertexInfo
    {
        public bool Processed { get; set; }
        public bool Discovered { get; set; }
        public TVert? Parent { get; set; }
    }
}

class Breadth1stSearcher
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

    protected void InitSearch()
    {
        _processed.SetAll(false);
        _discovared.SetAll(false);
        for (int i = 0; i < _parent.Length; i++) 
            _parent[i] = -1;
    }

    public virtual void StartFrom(int vertex = 0)
    {
        InitSearch();
        BfsFrom(vertex);
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
