
using LeetCode75StudyPlan.Stack;
using static System.Net.Mime.MediaTypeNames;

namespace LeetCode75StudyPlan.LinkLists;

internal class ListReorderer : ITestable
{    
    public void ReorderList(ListNode? head)
    {
        if (head == null) return;
        
        (var slow, var fast) = (head, head);
        while (slow.next is not null && fast?.next?.next is not null)
        {
            (slow, fast) = (slow.next, fast.next.next);            
        }

        (ListNode? prev, ListNode? current) = (null, slow.next);
        while(current is not null)
        {
            var next = current.next;
            current.next = prev;
            prev = current;
            current = next;
        }
        slow.next = null;

        (var h1, var h2) = (head, prev);
        while(h2 is not null)
        {
            var next = h1.next;
            h1.next = h2;
            h1 = h2;
            h2 = next;
        }
    }

    private ListNode FindHalf(ListNode head)
    {
        var slow = head;
        var fast = head;
        while(slow.next != null && fast?.next?.next != null)
        {
            slow = slow.next;
            fast = fast.next.next;
        }
        return slow;
    }

    
  

    public void ReorderList1(ListNode? head)
    {
        if (head != null && OneBeforeLast(head) is ListNode node && node.next is ListNode last)
        {
            node.next = null;
            var tail = head.next;
            head.next = last;
            last.next = tail;
            ReorderList1(tail);
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
   

    void ITestable.RunTests()
    {
        "143. Reorder List".WriteLine();

        (ListNode input, ListNode output)[] cases = new[]
        {
           (lx[1, 2, 3, 4, 5, 6, 7], lx[1, 7, 2, 6, 3, 5, 4]),
           (lx[1, 2, 3, 4, 5], lx[1, 5, 2, 4, 3]), 
           (lx[1, 2, 3, 4],    lx[1, 4, 2, 3]),
           (lx[1, 2, 3, 4, 5, 6, 7, 8], lx[1, 8, 2, 7, 3, 6, 4, 5]),
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
