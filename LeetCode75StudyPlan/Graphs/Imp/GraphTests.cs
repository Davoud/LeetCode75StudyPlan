namespace LeetCode75StudyPlan.Graphs.Imp;

using System.Reflection;
using Vertex = int;
internal class DfsTraversal : Depth1stSearcher
{
    public DfsTraversal(IGraph<Vertex> graph): base(graph)
    {
        Init();
        Dfs(0);
    }

    protected override void ProcessEdge(Vertex v, Vertex y)
    {
        Console.WriteLine($"Edge {v} --> {y}");
    }

    protected override void ProcessVertexLate(Vertex v)
    {
        Console.WriteLine($"Late {v}");
    }

    protected override void ProcessVertexEarly(Vertex v)
    {
        Console.WriteLine($"Early {v}");
    }
}

class HasCycle : Depth1stSearcher
{    
    private readonly Func<Vertex, Vertex, bool> hasCycleCondition;

    public HasCycle(IGraph<Vertex> g) : base(g) 
    {                 
        hasCycleCondition = g.Type == GraphType.Undirected 
            ? ((src, dest) => discovered[dest] && parent[src] != dest) 
            : ((_, dest) => discovered[dest]);

        Init();
        Dfs(0);

    }

    public bool IsTrue { get; set; }
    protected override void ProcessEdge(Vertex src, Vertex dest)
    {        
        if (hasCycleCondition(src, dest))
        {
            IsTrue = true;
            finished = true;
        }
    }
   
}
public static class GraphTests
{
    private static IGraph<Vertex> Sample1(GraphType t)        
    {
        var g = new GraphInt(5, t);
        g.AddEdge(0, 1);
        g.AddEdge(0, 3);
        g.AddEdge(1, 2);
        g.AddEdge(3, 4);
        //g.AddEdge(4, 1);
        g.AddEdge(4, 3);
        return g;
    }

    public static void StronglyConnected1()
    {
        var g = GraphInt.DirectedFrom(
            (0, 1), 
            (1, 2), (1, 3), (1, 4),
            (2, 0), 
            (3, 0), (3, 7), (3, 5),            
            (4, 5),
            (5, 6), 
            (6, 4),
            (7, 5));

        var sc = new StronglyConnectedComponentFinder(g, true);
        Console.WriteLine($"It contains {sc.NumberOfComponents} strongly connected component");
        foreach(var v in g)
        {
            Console.WriteLine($"Vertex {v} is in the component {sc.ComponentNumberOf(v)}");
        }

    }

    public static void StronglyConnected2()
    {
        var g = GraphInt.DirectedFrom(
            (0, 1),
            (1, 2), (1, 3), (1, 4),
            (2, 0),
            (3, 0), (3, 7), (3, 5),
            (4, 5),
            (5, 6),
            (6, 4),
            (7, 5));

        var sc = new StronglyConnectedComponentKosaraju(g);
        Console.WriteLine($"It contains {sc.NumberOfComponents} strongly connected component");
        foreach (var v in g)
        {
            Console.WriteLine($"Vertex {v} is in the component {sc.ComponentNumberOf(v)}");
        }

    }

    public static void StronglyConnected3()
    {
        var g = GraphInt.DirectedFrom(
            (0, 1), (1, 2), (2, 0),
            (2, 3),
            (3, 4), (4, 5), (5, 3));

        var sc = new StronglyConnectedComponentFinder(g, true);
        Console.WriteLine($"It contains {sc.NumberOfComponents} strongly connected component");
        foreach (var v in g)
        {
            Console.WriteLine($"Vertex {v} is in the component {sc.ComponentNumberOf(v)}");
        }
    }

    public static void ArticulationVertexHunting1()
    {
        var g = GraphInt.UndirectedFrom((0, 1), (0, 2), (1, 3), (2, 3), (3, 4), (3, 5), (4, 5), (4, 6), (6, 7), (5, 7));

        ArticulateVertexHunter hunter = new(g, true);
         
        foreach ((ArticulateVertexHunter.ArticulationVertexType type, int v) in hunter)
        {
            Console.WriteLine($"Articulate via {type} at {v}");
        }
    }

