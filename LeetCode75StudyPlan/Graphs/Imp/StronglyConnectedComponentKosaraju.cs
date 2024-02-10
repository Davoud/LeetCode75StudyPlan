
namespace LeetCode75StudyPlan.Graphs.Imp;

internal class StronglyConnectedComponentKosaraju : Depth1stSearcher
{
    
    private readonly int[] compNo;
    public int NumberOfComponents { get; }
    public int ComponentNumberOf(int vertext) => compNo[vertext];
        
    public StronglyConnectedComponentKosaraju(IGraph<int> graph) : base(graph)
    {
        compNo = new int[graph.VertexCount];        

        foreach (int v in new TopologicalSorter(graph.Reverse()))
        {
            if (!discovered[v])
            {
                NumberOfComponents++;
                Dfs(v);
            }
        }
    }

    protected override void ProcessVertexEarly(int v) => compNo[v] = NumberOfComponents;

}
