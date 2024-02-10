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

    public IGraph<TVert> Reverse()
    {
        var g = new Graph<TVert>(VertexCount, Type);

        foreach(TVert v in _graph.Keys)
        {
            if (_graph[v] is HashSet<TVert> adjs)
            {
                foreach(TVert w in adjs)
                {
                    g.AddEdge(w, v);
                }
            }
        }
        return g;
    }
}

public class GraphInt(int vertexCount, GraphType type) : IGraph<int>
{
    private readonly HashSet<int>?[] _graph = new HashSet<int>?[vertexCount];

    public IEnumerable<int> this[int v] => _graph[v] ?? [];

    public void AddEdge(int v, int w)
    {
        (_graph[v] ??= []).Add(w);
        if (type == GraphType.Undirected) 
            (_graph[w] ??= []).Add(v);
    }

    public GraphInt WithEdges(params (int, int)[] edges)
    {
        foreach((int v, int w) in edges)
            AddEdge(v, w);

        return this;
    }

    public IEnumerator<int> GetEnumerator() => Enumerable.Range(0, vertexCount).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public int VertexCount => vertexCount;
    public GraphType Type => type;

    public static GraphInt DirectedFrom(params (int, int)[] edges) 
        => new GraphInt(MaxVertexValue(edges), GraphType.Directed).WithEdges(edges);

    public static GraphInt UndirectedFrom(params (int, int)[] edges) 
        => new GraphInt(MaxVertexValue(edges), GraphType.Undirected).WithEdges(edges);
    private static int MaxVertexValue((int, int)[] edges)
    {
        var vCount = 0;
        foreach ((int v, int w) in edges)
        {
            vCount = Math.Max(vCount, v > w ? v : w);
        }
        return vCount + 1;
    }

    public IGraph<int> Reverse()
    {
        var g = new GraphInt(VertexCount, Type);        
        for(int v = 0; v < VertexCount; v++)
        {
            if (_graph[v] is HashSet<int> adjs)
            {
                foreach(int w in adjs)
                {
                    g.AddEdge(w, v);
                }
            }
        }
        return g;
    }
}
