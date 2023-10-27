namespace LeetCode75StudyPlan.Graphs;

using System.Collections;
using static LeetCode75StudyPlan.Graphs.GraphExtensions;
internal class GraphDeepCopier : Solution<Node?, Node?>
{
    protected override string Title => "133. Clone Graph";


    public Node? CloneGraphBfs(Node? node)
    {        
        if (node == null) return null;

        Node curr, copy;
        Queue<Node> queue = new();
        BitArray discovered = new(101);
        Dictionary<int, Node> copies = new();

        queue.Enqueue(node);
        discovered[node.val] = true;

        while(queue.Count > 0)
        {
            curr = queue.Dequeue();                                  
            copy = CopyOf(curr.val);

            foreach (Node neighbor in curr.neighbors)
            {
                copy.neighbors.Add(CopyOf(neighbor.val));                
                if (!discovered[neighbor.val])
                {
                    queue.Enqueue(neighbor);                    
                    discovered[neighbor.val] = true;                    
                }
            }
        }        

        return copies[1];

        Node CopyOf(int v)
        {
            if (!copies.TryGetValue(v, out Node? n))
            {
                n = new(v);
                copies[v] = n;
            }
            return n;              
        }
    }

    // Dfs, recursive    
    public Node? CloneGraphDfs(Node? node)
    {
        if (node == null) return null;

        Dictionary<int, Node> nodes = new();        
        var copied = new BitArray(100);
        Dfs(node);

        return nodes[1];

        void Dfs(Node node)
        {
            int v = node.val;
            if (copied[v - 1]) return;

            if(!nodes.TryGetValue(v, out Node? copy))
            {
                copy = new(v);
                nodes.Add(v, copy);
            }
            
            copied[v - 1] = true;

            foreach (Node neighbor in node.neighbors)
            {
                v = neighbor.val;
                if (!nodes.TryGetValue(v, out Node? neighborCopy))
                {
                    neighborCopy = new(v);
                    nodes.Add(v, neighborCopy);                    
                }
                copy.neighbors.Add(neighborCopy);
                Dfs(neighbor);
            }
        }
    }


    protected override IEnumerable<(Node?, Node?)> TestCases
    {
        get
        {
            IList<IList<int>> neighbours = List2D("[2,4],[1,3],[2,4],[1,3]");
            Node[] g1 = neighbours.BuildGraph();
            var g2 = neighbours.BuildGraph();           
            yield return (g1[0], g2[0]);

            neighbours = List2D("[2,3,4],[1,7],[1],[1,5,6,8],[4],[4],[2],[4]");
            g1 = neighbours.BuildGraph();
            g2 = neighbours.BuildGraph();          
            yield return (g1[0], g2[0]);
        }
    }

    

    protected override bool IsEqual(Node? expected, Node? actual)
    {
        //"Actual:".WriteLine();
        //actual?.TraversDfs(Console.WriteLine);
        //"Expected:".WriteLine();
        //expected?.TraversDfs(Console.WriteLine);
        return actual?.val == expected?.val && !ReferenceEquals(actual, expected);
    }

    protected override Node? Solve(Node? input) => CloneGraphBfs(input);

}
