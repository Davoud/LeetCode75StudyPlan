namespace LeetCode75StudyPlan.LinkLists;

internal class MergeMultipleSortedLists : Solution<ListNode?[], ListNode?>
{
    protected override string Title => "23. Merge k Sorted Lists";

    protected override IEnumerable<(ListNode?[], ListNode?)> TestCases
    {
        get
        {
            yield return (Arr(lx[1, 4, 5], lx[1, 3, 4], lx[2, 6]), lx[1, 1, 2, 3, 4, 4, 5, 6]);
            yield return (Array.Empty<ListNode>(), lx.Empty);
            yield return (Arr(lx.Empty), lx.Empty);
        }
    }

    public ListNode? MergeKListsRecursive(ListNode?[] lists)
    {
        if (lists == null || lists.Length == 0) return null;

        var head = lists[0];
        for(int i = 1; i < lists.Length; i++)
        {
            head = MergeTwo(head, lists[i]);
        }
        return head;
    }

    public ListNode? MergeKLists1(ListNode?[] lists)
    {
        var head = new ListNode(0);
        var last = head;

        while (true)
        {
            var min = int.MaxValue;
            var index = -1;
            for (int i = 0; i < lists.Length; i++)
            {
                if (lists[i] is ListNode node && node.val < min)
                {
                    min = node.val;
                    index = i;
                }
            }
            if (index >= 0 && lists[index] is ListNode minHeads)
            {
                lists[index] = minHeads.next;
                last.next = new ListNode(minHeads.val);
                last = last.next;
            }
            else
            {
                break;
            }
        }

        return head.next;
    }

    public ListNode? MergeKLists(ListNode?[] lists)
    {
        var head = new ListNode(0);
        var last = head;

        while (true)
        {
            var min = int.MaxValue;
            var index = -1;
            for (int i = 0; i < lists.Length; i++)
            {
                if (lists[i]?.val < min)
                {
                    min = lists[i].val;
                    index = i;
                }
            }
            if (index >= 0)
            {
                var minHeads = lists[index];
                lists[index] = minHeads.next;
                minHeads.next = null;
                last.next = minHeads;// new ListNode(minHeads.val);
                last = last.next;
            }
            else
            {
                break;
            }
        }

        return head.next;
    }

    protected override ListNode? Solve(ListNode?[] lists)
    {
        //return MergeKLists(lists);

        return MergeKListsRecursive(lists);
    }

    private ListNode? MergeTwo(ListNode? list1, ListNode? list2)
    {
        if (list1 == null) return list2;
        if (list2 == null) return list1;            
        if (list1.val < list2.val) 
        {
            list1.next = MergeTwo(list1.next, list2);
            return list1;    
        }
        else
        {
            list2.next = MergeTwo(list1, list2.next);
            return list2;
        }
    }
}