    public static void ArticulationVertexHunting2()
    {
        var g = GraphInt.UndirectedFrom((0, 1), (0, 5), (1, 2), (2, 3), (3, 4), (4, 1));

        foreach ((ArticulateVertexHunter.ArticulationVertexType type, int v) in new ArticulateVertexHunter(g))
        {
            Console.WriteLine($"Articulate via {type} at {v}");
        }
    }


    public static void FindCycle()
    {
        if (new HasCycle(Sample1(GraphType.Directed)).IsTrue)
        {
            Console.WriteLine("Graph is Cyclic");
        }
        else
        {
            Console.WriteLine("No Cycles found");
        }
        
    }

    public static void Test1()
    {
        var g = Sample1(GraphType.Directed);
        var dfs = new DfsTraversal(g);
          
        int len = g.VertexCount;

        Console.WriteLine("\nParents:");
        int[] parents = dfs.Parents;
        for (int i = 0; i < len; i++)
        {
            Console.WriteLine($"\tparent of {i} is {parents[i]}");
        }

        int[] entries = dfs.Entries;
        int[] exits = dfs.Exits;

        //Console.WriteLine("\nEntries");
        //for (int i = 0; i < len; i++)
        //{
        //    Console.WriteLine($"\tentered {i} at {entries[i]}");
        //}

        //Console.WriteLine("\nExits");
        //for (int i = 0; i < len; i++)
        //{
        //    Console.WriteLine($"\texited {i} at {exits[i]}");
        //}

        var timing = new SortedList<int, int>();
        for(int i = 0; i < len; i++)
        {
            timing.Add(entries[i], i);
            timing.Add(exits[i], -i);
        }

        Console.WriteLine();
        foreach (KeyValuePair<int, int> item in timing)
        {
            if(item.Value >= 0)
            {
                Console.WriteLine($"\t@{item.Key,3}: ENTER {item.Value}");
            }
            else
            {
                Console.WriteLine($"\t@{item.Key,3}:  EXIT {-item.Value}");
            }

        }
        
    }

    public static void TestGraphChar()
    {
        IGraph<char> g = new Graph<char>(GraphType.Directed, 6)
            .WithEdges(
                 ('A', 'D'), ('B', 'E'), ('A', 'B'), ('B', 'C'),
                 ('D', 'E'), ('E', 'F'), ('C', 'F'));

        //g['A'].Count().ToString().WriteLine();

        Console.WriteLine($"Graph ({g.VertexCount}, {g.Type}");

        foreach(var item in g)
        {
            Console.WriteLine($"{item}: " + string.Join(", ", g[item]));            
        }

        Console.WriteLine("Dfs");
        var dfc = new GraphDepthFirst<char>(g, 'A');
        foreach (KeyValuePair<char, char> kv in dfc.Parents)
        {
            Console.WriteLine($"Perent of {kv.Key} is {kv.Value}");
        }

        Console.WriteLine("Bfs");
        var bfs = new GraphBreadthFirst<char>(g, 'A');
        foreach(var vert in g)
        {
            Console.WriteLine($"Perent of {vert} is {bfs.ParentOf(vert)}");
        }
    }

    public static void TestGraphString()
    {
        var g = new Graph<string>(GraphType.Directed, 6);

        g.WithEdges(
            ("Aa", "D"), ("B", "E"), ("Aa", "B"), ("B", "Cc"),
            ("D", "E"), ("E", "Fff"), ("Cc", "Fff"));


        foreach (var item in g)
        {
            Console.WriteLine($"{item}: " + string.Join(", ", g[item]));
        }
    }

    public static void TopologicalSorting()
    {
        const int A = 0, B = 1, C = 2, D = 3, E = 4, F = 5, G = 6;
        
        var g = new GraphInt(7, GraphType.Directed)
            .WithEdges(
                (A, B), (A, C), (B, C), (B, D), (C, E), 
                (C, F), (F, E), (E, D), (G, A), (G, F));

        var tps = new TopologicalSorter(g);
        
        

        Console.WriteLine(string.Join(", ", tps.Select(ToChar)));

        static char ToChar(int i) => (char)(i + 'A');
        
    }
}