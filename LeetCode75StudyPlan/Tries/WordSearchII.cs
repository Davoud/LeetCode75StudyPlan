
using System.ComponentModel;
using System.Data;
using System.Transactions;

namespace LeetCode75StudyPlan.Tries;

internal class WordSearchIIb : Solution<(char[][] board, string[] words), IList<string>>
{
    private record Node(int Row, int Col);

    private char[][] _board = null!;
    private int N;
    private int M;
    public IList<string> FindWords(char[][] board, string[] words)
    {
        _board = board;
        N = _board.Length;
        M = _board[0].Length;
        return words.Where(w => Find(w.AsSpan())).ToList();
    }

    private string S(Node n) => $"{n.Row}{_board[n.Row][n.Col]}{n.Col}";

    private bool Find(ReadOnlySpan<char> word)
    {
        foreach (Node node in FindAll(word[0]))
        {
            if (word.Length == 1 && word[0] == _board[node.Row][node.Col])
                return true;
                        
            if (Follow(node, word[1..], new()))
                return true;                        
        }
        return false;
    }

    private bool Follow(Node node, ReadOnlySpan<char> word, HashSet<Node> visited)
    {        
        if (word.Length == 0)
            return false;

        var c = word[0];
        
        foreach (var adj in AdjecntsOf(node))
        {
            
            if (_board[adj.Row][adj.Col] == c)
            {                
                visited.Add(node);
                //Console.WriteLine($"{c}: {S(node)} | {S(adj)}");
                if (!visited.Contains(adj) && (word.Length == 1 || Follow(adj, word[1..], visited)))
                {
                    return true;
                }
            }
        }

        return false;

    }

    private IEnumerable<Node> FindAll(char c)
    {
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < M; j++)
            {
                if (c == _board[i][j])
                    yield return new Node(i, j);
            }
        }
    }

    private IEnumerable<Node> AdjecntsOf(Node node)
    {
        int x = node.Row - 1;
        if (x >= 0) yield return node with { Row = x };

        x = node.Col - 1;
        if (x >= 0) yield return node with { Col = x };

        x = node.Row + 1;
        if (x < N) yield return node with { Row = x };

        x = node.Col + 1;
        if (x < M) yield return node with { Col = x };
    }

    protected override string Title => "212. Word Search II";

    protected override IEnumerable<((char[][] board, string[] words), IList<string>)> TestCases
    {
        get
        {
            char[][] board = [
                ['b', 'a', 'b', 'a'],
                ['b', 'b', 'b', 'a'],
                ['b', 'b', 'b', 'a'],
                ['b', 'a', 'a', 'a'],
                ['a', 'a', 'a', 'a'],
                ['a', 'a', 'a', 'a']];
         
            yield return ((board, ["aabaaaaab"]), ["aabaaaaab"]);

            //yield break;

            board = [
                ['a', 'b', 'c'],
                ['a', 'e', 'd'],
                ['a', 'f', 'g']];

            yield return ((board, ["eaafgdcba"]), ["eaafgdcba"]);

            //yield break;

            yield return ((board, ["abcdefg", "gfedcbaaa", "eaabcdgfa", "befa", "dgc", "ade"]), 
                ["abcdefg", "gfedcbaaa", "eaabcdgfa", "befa"]);

            yield return (([['a', 'a']], ["aaa"]), new List<string>());

            yield return (([['a']], ["a"]), ["a"]);

            board = [
                ['o', 'a', 'a', 'n'],
                ['e', 't', 'a', 'e'],
                ['i', 'h', 'k', 'r'],
                ['i', 'f', 'l', 'v']];
            yield return ((board, ["oath", "pea", "eat", "rain"]), ["oath", "eat"]);

            yield return (([['a']], ["b"]), []);

            yield return (([['a', 'b'], ['c', 'd']], ["abcd"]), []);
            
        }
    }

    protected override bool IsEqual(IList<string> actual, IList<string> expected)
    {
        return actual.SequenceEqual(expected);
    }

    protected override IList<string> Solve((char[][] board, string[] words) input)
        => FindWords(input.board, input.words);
}

