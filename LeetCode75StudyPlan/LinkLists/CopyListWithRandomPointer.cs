
namespace LeetCode75StudyPlan.LinkLists;

public class Node
{
    public int val;
    public Node? next;
    public Node? random;

    public Node(int _val)
    {
        val = _val;
        next = null;
        random = null;
    }

    public override string ToString()
    {
        string rnd = random != null ? random.val.ToString() : "null";
        return $"[{val},{rnd}]";
    }
}

internal class CopyListWithRandomPointer : Solution<Node?, Node?>, ITestable
{
    protected override string Title => "138. Copy List with Random Pointer";


    protected override IEnumerable<(Node?, Node?)> TestCases
    {
        get
        {
            var input = MakeListFrom(new[] { (7, -1), (13, 0), (11, 4), (10, 2), (1, 0) });
            var output = MakeListFrom(new[] { (7, -1), (13, 0), (11, 4), (10, 2), (1, 0) });

            yield return (input, output);

            input = MakeListFrom(new[] { (3, -1),(3, 0),(3, -1) });
            output = MakeListFrom(new[] { (3, -1), (3, 0), (3, -1) });

            yield return (input, output);
        }
    }

    private static Node MakeListFrom((int value, int random)[] values)
    {
        var list = new Node[values.Length];

        for (int i = 0; i < values.Length; i++) 
            list[i] = new Node(values[i].value);

        for (int i = 0; i < values.Length; i++)
        {
            if(i < values.Length - 1)
                list[i].next = list[i + 1];

            var (_, random) = values[i];
            if (random >= 0)
            {
                list[i].random = list[random];
            }
        }

        return list[0];

      
    }
    protected override Node? Solve(Node? input)
    {
        if (input == null) return null;

        var dic = new Dictionary<Node, Node>();
        Node? head = input;
        Node? copy = null;
        Node? prev = null;

        while(head != null)
        {
            var cp = new Node(head.val)
            {
                random = head.random
            };
            if (prev == null)
            {
                copy = cp;
                prev = copy;
            }
            else
            {
                prev.next = cp;
            }            

            dic[head] = cp;
            head = head.next;            
            prev = cp;
        }

        head = copy;
        while(head != null)
        {
            if(head.random != null && dic.TryGetValue(head.random, out var node))
            {
                head.random = node;
            }
            head = head.next;
        }

        dic.Clear();

        return copy;
    }

    private string AsString(Node? input)
    {
        StringBuilder sb = new("[");
        while(input!= null)
        {
            sb.Append(input).Append(',');
            input = input.next;
        }
        return sb.Remove(sb.Length - 1, 1).Append(']').ToString();
    }

    void ITestable.RunTests()
    {
        Console.WriteLine(Title);
        foreach(var item in TestCases)
        {
            var cp = Solve(item.Item1);
            Console.WriteLine(AsString(item.Item1));
            Console.WriteLine(AsString(cp));
        }
    }
}
