using System.Runtime.Versioning;

namespace LeetCode75StudyPlan.LinkLists;

internal class ReverseNodesInGroup : Solution<(ListNode? head, int k), ListNode?>
{
    protected override string Title => "25. Reverse Nodes in k-Group (https://leetcode.com/problems/reverse-nodes-in-k-group/)";

    protected override IEnumerable<((ListNode?, int), ListNode?)> TestCases
    {
        get
        {
            yield return ( (lx[1, 2, 3, 4, 5], 3), lx[3, 2, 1, 4, 5] );
            yield return ( (lx[1, 2, 3, 4, 5], 2), lx[2, 1, 4, 3, 5] );
        }
    }

    protected override ListNode? Solve((ListNode? head, int k) input)
    {
        if (input.head == null)
        {
            return null;
        }
        else
        {            
            return ReverseKGroup(input.head, input.k);
        }
    }

    public ListNode? ReverseKGroup(ListNode? head, int k)
    {
        ListNode first = new(0, head);
        ListNode? curr = head;
        ListNode? tail = first;
        
        while (curr != null)
        {
            int i = k;
            while (curr != null && i > 0)
            {
                curr = curr.next;
                i--;
            }

            if (i == 0)
            {
                (ListNode? h, ListNode t) = Reverse(head, k);
                tail.next = h;
                t.next = curr;
                tail = t;
                head = curr;
            }
        }

        return first.next;
    }

    public (ListNode?, ListNode) Reverse(ListNode? head, int k)
    {
        ListNode? curr = head;
        ListNode? prev = null;
        
        while (curr != null && k > 0)
        {
            var next = curr.next;
            curr.next = prev;
            prev = curr;
            curr = next;
            k--;
        }        

        return (prev, head);
    }

    public (ListNode? rev, ListNode? rest) StackReverse(ListNode head, int k)
    {
        var n = head;
        var stack = new Stack<ListNode>();
        int count = 0;
        while (n != null && count++ < k)
        {
            var next = n.next;
            n.next = null;
            stack.Push(n);            
            n = next;            
        }

        if(count < k)
        {
            return (head, null);
        }

        ListNode? first = new ListNode(0);
        ListNode? node = first;
        while(stack.Count > 0)
        {                        
            node.next = stack.Pop();
            node = node.next;            
        }

        return (first.next, n);
    }
}
