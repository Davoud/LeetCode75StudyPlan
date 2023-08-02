namespace LeetCode75StudyPlan.BinarySearch;

internal class TimeBasedKeyValueStore : ITestable
{
    public class Entry
    {
        private readonly List<(int timestamp, string value)> _items;

        public Entry(int timestamp, string value)
        {
            _items = new List<(int, string)>() { (timestamp, value) };
        }

        public void Add(int timestamp, string value)
        {
            _items.Add((timestamp, value));
        }

        public string GetValue(int timestamp)
        {
            if (timestamp < _items[0].timestamp) return "";
            int l = 0, r = _items.Count - 1;
            while(l < r)
            {
                int m = (l + r) >> 1;
                if(timestamp > _items[m].timestamp)
                {
                    l = m + 1;
                }
                else
                {
                    r = m;
                }
            }
            return l > 0 && _items[l].timestamp > timestamp ? _items[l - 1].value : _items[l].value;
        }
    }

    public class TimeMap
    {
        private readonly Dictionary<string, Entry> _dic;
        public TimeMap()
        {
            _dic = new Dictionary<string, Entry>();   
        }

        public void Set(string key, string value, int timestamp)
        {
            if(_dic.TryGetValue(key, out var entry))
            {
                entry.Add(timestamp, value);
            }
            else
            {
                _dic.Add(key, new Entry(timestamp, value));
            }
        }

        public string Get(string key, int timestamp)
        {
            if (_dic.TryGetValue(key, out var entry))
            {
                return entry.GetValue(timestamp);
            }
            else
            {
                return "";
            }
        }
         
    }

    void ITestable.RunTests()
    {
        "\n981. Time Based Key-Value Store".WriteLine();        
                
        Apply(new(), new object[] 
        {
            new Set("foo", "bar", 1),
            new Get("foo", 1, "bar"),
            new Get("foo", 3, "bar"),
            new Set("foo", "bar2", 4),
            new Get("foo", 4, "bar2"),
            new Get("foo", 5, "bar2")
        });

        
        Apply(new(), new object[]
        {
            new Set("love", "h", 10),
            new Set("love", "l", 20),
            new Get("love", 5, ""),
            new Get("love", 10, "h"),
            new Get("love", 15, "h"),
            new Get("love", 20, "l"),
            new Get("love", 25, "l")
        });

    }

    private record Set(string Key, string Value, int Timestamp);
    private record Get(string Key, int Timestamp, string Value); 

    private void Apply(TimeMap map, object[] operations)
    {
        foreach (var operation in operations)
        {
            if(operation is Set v)
            {
                map.Set(v.Key, v.Value, v.Timestamp);
            }
            else if(operation is Get g)
            {
                var actual = map.Get(g.Key, g.Timestamp);
                WriteResult(actual == g.Value, g.Value, actual);                                
            }
        }
        string.Empty.WriteLine();
    }
}
