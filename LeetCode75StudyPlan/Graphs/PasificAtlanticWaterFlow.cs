namespace LeetCode75StudyPlan.Graphs;

internal class PasificAtlanticWaterFlow : Solution<int[][], IList<IList<int>>>
{

    private class Node
    {       
        public int Value;
        public bool Pacific;
        public bool Atlantic;
        public readonly IList<Node> Neighburs = new List<Node>();    
    }
  

    // not efficient enough
    public IList<IList<int>> PacificAtlanticViaGraph(int[][] heights)
    {
        var graph = GraphOf(heights);

       
        IList<IList<int>> pa = new List<IList<int>>();

        for (int i = 0; i < heights.Length; i++)
        {
            for (int j = 0; j < heights[i].Length; j++)
            {
                Node node = graph.Islands[i, j];
                if ((node.Pacific && node.Atlantic) || FlowToBoth(node))                
                    pa.Add(new List<int> { i, j });
            }
        }


        return pa;

        bool FlowToBoth(Node node)
        {            
            (bool p, bool a) = (false, false);

            Queue<Node> q = new();
            HashSet<Node> visited = new();

            q.Enqueue(node);

            while (q.Count > 0)
            {
                Node n = q.Dequeue();
                visited.Add(n);

                if (n.Pacific) p = true;
                else if (n.Atlantic) a = true;

                if (p && a) return true;

                foreach (var adj in n.Neighburs)
                {
                    if (!visited.Contains(adj))
                        q.Enqueue(adj);
                }
            }

            return p && a;
        }

    }

    private (Node[,] Islands, Node Pacific, Node Atlantic) GraphOf(int[][] heights)
    {
        int n = heights.Length;
        int m = heights[0].Length;

        Node[,] g = new Node[n, m];

        Node pacific = new() { Value = -1, Pacific = true };
        Node atlantic = new() { Value = -2, Atlantic = true };

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                g[i, j] = new Node
                {
                    Value = heights[i][j],
                    Pacific = (i == 0 || j == 0),
                    Atlantic = (i == n - 1 || j == m - 1)
                };
            }
        }

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {                   
                Node curr = g[i, j];
                Node? prev = Add(curr, i - 1, j, null);
                prev = Add(curr, i, j - 1, prev);
                prev = Add(curr, i + 1, j, prev);
                Add(curr, i, j + 1, prev);
            }
        }

        return (g, pacific, atlantic);

        Node? Add(Node curr, int i, int j, Node? prev)
        {
            Node adj = (i < 0 || j < 0) ? pacific : (i == n || j == m) ? atlantic : g[i, j];
            if (!ReferenceEquals(adj, prev) && adj.Value <= curr.Value)
            {
                curr.Neighburs.Add(adj);
                if (adj.Pacific) curr.Pacific = true;
                if (adj.Atlantic) curr.Atlantic = true;
                return adj;
            }
            return null;
        }
    }

    protected override string Title => "417. Pasific Atlantic Water Flow";

    protected override IEnumerable<(int[][], IList<IList<int>>)> TestCases
    {
        get
        {
            var heights = Arr(
                @int[1, 2, 2, 3, 5],
                @int[3, 2, 3, 4, 4],
                @int[2, 4, 5, 3, 1],
                @int[6, 7, 1, 4, 5],
                @int[5, 1, 1, 2, 4]);

            yield return (heights, List2D("[0,4],[1,3],[1,4],[2,2],[3,0],[3,1],[4,0]"));
        }
    }

    protected override IList<IList<int>> Solve(int[][] input) => PacificAtlanticViaGraph(input);

}
