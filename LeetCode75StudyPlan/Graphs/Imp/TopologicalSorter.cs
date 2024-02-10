using System.Collections;

namespace LeetCode75StudyPlan.Graphs.Imp;

internal class TopologicalSorter : Depth1stSearcher, IEnumerable<int>
{
    private readonly Stack<int> ts;
    public bool HasCycle { get; private set; }
    public TopologicalSorter(IGraph<int> graph) : base(graph)
    {
        ts = new Stack<int>();
        for(int v = 0; v < graph.VertexCount; v++)
        {
            if (!discovered[v])
                Dfs(v);
        }
    }


    protected override void ProcessVertexLate(int v) => ts.Push(v);
    
    protected override void ProcessEdge(int v, int y)
    {
        if(TypeOfEdge(v, y) == EdgeType.Backward)
        {
            //throw new InvalidOperationException($"NOT A DAG ({v}->{y})");
            HasCycle = true;
        }
    }

    public IEnumerator<int> GetEnumerator() => ts.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    
}
