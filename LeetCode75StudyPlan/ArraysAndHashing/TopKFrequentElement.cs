namespace LeetCode75StudyPlan.ArraysAndHashing;

public class TopKFrequentElement
{

    public static class Tests
    {
        public static void Run()
        {
            var nums = new int[] { 4, 1, -1, 2, -1, 2, 3 };
            int k = 2;
            TopKFrequentElement topKFrequent = new();
            int[] output = topKFrequent.TopKFrequent(nums, k);
            output.Dump(); 
        }
    }

    class Cmp : IComparer<(int key, int value)>
    {
        public int Compare((int key, int value) x, (int key, int value) y)
        {
            var cr = y.value.CompareTo(x.value);
            return cr != 0 ? cr : y.key.CompareTo(x.key);
        }
    }

    public int[] TopKFrequent1(int[] nums, int k)
    {
        var count = new Dictionary<int, int>();
        var set = new SortedSet<(int key, int value)>(new Cmp());
        
        foreach (int num in nums)
            count[num] = count.TryGetValue(num, out int value) ? value + 1 : 1;            
        
        foreach(KeyValuePair<int, int> keyValue in count)
            set.Add((keyValue.Key, keyValue.Value));

        var result = new int[k];
        int i = 0;
        foreach(var (key, _) in set)
        {
            result[i] = key;
            i++;
            if (i >= k) break;            
        }

        return result;
    }

    public int[] TopKFrequent(int[] nums, int k)
    {
        Dictionary<int, int> map = new();
        PriorityQueue<int, int> pq = new(Comparer<int>.Create((a,b) => b - a));

        foreach (int num in nums)
        {
            if (!map.TryAdd(num, 1))
            {
                map[num]++;
            }
        }

        foreach(KeyValuePair<int, int> kv in map)
        {
            pq.Enqueue(kv.Key, kv.Value);
        }

        var result = new int[k];
        for(int i = 0; i < k; i++)
        {
            result[i] = pq.Dequeue();
        }

        return result;
    }
}