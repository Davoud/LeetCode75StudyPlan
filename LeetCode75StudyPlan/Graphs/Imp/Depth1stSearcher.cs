using System.Collections;
using Vertex = int;

namespace LeetCode75StudyPlan.Graphs.Imp;

class GraphDepthFirst<TVert> where TVert : notnull, IEquatable<TVert>
{
    protected readonly Dictionary<TVert, VertexInfo> info;
    protected readonly IGraph<TVert> graph;
    protected bool finished = false;
    public GraphDepthFirst(IGraph<TVert> graph, TVert start)
    {
        info = new Dictionary<TVert, VertexInfo>(graph.VertexCount);
        this.graph = graph;
        
        foreach(TVert vertext in graph)
            info.Add(vertext, new());
        
        Dfs(start);
    }

    protected void Dfs(TVert vertext)
    {
        if (finished) return;

        VertexInfo v = info[vertext];

        v.Discovered = true;
        
        //entryTime[v] = ++time;

        ProcessVertexEarly(vertext);

        foreach (TVert adgVertext in graph[vertext])
        {
            VertexInfo y = info[adgVertext];
            if (!y.Discovered)
            {
                y.Parent = vertext;
                ProcessEdge(vertext, adgVertext);
                Dfs(adgVertext);
            }
            else if ((!y.Processed && (v.Parent?.Equals(adgVertext) ?? false)) || graph.Type == GraphType.Directed)
            {
                ProcessEdge(vertext, adgVertext);
            }

            if (finished) return;
        }

        ProcessVertexLate(vertext);

        //exitTime[v] = ++time;

        v.Processed = true;
    }

    protected virtual void ProcessVertexEarly(TVert v) { }
    protected virtual void ProcessEdge(TVert v, TVert y) { }
    protected virtual void ProcessVertexLate(TVert v) { }

    public IReadOnlyDictionary<TVert, TVert?> Parents => info.ToDictionary(v => v.Key, y => y.Value.Parent);

    protected class VertexInfo
    {
        public bool Processed { get; set; }
        public bool Discovered { get; set; }
        public TVert? Parent { get; set; }
    }
}

class Depth1stSearcher(IGraph<int> graph)
{
    protected readonly BitArray processed = new(graph.VertexCount);
    protected readonly BitArray discovared = new(graph.VertexCount);
    protected readonly Vertex[] parent = new Vertex[graph.VertexCount];
    protected readonly int[] entryTime = new int[graph.VertexCount];
    protected readonly int[] exitTime = new int[graph.VertexCount];
    protected bool finished = false;
    protected int time = 0;
    public virtual void InitSearch(Vertex vertext = 0)
    {
        processed.SetAll(false);
        discovared.SetAll(false);
        Array.Fill(parent, -1);
        Dfs(vertext);
    }

    protected void Dfs(Vertex v)
    {
        if(finished) return;

        discovared[v] = true;
        entryTime[v] = ++time;

        ProcessVertexEarly(v);

        foreach(Vertex y in graph[v])
        {
            if (!discovared[y])
            {
                parent[y] = v;
                ProcessEdge(v, y);
                Dfs(y);
            }
            else if ((!processed[y] && parent[v] != y) || graph.Type ==  GraphType.Directed)
            {
                ProcessEdge(v, y);
            }

            if (finished) return;
        }

        ProcessVertexLate(v);

        exitTime[v] = ++time;

        processed[v] = true;
    }

    protected virtual void ProcessVertexEarly(Vertex v) { }
    protected virtual void ProcessEdge(Vertex v, Vertex y) { }
    protected virtual void ProcessVertexLate(Vertex v) { }

    public Vertex[] Parents => parent.ToArray();
    public int[] Entries => entryTime.ToArray();
    public int[] Exits => exitTime.ToArray();
}
