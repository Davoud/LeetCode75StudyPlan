namespace LeetCode75StudyPlan.LinkLists;

internal class RemoveNthFromEndOfList : Solution<(ListNode? head, int n), ListNode?>
{
    protected override string Title => "19. Remove Nth Node From End of List";

    public ListNode? RemoveNthFromEnd(ListNode? head, int n)
    {
        if (head is null) return null;
        if (n == 0) return head;

        int k = 1;
        ListNode? beforNth = null;

        for (ListNode? node = head; node != null; node = node?.next)
        {
            if (k > n)
                beforNth = beforNth?.next ?? head;

            k++;
        }

        if (beforNth != null)
            beforNth.next = beforNth?.next?.next;
        else if (k == n + 1)
            head = head.next;

        return head;
    }

    protected override IEnumerable<((ListNode? head, int n), ListNode?)> TestCases =>
        new[]
        {
            ((lx[1], 1),             lx.Empty),
            ((lx[1, 2, 3, 4, 5], 5), lx[2, 3, 4, 5]),
            ((lx[1, 2, 3, 4, 5], 2), lx[1, 2, 3, 5]),
            ((lx[1, 2, 3, 4, 5], 1), lx[1, 2, 3, 4]),
            ((lx[1,2], 1),           lx[1]),
            ((lx[1, 2, 3, 4, 5, 0, 6, 7, 8, 9], 5), lx[1, 2, 3, 4, 5, 6, 7, 8, 9])
        };

    protected override ListNode? Solve((ListNode? head, int n) input) => RemoveNthFromEnd(input.head, input.n);


}


