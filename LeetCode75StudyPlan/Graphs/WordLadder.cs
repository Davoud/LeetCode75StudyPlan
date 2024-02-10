
using LeetCode75StudyPlan.Graphs.Imp;
using System.Collections;
using System.ComponentModel;

namespace LeetCode75StudyPlan.Graphs;

internal class WordLadder : Solution<(string beginWord, string endWord, IList<string> wordList), int>
{
    public int LadderLength(string beginWord, string endWord, IList<string> wordList)
    {
        int end = wordList.IndexOf(endWord);
        if (end == -1) return 0;

        int begin = wordList.IndexOf(beginWord);
        if (begin == -1)
        {
            wordList.Add(beginWord);
            begin = wordList.Count - 1;
        }
        
        List<int>?[] graph = new List<int>?[wordList.Count];
        for(int i = 0; i < wordList.Count - 1; i++)
        {
            string word = wordList[i];                    
            for(int j = i + 1; j < wordList.Count; j++)
            {
                if(IsAdj(word, wordList[j]))
                {
                    (graph[i] ??= new()).Add(j);
                    (graph[j] ??= new()).Add(i);
                }
            }
        }
               
        int[] parents = Bfs(graph, begin);                
        return FindPath(begin, end);

        int FindPath(int v, int w)
        {
            if (v == w)
            {
                return 1;
            }
            else
            {
                int p = parents[w];
                if(p == -1) return 0;                
                return FindPath(v, p) + 1;
            }                       
        }
    }

    private static int[] Bfs(List<int>?[] graph, int begin)
    {
        BitArray discovared = new(graph.Length);
        int[] parent = new int[graph.Length];
        Array.Fill(parent, -1);

        Queue<int> q = new();
        q.Enqueue(begin);
        discovared[begin] = true;

        while (q.Count > 0)
        {
            int v = q.Dequeue();
            if (graph[v] is List<int> adj)
            {
                foreach (int y in adj)
                {
                    if (!discovared[y])
                    {
                        q.Enqueue(y);
                        discovared[y] = true;
                        parent[y] = v;
                    }
                }
            }
        }
        return parent;
    }

    static bool IsAdj(string x, string y)
    {
        int cmp = 0;
        for (int i = 0; i < x.Length; i++)
        {
            cmp += x[i] - y[i] == 0 ? 0 : 1;
        }
        return cmp == 1;
    }

    public int _LadderLength(string beginWord, string endWord, IList<string> wordList)
    {
        
        int end = wordList.IndexOf(endWord);        
        if (end == -1) return 0;

        int begin = wordList.IndexOf(beginWord);
        if (begin == -1)
        {
            wordList.Add(beginWord);
            begin = wordList.Count - 1;
        }
        

        var graph = new GraphInt(wordList.Count, GraphType.Undirected);

        for(int i = 0; i < wordList.Count - 1; i++)
        {
            string word = wordList[i];        
            
            for(int j = i + 1; j < wordList.Count; j++)
            {
                if(IsAdj(word, wordList[j]))
                {
                    graph.AddEdge(i, j);
                }
            }
        }

        for(int i = 0; i < graph.VertexCount; i++)
        {
            Console.WriteLine($"{i}:{wordList[i]} => {string.Join(", ", graph[i].Select(v => wordList[v]))}\n");            
        }

        var bfs = new Bfs(graph, begin);
        var count = 0;
        foreach(var v in bfs.ShortestPath(begin, end))
        {
            count++;
            Console.Write($"{wordList[v]} => ");
        }

        return count;

        

        
    }
    protected override string Title => "127. Word Ladder";

    protected override IEnumerable<((string beginWord, string endWord, IList<string> wordList), int)> TestCases
    {
        get
        {
            yield return (("hot", "dog", ["hot", "dog"]), 0);
            yield return (("hit", "cog", ["hot", "dot", "dog", "lot", "log", "cog"]), 5);
            yield return (("a", "c", ["a", "b", "c"]), 2);
        }
    }

    protected override int Solve((string beginWord, string endWord, IList<string> wordList) input)
        => LadderLength(input.beginWord, input.endWord, input.wordList);
}
