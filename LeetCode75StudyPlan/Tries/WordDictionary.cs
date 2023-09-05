using LeetCode75StudyPlan.LinkLists;
using LeetCode75StudyPlan.Trees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode75StudyPlan.Tries;


public class WordDictionary : ITrie
{

    private TrieNode root;

    public WordDictionary()
    {
        root = new TrieNode();
    }

    public void AddWord(string word)
    {
        var node = root;
        foreach (char c in word)
            node = node.InsertOrGet(c);
        node.Mark();
    }

    public void Insert(string word) => AddWord(word);

    public bool Search(string word) => Search(root, word.AsSpan())?.IsMarked ?? false;

    public bool StartsWith(string prefix) => Search(root, prefix.AsSpan()) != null;

    private TrieNode? Search(TrieNode? node, ReadOnlySpan<char> word)
    {
        if (node == null || word.Length == 0) return node;

        char c = word[0];

        if (c == '.')
        {
            foreach (TrieNode? child in node.Children)
            {
                var s = Search(child, word[1..]);
                if (s?.IsMarked ?? false) return s;
            }
            return null;
        }

        if (node[c] is TrieNode n)
        {
            return Search(n, word[1..]);
        }

        return null;
    }

    class TrieNode
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