internal class WordSearchII : Solution<(char[][] board, string[] words), IList<string>>
{

    private readonly Trie _trie = new();

    private class Trie
    {
        private readonly Node root = new();
        public virtual void Insert(params char?[] word)
        {
            var node = root;
            foreach (char? c in word)
                if (c.HasValue) node = node.InsertOrGet(c.Value);
            node.Mark();
        }

        public virtual bool Search(params char[] word)
        {
            var node = root;
            foreach (char c in word)
            {
                if (node[c] is Node n)
                {
                    node = n;
                }
                else
                {
                    node = null;
                    break;
                }
            }
            return node?.IsMarked ?? false;
        }

        class Node
        {
            private Dictionary<char, Node?> _nodes = new();
            private bool _isMarked;
            public Node? this[char c]
            {
                get => _nodes.TryGetValue(c, out Node? node) ? node : null;
                set => _nodes[c] = value;
            }
            public Node InsertOrGet(char c)
            {
                if (_nodes.TryGetValue(c, out Node? node))
                {
                    return node!;
                }
                else
                {
                    node = new Node();
                    _nodes[c] = node;
                    return node;
                }
            }

            public bool IsMarked => _isMarked;
            public void Mark() => _isMarked = true;

            public IEnumerable<Node?> Children => _nodes.Values;
        }
    }

    private class Board
    {
        public readonly int N;
        public readonly int M;
        private readonly char[][] _board;
        public Board(char[][] board)
        {
            _board = board;
            N = board.Length;
            if (N > 0)
                M = board[0].Length;
        }

        public IEnumerable<char?> AdjecntsOf(int i, int j)
        {
            (int x, int y) = (i - 1, j);
            if (x >= 0) yield return _board[x][y];

            (x, y) = (i, j - 1);
            if (y >= 0) yield return _board[x][y];

            (x, y) = (i + 1, j);
            if (x < N) yield return _board[x][y];

            (x, y) = (i, j + 1);
            if (y < M) yield return _board[x][y];

            yield return null;
        }

    }

    public IList<string> FindWords(char[][] board, string[] words)
    {
        var found = new List<string>();

        Board brd = new(board);

        for (int i = 0; i < brd.N; i++)
        {
            for (int j = 0; j < brd.M; j++)
            {
                char c = board[i][j];
                foreach (char? a in brd.AdjecntsOf(i, j))
                    _trie.Insert(c, a);
            }
        }

        foreach (string word in words)
        {
            if (word.Length == 1)
            {
                if (_trie.Search(word[0]))
                    found.Add(word);
            }
            else
            {
                bool? trueForAll = null;
                for (int i = 0; i < word.Length - 1; i++)
                    trueForAll = (trueForAll ?? true) && _trie.Search(word[i], word[i + 1]);

                if (trueForAll ?? false) found.Add(word);
            }
        }

        return found;
    }


    protected override string Title => "212. Word Search II";

    protected override IEnumerable<((char[][] board, string[] words), IList<string>)> TestCases
    {
        get
        {
            char[][] board = [['a', 'a']];
            yield return ((board, ["aaa"]), new List<string>());    

            board = [['a']];
            yield return ((board, ["b"]), new List<string>());

            board = [['a']];
            yield return ((board, ["a"]), ["a"]);

            board = [
                ['o', 'a', 'a', 'n'],
                ['e', 't', 'a', 'e'],
                ['i', 'h', 'k', 'r'],
                ['i', 'f', 'l', 'v']];

            yield return ((board, ["oath", "pea", "eat", "rain"]), ["oath", "eat"]);

            board = [['a', 'b'], ['c', 'd']];
            yield return ((board, ["abcd"]), []);




        }
    }

    protected override bool IsEqual(IList<string> actual, IList<string> expected)
    {
        return actual.SequenceEqual(expected);
    }

    protected override IList<string> Solve((char[][] board, string[] words) input)
        => FindWords(input.board, input.words);

}
