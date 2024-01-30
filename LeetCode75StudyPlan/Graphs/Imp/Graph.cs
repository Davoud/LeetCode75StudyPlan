using System.Collections;
using System.ComponentModel.Design;

namespace LeetCode75StudyPlan.Graphs.Imp;


public class Graph<TVert>(int vertexCount, GraphType type) : IGraph<TVert> where TVert : notnull
{

    public int VertexCount => vertexCount;
    public GraphType Type => type;

    private readonly Dictionary<TVert, SortedSet<TVert>?> _graph =  new(vertexCount);

    public IEnumerable<TVert> this[TVert vertex] 
    {
        get
        {
            if (_graph.TryGetValue(vertex, out SortedSet<TVert>? result))
            {
                return result!.AsEnumerable();
            }
            return Enumerable.Empty<TVert>();
        }
    }

    public void AddEdge(TVert v, TVert w)
    {
        _graph.TryAdd(v, []);
        _graph.TryAdd(w, []);
        
        _graph[v]!.Add(w);
        
        if (type == GraphType.Undirected)
            _graph[w]!.Add(v);
    }

    public Graph<TVert> WithEdges(params (TVert, TVert)[] edges)
    {        
        foreach ((TVert v, TVert w) in edges)
            AddEdge(v, w);

        return this;
    }

    public IEnumerator<TVert> GetEnumerator() => _graph.Keys.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class GrI32(int vertexCount, GraphType type) : IGraph<int>
{
    private readonly HashSet<int>?[] _graph = new HashSet<int>?[vertexCount];

    public IEnumerable<int> this[int index]
        => _graph[index] is HashSet<int> adjecents ? adjecents : Enumerable.Empty<int>();

    public void AddEdge(int v, int w)
    {
        (_graph[v] ??= new()).Add(w);
        if (type == GraphType.Undirected) 
            (_graph[w] ??= new()).Add(v);
    }

    public GrI32 WithEdges(params (int, int)[] edges)
    {
        foreach((int v, int w) in edges)
            AddEdge(v, w);

        return this;
    }

    public IEnumerator<int> GetEnumerator() => Enumerable.Range(0, _graph.Length).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public int VertexCount => vertexCount;
    public GraphType Type => type;
}
