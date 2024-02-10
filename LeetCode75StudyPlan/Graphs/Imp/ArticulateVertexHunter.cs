using System.Collections;
using static LeetCode75StudyPlan.Graphs.Imp.ArticulateVertexHunter;

namespace LeetCode75StudyPlan.Graphs.Imp;

internal class ArticulateVertexHunter : Depth1stSearcher, IEnumerable<(ArticulationVertexType, int)>
{
    private readonly bool logging;
    private readonly int[] reachalbeAncestor;
    private readonly int[] treeOutDegree;
    private readonly List<(ArticulationVertexType, int)> results;

    public enum ArticulationVertexType
    {
        Root,
        Bridge,
        Parent,
    }

    public ArticulateVertexHunter(IGraph<int> graph, bool verbose = false) : base(graph)
    {
        Init();
        logging = verbose;
        reachalbeAncestor = new int[graph.VertexCount];
        treeOutDegree = new int[graph.VertexCount];
        results = [];
        Dfs(0);

        if(logging)
        {
            Log("\nReachable Ancestors:");
            for(int i = 0; i < graph.VertexCount; i++)
            {
                Log($"{i,3} reaches {reachalbeAncestor[i]}");
            }
        }
    }

    protected override void ProcessVertexEarly(int v)
    {
        Log($"Enter {v}");
        reachalbeAncestor[v] = v;
    }

    protected override void ProcessEdge(int x, int y)
    {
        Log($"From  {x} to {y}");
        switch (TypeOfEdge(x, y))
        {
            case EdgeType.Tree:                
                treeOutDegree[x]++;
                Log($"  TREE Degree[{x}] = {treeOutDegree[x]}");
                break;

            case EdgeType.Backward:
                if (entryTime[y] < entryTime[reachalbeAncestor[x]]) 
                {
                    reachalbeAncestor[x] = y;
                }
                Log($"  BACK Ancestor[{x}] = {reachalbeAncestor[x]}");
                break;            
        }
    }

    protected override void ProcessVertexLate(int v)
    {
        
        int papa = parent[v];
        int degree = treeOutDegree[v];
        int ancestor = reachalbeAncestor[v];

        Log($"Exit  {v} [parent {papa}, degree {degree}, ancestor {ancestor}]");
        if (papa < 1)
        {
            if (degree > 1)
            {                
                results.Add((ArticulationVertexType.Root, v));
            }
            return;
        }

        bool root = parent[papa] < 1;

        if(!root)
        {
            if(ancestor == papa)
            {                
                results.Add((ArticulationVertexType.Parent, papa));
            }

            if(ancestor == v)
            {
                results.Add((ArticulationVertexType.Bridge, papa));
                if (degree > 0)
                {                    
                    results.Add((ArticulationVertexType.Bridge, v));
                }
            }
        }

        int timeV = entryTime[ancestor];
        int timePapa = entryTime[reachalbeAncestor[papa]];

        if(timeV < timePapa)
        {
            reachalbeAncestor[papa] = ancestor;
        }
    }

    public IEnumerator<(ArticulationVertexType, int)> GetEnumerator() => results.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private void Log(string message)
    {
        if(logging)
        {
            Console.WriteLine(message);
        }
    }
}
