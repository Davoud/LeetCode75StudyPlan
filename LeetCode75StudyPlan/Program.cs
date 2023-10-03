using LeetCode75StudyPlan;
using LeetCode75StudyPlan.Heaps;
using LeetCode75StudyPlan.Backtracking;
using System;
using System.Collections;
using System.Numerics;

((ITestable)new WordSearch()).RunTests();

//int n = 3;
//int m = 7;

//var mat = new bool[n, m];
//var b = new BitArray(n * m);

//for (int i = 0; i < n; i++)
//{
//    for (int j = 0; j < m; j++)
//    {
//        mat[i, j] = (i + j) % 2 == 0;
//        b[(i * m) + j] = mat[i, j];
//    }
//}

//for (int i = 0; i < n; i++)
//{
//    for (int j = 0; j < m; j++)
//    {
//        var k = (i * m) + j;
//        Console.WriteLine($"({i},{j}) : ({k}) => {mat[i, j] == b[k]}");
//    }
//}
