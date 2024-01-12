namespace LeetCode75StudyPlan.Graphs.Imp;

public class Graph(int vertexCount, GraphType type) : IGraph<int>
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

    public Graph WithEdges(params (int, int)[] edges)
    {
        foreach((int v, int w) in edges)
            AddEdge(v, w);

        return this;
    }
    
    public int VertexCount => vertexCount;
    public GraphType Type => type;
}
