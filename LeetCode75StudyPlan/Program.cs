using LeetCode75StudyPlan;
using LeetCode75StudyPlan.Heaps;
using LeetCode75StudyPlan.Tries;
using System;

var nums = @int[1, 4, 4, 7, 10, -1] ;
var pq = new PriorityQueue<int, int>(nums.Select(i => (i, i)));


//((ITestable)new KthLargestElementArray()).RunTests();