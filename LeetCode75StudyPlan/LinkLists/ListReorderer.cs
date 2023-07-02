
namespace LeetCode75StudyPlan.LinkLists;

internal class ListReorderer : ITestable
{

    public void ReorderList(ListNode? head)
    {

        if (head != null && OneBeforeLast(head) is ListNode node && node.next is ListNode last)
        {
            node.next = null;

            var tail = head.next;

            head.next = last;

            last.next = tail;

            ReorderList(tail);
        }

    }

    private ListNode? OneBeforeLast(ListNode head)
    {
        ListNode? one = null;
        ListNode node = head;
        while (node.next != null)
        {
            one = node;
            node = node.next;
        }
        return one;
    }

    private ListNode Last(ListNode head)
    {
        if (head.next == null)
        {
            return head;
        }
        else
        {
            var last = Last(head.next);

            return last;
        }
    }

    void ITestable.RunTests()
    {
        "143. Reorder List".WriteLine();

        (ListNode input, ListNode output)[] cases = new[]
        {
            (lx[1, 2, 3, 4],    lx[1, 4, 2, 3]),
            (lx[1, 2, 3, 4, 5], lx[1, 5, 2, 4, 3]),
            (lx[1], lx[1]),
        };


        foreach (var (input, output) in cases)
        {
            ReorderList(input);
            if (input == output)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Exptected: {output}, Actual: {input}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Exptected: {output}, Actual: {input}");
            }
            Console.ResetColor();

        }

    }
}
