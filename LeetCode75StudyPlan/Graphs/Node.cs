namespace LeetCode75StudyPlan.Graphs;

public class Node
{
    public int val;
    public IList<Node> neighbors;

    public Node()
    {
        val = 0;
        neighbors = new List<Node>();
    }

    public Node(int _val)
    {
        val = _val;
        neighbors = new List<Node>();
    }

    public Node(int _val, List<Node> _neighbors)
    {
        val = _val;
        neighbors = _neighbors;
    }

    public Node(int _val, params Node[] _neighbors)
    {
        val = _val;
        neighbors = _neighbors;
    }

    public override string ToString()
    {
        var adjs = string.Join(",", neighbors.Select(n => n.val.ToString()));
        return $"[{val} : {adjs}]";
    }
}

public static class GraphExtensions
{
    public static Node[] BuildGraph(this IList<IList<int>> neighbours)
    {
        var g = new Node[neighbours.Count];

        for (int i = 0; i < g.Length; i++)
        {
            g[i] = new(i + 1);
        }

        for (int i = 0; i < g.Length; i++)
        {
            foreach (var item in neighbours[i])
            {
                g[i].neighbors.Add(g[item - 1]);
            }
        }

        return g;
    }

    public static void Dump(this Node[] g)
    {
        StringBuilder sb = new("\n");
        foreach (var node in g)
        {
            sb.Append('#').Append(node.val).Append(": ")
                .AppendJoin(",", node.neighbors.Select(n => n.val))
                .Append('\n');
        }
        Console.WriteLine(sb.ToString());
    }

    
    public static void TraversDfs(this Node g, Action<Node> onNodeVisited)
    {
        ISet<Node> visited = new HashSet<Node>();
        Dfs(g);

        void Dfs(Node n)
        {
            if (!visited.Contains(n))
            {
                onNodeVisited(n);
                visited.Add(n);
                foreach (var node in n.neighbors)
                    Dfs(node);                
            }
        }
    }

    public static void TraversBfs(this Node g, Action<Node> onNodeVisited)
    {
        HashSet<Node> discovered = new();

        Queue<Node> q = new();

        q.Enqueue(g);
        discovered.Add(g);

        while(q.Count > 0)
        {
            Node n = q.Dequeue();
            onNodeVisited(n);
            foreach(var p in n.neighbors)
            {
                if(!discovered.Contains(p))
                {
                    q.Enqueue(p);
                    discovered.Add(p);
                }
            }
        }


    }
}