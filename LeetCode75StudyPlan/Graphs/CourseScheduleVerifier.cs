using System.Collections;

using Graph = System.Collections.Generic.IReadOnlyList<System.Collections.Generic.IReadOnlyList<int>>;

namespace LeetCode75StudyPlan.Graphs;


abstract class ScheduleVerifier : Solution<(int numCourse, int[][] prerequisites), bool>
{
    public abstract bool CanFinish(int numCourses, int[][] prerequisites);

    protected override string Title => "207: Course Schedule";

    protected override IEnumerable<((int numCourse, int[][] prerequisites), bool)> TestCases
    {
        get
        {
            yield return ((3, [[0, 1], [0, 2], [1, 2]]), true);            
            yield return ((20, [[0, 10], [3, 18], [5, 5], [6, 11], [11, 14], [13, 1], [15, 1], [17, 4]]), false);
            yield return ((5, [[1, 4], [2, 4], [3, 1], [3, 2]]), true);
            yield return ((4, [[3, 2], [2, 1], [1, 0]]), true);
            yield return ((2, [[1, 0]]), true);
            yield return ((2, [[1, 0], [0, 1]]), false);

        }
    }
    protected override bool Solve((int numCourse, int[][] prerequisites) input)
    {
        return CanFinish(input.numCourse, input.prerequisites);
    }
}

class ScheduleVerifierSubmit : ScheduleVerifier
{
    public override bool CanFinish(int numCourses, int[][] prerequisites)
    {
        List<List<int>> graph = new(numCourses);       
        for (int v = 0; v < numCourses; v++) graph.Add(new());
        
        int[] inDegree = new int[numCourses];
        foreach (int[] pr in prerequisites)
        {
            if (pr[0] == pr[1]) return false;
            graph[pr[1]].Add(pr[0]);
            inDegree[pr[0]]++;
        }

        Queue<int> q = new();
        int visited = 0;
       
        for (int u = 0; u < inDegree.Length; u++)
            if (inDegree[u] == 0) q.Enqueue(u);                    

        while (q.Count > 0)
        {
            int u = q.Dequeue();
            visited++;
            foreach (var v in graph[u])
                if (--inDegree[v] == 0) q.Enqueue(v);                            
        }

        return visited == numCourses;
    }
}

class ScheduleVerifierKahn : ScheduleVerifier
{
    public override bool CanFinish(int numCourses, int[][] prerequisites)
    {        
        return !HasCycle(GraphOf(numCourses, prerequisites));
    }

    private static bool HasCycle(Graph graph)
    {
        int[] inDegree = new int[graph.Count];
        Queue<int> q = new(); 
                              
        int visited = 0; 
        
        foreach (IReadOnlyList<int> adjList in graph)
        {
            foreach (var v in adjList) 
            {
                inDegree[v]++; 
            }
        }
        
        for (int u = 0; u < graph.Count; u++)
        {
            if (inDegree[u] == 0)
            {
                q.Enqueue(u);
            }
        }

        while (q.Count > 0)
        {
            int u = q.Dequeue();
            visited++;
            
            foreach (var v in graph[u])
            {
                inDegree[v]--;
                if (inDegree[v] == 0)
                {
                    q.Enqueue(v);
                }
            }
        }

        return visited != graph.Count;                              
    }

    private static Graph GraphOf(int vertexCount, int[][] edges)
    {
        List<List<int>> graph = new (vertexCount);
        for (int v = 0; v < vertexCount; v++) graph.Add(new());
        foreach (int[] pr in edges) graph[pr[1]].Add(pr[0]);                                    
        return graph;
    }

}

