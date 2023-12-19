namespace LeetCode75StudyPlan.Graphs;

internal class CourseScheduleOrderer : Solution<(int numCourses, int[][] prerequisites), int[]>
{
    public int[] FindOrder(int numCourses, int[][] prerequisites)
    {
        List<int>?[] graph = new List<int>?[numCourses];
        int[] inDegree = new int[numCourses];

        foreach (int[] pr in prerequisites)
        {           
            (graph[pr[1]] ??= new()).Add(pr[0]);
            inDegree[pr[0]]++;
        }

        int visited = 0;
        Queue<int> queue = new();
        int[] ordered = new int[numCourses];

        for (int v = 0; v < numCourses; v++)
            if (inDegree[v] == 0) queue.Enqueue(v);

        while (queue.Count > 0)
        {
            int v = queue.Dequeue();
            ordered[visited++] = v;
            if (graph[v] is List<int> adj)
                foreach (int node in adj)
                    if (--inDegree[node] == 0) queue.Enqueue(node);
        }

        return visited == numCourses ? ordered : [];
    }

    protected override string Title => "210: Course Scheduler II";

    protected override IEnumerable<((int numCourses, int[][] prerequisites), int[])> TestCases
    {
        get
        {
            yield return ((2, [[0, 1]]), [1, 0]);           
            yield return ((2, [[1,0]]), [0, 1]);
            yield return ((4, [[1, 0], [2, 0], [3, 1], [3, 2]]), [0, 1, 2, 3]);
            yield return ((3, [[1, 0], [2, 1], [1, 2]]), []);
        }
    }
    

    protected override int[] Solve((int numCourses, int[][] prerequisites) input)
        => FindOrder(input.numCourses, input.prerequisites);
    
}
