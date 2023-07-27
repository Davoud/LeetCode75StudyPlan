namespace LeetCode75StudyPlan.BinarySearch;

internal class TimeBasedKeyValueStore : ITestable
{
    public class TimeMap
    {
        private readonly Dictionary<string, string> _dic = new Dictionary<string, string>();
        public TimeMap()
        {

        }

        public void Set(string key, string value, int timestamp)
        {

        }

        public string Get(string key, int timestamp)
        {
            return key;
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
    }
}
