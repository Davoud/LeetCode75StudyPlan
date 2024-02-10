namespace LeetCode75StudyPlan.Graphs.Imp;

internal class StronglyConnectedComponentFinder : Depth1stSearcher
{
    private readonly int[] oldestVertex;
    private readonly int[] compNo;
    private readonly Stack<int> active;
    private int componentsFound;

    public int NumberOfComponents => componentsFound;
    public int ComponentNumberOf(int vertext) => compNo[vertext];

    public StronglyConnectedComponentFinder(IGraph<int> graph) : base(graph)
    {
        oldestVertex = new int[graph.VertexCount];
        compNo = new int[graph.VertexCount];

        foreach (int v in graph)
        {
            oldestVertex[v] = v;
            compNo[v] = -1;
        }

        active = new Stack<int>();
        Init();

        foreach(int v in graph)
        {
            if (!discovered[v])
            {
                Dfs(v);
            }
        }
        
    }

    protected override void ProcessVertexEarly(int v) => active.Push(v);

    protected override void ProcessEdge(int x, int y)
    {
        EdgeType edge = TypeOfEdge(x, y);

        if(edge == EdgeType.Backward && entryTime[y] < entryTime[oldestVertex[x]])
        {
            oldestVertex[x] = y;
        }

        if(edge == EdgeType.Cross && compNo[y] == -1 && entryTime[y] < entryTime[oldestVertex[x]])
        {
            oldestVertex[x] = y;
        }
    }

    protected override void ProcessVertexLate(int v)
    {
        int low = oldestVertex[v];
        if (low == v)
        {
            PopComponent(v);
        }

        int papa = parent[v];
        if(papa > 0 && entryTime[low] < entryTime[oldestVertex[papa]])
        {
            oldestVertex[papa] = low;
        }
    }

    private void PopComponent(int v)
    {        
        compNo[v] = ++componentsFound;

        int t;
        while((t = active.Pop()) != v)
        {
            compNo[t] = componentsFound;
        }

    }
}
