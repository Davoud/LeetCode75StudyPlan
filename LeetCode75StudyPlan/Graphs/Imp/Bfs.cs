
using System.Collections;


namespace LeetCode75StudyPlan.Graphs.Imp;

public class Bfs
{
    private readonly BitArray _processed;
    private readonly BitArray _discovared;
    private readonly int[] _parent;
        
    public Bfs(IGraph<int> graph, int start)
    {       
        _processed = new(graph.VertexCount);
        _discovared = new(graph.VertexCount);
        _parent = new int[graph.VertexCount];

        for (int i = 0; i < _parent.Length; i++) _parent[i] = -1;

        BfsFrom(graph, start);
    }

    public Action<int>? ProcessVertexEarly { get; set; }
    public Action<int, int>? ProcessEdge { get; set; }
    public Action<int>? ProcessVertexLate { get; set; }

    private void BfsFrom(IGraph<int> graph, int start)
    {        
        
        Queue<int> q = new();
        q.Enqueue(start);
        _discovared[start] = true;

        while(q.Count > 0)
        {
            int v = q.Dequeue();          
            ProcessVertexEarly?.Invoke(v);
            _processed[v] = true;
            foreach (int y in graph[v])
            {
                if (ProcessEdge != null && (!_processed[y] || graph.Type == GraphType.Directed))
                {
                    ProcessEdge.Invoke(v, y);
                }

                if (!_discovared[y])
                {
                    q.Enqueue(y);
                    _discovared[y] = true;
                    _parent[y] = v;
                }
            }
            ProcessVertexLate?.Invoke(v);
        }
       
    }

    public int ParentOf(int v) => _parent[v];
    
    public IList<int> ShortestPath(int start, int end)
    {
        var path = new List<int>();
        FindPath(start, end, path);
        return path;
    }

    private void FindPath(int start, int end, List<int> path)
    {
        if (start == end || end == -1)
        {
            path.Add(start);
        }
        else
        {
            FindPath(start, _parent[end], path);
            path.Add(end);
        }
    }
}