using System.Collections;

namespace LeetCode75StudyPlan.Tries;

class HashTrie : ITrie
{
    protected TrieNode root;

    public HashTrie() => root = new TrieNode();

    public virtual void Insert(string word)
    {
        var node = root;
        foreach (char c in word)
            node = node.InsertOrGet(c);
        node.Mark();
    }

    public virtual bool Search(string word)
    {
        var node = root;
        foreach (char c in word)
        {
            if (node[c] is TrieNode n)
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

    public virtual bool StartsWith(string prefix)
    {
        var node = root;
        foreach (char c in prefix)
        {
            if (node[c] is TrieNode n)
                node = n;
            else
                return false;
        }
        return true;
    }

    protected class TrieNode
    {
        private Dictionary<char, TrieNode?> _nodes = new();
        private bool _isMarked;
        public TrieNode? this[char c]
        {
            get => _nodes.TryGetValue(c, out TrieNode? node) ? node : null;
            set => _nodes[c] = value;
        }
        public TrieNode InsertOrGet(char c)
        {
            if (_nodes.TryGetValue(c, out TrieNode? node))
            {
                return node!;
            }
            else
            {
                node = new TrieNode();
                _nodes[c] = node;
                return node;
            }
        }

        public bool IsMarked => _isMarked;
        public void Mark() => _isMarked = true;

        public IEnumerable<TrieNode?> Children => _nodes.Values;
    }
}