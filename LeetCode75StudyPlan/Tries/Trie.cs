namespace LeetCode75StudyPlan.Tries;

internal class Trie : ITrie
{
    private class TrieNode
    {  
        private TrieNode?[] _nodes = new TrieNode?[26];
        private bool _isMarked;
        public TrieNode? this[char c]
        {
            get => _nodes[c - 'a'];
            set => _nodes[c - 'a'] = value;
        }

        public TrieNode InsertOrGet(char c)
        {
            int i = c - 'a';
            if (_nodes[i] is TrieNode n) return n;                        
            _nodes[i] = new TrieNode();
            return _nodes[i]!;            
        }

        public bool IsMarked => _isMarked;
        public void Mark() => _isMarked = true;

        public override string ToString()
        {
            var sb = new StringBuilder(IsMarked ? "Node(marked)[" : "Node[");
            for(var i = 0; i < 26; i++) 
            {
                if (_nodes[i] != null)
                {
                    sb.Append((char)(i + 'a'));
                }
            }                  
            return sb.Append(']').ToString();
        }

    }

    private TrieNode root;

    public Trie() => root = new TrieNode();

    public void Insert(string word)
    {
        var node = root;
        foreach(char c in word)
            node = node.InsertOrGet(c);         
        node.Mark();
    }

    public bool Search(string word)
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

    public bool StartsWith(string prefix)
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

    public override string ToString()
    {
        return root.ToString();
    }

}
