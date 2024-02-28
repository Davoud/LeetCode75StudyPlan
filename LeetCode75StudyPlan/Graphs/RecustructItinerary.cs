namespace LeetCode75StudyPlan.Graphs;
using Graphs.Imp;
using System.Collections;
using System.Collections.Immutable;

internal class RecustructItineraryLeetCode :  Solution<IList<IList<string>>, IList<string>>
{
    private List<string> itinerary;
    private Dictionary<string, bool> marked;
    public IList<string> FindItinerary(IList<IList<string>> tickets)
    {        
        var graph = BuildGraph(tickets);

        itinerary = new() { "JFK" };
        marked = graph.ToDictionary(k => k.Key, _ => false);
        Dfs(graph, "JFK");
        return itinerary;
    }

    private void Dfs(Dictionary<string, List<string>> graph, string v) 
    {
        marked[v] = true;
        foreach(string y in graph[v])
        {
            itinerary.Add(y);
            if (!marked[y])
                Dfs(graph, y);                        
        }
    }

    private Dictionary<string, List<string>> BuildGraph(IList<IList<string>> tickets)
    {
        var g = new Dictionary<string, List<string>>();

        foreach(var ticket in tickets)
        {
            g.TryAdd(ticket[0], new());
            g.TryAdd(ticket[1], new());
            g[ticket[0]].Add(ticket[1]);            
        }
        
        foreach(List<string> ajd in g.Values)
        {
            ajd.Sort();
        }

        return g;
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
    


internal class RecustructItinerary : Solution<IList<IList<string>>, IList<string>>
{
    public IList<string> FindItinerary(IList<IList<string>> tickets)
    {         
        var g = new Graph<string>(GraphType.Directed);
        foreach(var ticket in tickets)        
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
