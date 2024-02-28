namespace LeetCode75StudyPlan.Graphs.Imp;

internal class StronglyConnectedComponentFinder : Depth1stSearcher
{
    
    //private readonly int[] oldestVertex;
    private readonly VerboseArray<int> oldestVertex;
    private readonly int[] compNo;
    private readonly Stack<int> active;
    private int componentsFound;
    private bool enableLogging;

    public int NumberOfComponents => componentsFound;
    public int ComponentNumberOf(int vertext) => compNo[vertext];

    public StronglyConnectedComponentFinder(IGraph<int> graph, bool verbose = false) : base(graph)
    {
        enableLogging = verbose;
        oldestVertex = new(new int[graph.VertexCount], !verbose);
        compNo = new int[graph.VertexCount];

        Log("Initializing Start");
        foreach (int v in graph)
        {
            oldestVertex[v] = v;
            compNo[v] = -1;
        }

        active = new Stack<int>();
        Init();

        Log("Initializing End");
        foreach (int v in graph)
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
            Log($"BACK {x}=>{y}");
            oldestVertex[x] = y;
        }

        if(edge == EdgeType.Cross && compNo[y] == -1 && entryTime[y] < entryTime[oldestVertex[x]])
        {
            Log($"CROSS {x}=>{y}");
            oldestVertex[x] = y;
        }
    }

    protected override void ProcessVertexLate(int v)
    {
        int low = oldestVertex[v];
        if (low == v)
        {
            compNo[v] = ++componentsFound;

            int w = active.Pop();
            while (w != v)
            {
                compNo[w] = componentsFound;
                w = active.Pop();
            }
        }

        int papa = parent[v];
        if(papa > 0 && entryTime[low] < entryTime[oldestVertex[papa]])
        {
            Log($"Shortcutting {v} (parent: {papa}, oldest: {low}, entry[oldest[parent]] {entryTime[oldestVertex[papa]]}");
            oldestVertex[papa] = low;
        }
    }

    private void Log(string msg)
    {
        if(enableLogging)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(msg);
            Console.ResetColor();
        }
    }
   
}
