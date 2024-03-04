namespace LeetCode75StudyPlan.Graphs;
using Graphs.Imp;

internal class RecustructItineraryLeetCode : Solution<IList<IList<string>>, IList<string>>
{
    class Vertex 
    {
        private int outDegree;
        private readonly List<string> edges = [];
        public void Add(string value)
        {
            edges.Add(value);
            outDegree++;
        }
        public void SortDesc() => edges.Sort((a, b) => b.CompareTo(a));
        public bool HasMoreEdges => outDegree != 0;
        public string Next => edges[--outDegree];
    }

    public IList<string> FindItinerary(IList<IList<string>> tickets)
    {
        Dictionary<string, Vertex> graph = ToGraph(tickets);

        var path = new Stack<string>();
        Dfs("JFK");
        return path.ToList();

        void Dfs(string v)
        {
            Vertex vertex = graph[v];
            while (vertex.HasMoreEdges) Dfs(vertex.Next);
            path.Push(v);
        }
    }

    private static Dictionary<string, Vertex> ToGraph(IList<IList<string>> tickets)
    {
        var graph = new Dictionary<string, Vertex>();
        
        foreach (var ticket in tickets)
        {
            graph.TryAdd(ticket[0], new());
            graph.TryAdd(ticket[1], new());
            graph[ticket[0]].Add(ticket[1]);
        }
        
        foreach (Vertex v in graph.Values)
        {
            v.SortDesc();
        }
        
        return graph;
    }

    
    
    public IList<string> _FindItinerary(IList<IList<string>> tickets)
    {
        Stack<string> path = new();
        Dictionary<string, int> outgoing = new();
        Dictionary<string, List<string>> graph = BuildGraph(tickets);
        
        foreach (var item in graph)
        {
            outgoing[item.Key] = item.Value.Count;
        }

        Dfs("JFK");
        return path.ToList();

        void Dfs(string v)
        {
            while (outgoing[v] != 0)
            {
                outgoing[v]--;
                Dfs(graph[v][outgoing[v]]);
            }
            path.Push(v);
        }
    }

    
    private Dictionary<string, List<string>> BuildGraph(IList<IList<string>> tickets)
    {
        var g = new Dictionary<string, List<string>>();

        foreach (var ticket in tickets)
        {
            g.TryAdd(ticket[0], new());
            g.TryAdd(ticket[1], new());
            g[ticket[0]].Add(ticket[1]);
        }

        foreach (List<string> ajd in g.Values)
        {
            ajd.Sort((a, b) => b.CompareTo(a));
        }

        return g;
    }

    protected override string Title => "332. Reconstruct Itinerary";

    protected override IEnumerable<(IList<IList<string>>, IList<string>)> TestCases
    {
        get
        {
            //yield return ([["A", "B"], ["B", "C"], ["C", "D"], ["B", "E"], ["E", "B"]],
            //    ["A", "B", "E", "B", "C", "D"]);

            yield return ([["JFK", "KUL"], ["JFK", "NRT"], ["NRT", "JFK"]],
                ["JFK", "NRT", "JFK", "KUL"]);

            yield return ([["MUC", "LHR"], ["JFK", "MUC"], ["SFO", "SJC"], ["LHR", "SFO"]],
                ["JFK", "MUC", "LHR", "SFO", "SJC"]);

            yield return ([["JFK", "SFO"], ["JFK", "ATL"], ["SFO", "ATL"], ["ATL", "JFK"], ["ATL", "SFO"]],
                ["JFK", "ATL", "JFK", "SFO", "ATL", "SFO"]);

        }
    }

    protected override IList<string> Solve(IList<IList<string>> input) => FindItinerary(input);

    protected override bool IsEqual(IList<string> actual, IList<string> expected)
    {
        return actual.SequenceEqual(expected);
    }
}



internal class RecustructItinerary : Solution<IList<IList<string>>, IList<string>>
{
    public IList<string> FindItinerary(IList<IList<string>> tickets)
    {
        var g = new Graph<string>(GraphType.Directed);
        foreach (var ticket in tickets)
            g.AddEdge(ticket[0], ticket[1]);

        return new ItineraryDfs(g, "JFK").Itinerary;
    }

    class ItineraryDfs(IGraph<string> graph, string start) : GraphDepthFirst<string>(graph, start)
    {
        public List<string> Itinerary { get; } = [start];
        protected override void ProcessEdge(string v, string y) => Itinerary.Add(y);
    }

    protected override string Title => "332. Reconstruct Itinerary";

    protected override IEnumerable<(IList<IList<string>>, IList<string>)> TestCases
    {
        get
        {
            yield return ([["JFK", "KUL"], ["JFK", "NRT"], ["NRT", "JFK"]],
              ["JFK", "NRT", "JFK", "KUL"]);

            yield return ([["MUC", "LHR"], ["JFK", "MUC"], ["SFO", "SJC"], ["LHR", "SFO"]],
                ["JFK", "MUC", "LHR", "SFO", "SJC"]);

            yield return ([["JFK", "SFO"], ["JFK", "ATL"], ["SFO", "ATL"], ["ATL", "JFK"], ["ATL", "SFO"]],
                ["JFK", "ATL", "JFK", "SFO", "ATL", "SFO"]);
        }
    }

    protected override IList<string> Solve(IList<IList<string>> input) => FindItinerary(input);

    protected override bool IsEqual(IList<string> actual, IList<string> expected)
    {
        return actual.SequenceEqual(expected);
    }
}
