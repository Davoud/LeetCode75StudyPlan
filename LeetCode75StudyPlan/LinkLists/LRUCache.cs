using System.Runtime.Versioning;
using System.Security.Cryptography;
using System.Threading;

namespace LeetCode75StudyPlan.LinkLists;

internal class LRUCache: ITestable
{
    class Node
    {
        public int value;
        public int key;
        public Node? next;
        public Node? prev;
        public override string ToString() => $"({key}:{value})";
    }

    private int _capacity;
    private Dictionary<int, Node> _cache;
    private Node? _first;
    private Node? _last;


    public LRUCache(int capacity)
    {
        _cache = new(capacity);
        _capacity = capacity;        
    }

    public int Get(int key)
    {
        if (_cache.TryGetValue(key, out var node))
        {
            if(node == _first)
            {
                return node.value;
            }

            if(node == _last) 
                RemoveLast();                   
            else 
                Remove(node);            
            
            AddFirst(node);
            Console.WriteLine($"Get {key} => {this} (last: {_last})");
            return node.value;
        }
        else
        {
            return -1;
        }
    }

    public void Put(int key, int value)
    {
        if (_cache.TryGetValue(key, out var node))
        {
            node.value = value;
            if (node == _last)
            {
                RemoveLast();
            }
            else
            {
                Remove(node);
            }
            AddFirst(node);
        }
        else if (_cache.Count >= _capacity)
        {
            if (RemoveLast() is int removedKey)
            {
                _cache.Remove(removedKey);
            }
            _cache[key] = AddFirst(new() { key = key, value = value });
        }
        else
        {
            _cache[key] = AddFirst(new() { key = key, value = value }); ;
        }

        Console.WriteLine($"Put {key}:{value} => {this} (last: {_last})");
    }

    private int? RemoveLast()
    {
        if (_last != null)
        {
            int key = _last.key;

            if(_last == _first)
            {
                _last = null;
                _first= null;
                return key;
            }

            var node = _last.prev;
            if (node != null) node.next = null;
            _last.next = null;
            _last.prev = null;            
            _last = node;
            return key;
        }

        return null;
    }

    private void Remove(Node node)
    {
        if(node.prev != null)
            node.prev.next = node.next;
        
        if(node.next != null)        
            node.next.prev = node.prev;
        
        node.next = null;
        node.prev = null;
    }

    private Node AddFirst(Node node)
    {        
        if (_first != null)
        {
            node.next = _first;
            _first.prev = node;
            node.prev = null;
            _first = node;
        }
        else
        {
            _first = node;
            _last = node;
        }
        return node;
    }


    public override string ToString()
    {
        var node = _first;
        StringBuilder sb = new("{ ");
        int max = _capacity + 1;
        while (node != null)
        {
            sb.Append(node).Append(' ');
            node = node.next;

            if(--max <= 0)
            {
                sb.Append("<cycle>");
                break;
            }
        }
        return sb.Append('}').ToString();
    }

    void ITestable.RunTests()
    {
        int testCaseNo = 3;
        LRUCache lru;
        if (testCaseNo == 1)
        {
            lru = new LRUCache(2);
            lru.Put(1, 1);          // cache is {1=1}
            lru.Put(2, 2);          // cache is {1=1, 2=2}
            Assert(lru.Get(1), 1);  // return 1          
            lru.Put(3, 3);          // LRU key was 2, evicts key 2, cache is {1=1, 3=3}
            Assert(lru.Get(2), -1); // returns -1 (not found)            
            lru.Put(4, 4);          // LRU key was 1, evicts key 1, cache is {4=4, 3=3}
            Assert(lru.Get(1), -1); // return -1 (not found)            
            Assert(lru.Get(3), 3);  // return 3            
            Assert(lru.Get(4), 4);  // return 4            
        }
      
        else if (testCaseNo == 2) 
        {
            lru = new LRUCache(1);
            lru.Put(2, 1);
            Assert(lru.Get(2), 1);            
            lru.Put(3, 2);
            Assert(lru.Get(2), -1);            
            Assert(lru.Get(3), 2);            
        }

       
        else if(testCaseNo == 3)
        {
            lru = new LRUCache(2);
            lru.Put(2, 1);
            lru.Put(3, 2);
            Assert(lru.Get(3), 2);
            Assert(lru.Get(2), 1);
            lru.Put(4, 3);
            Assert(lru.Get(2), 1);
            Assert(lru.Get(3), -1);
            Assert(lru.Get(4), 3);

        }

        void Assert(int actual, int expected)
        {
            if(actual != expected)
            {
                $"Exptected {expected}, Acuual {actual}".WriteLine();
            }
        }
    }




}
