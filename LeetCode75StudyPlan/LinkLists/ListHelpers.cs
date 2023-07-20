using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode75StudyPlan.LinkLists
{
    internal static class ListHelpers
    {
        public static IEnumerable<ListNode> Items(this ListNode? node)
        {            
            ListNode? n = node;
            while(n != null)
            {
                yield return n;
                n = n.next;
            }
        }

        public static IEnumerable<int> Values(this ListNode? node)
        {
            ListNode? n = node;
            while (n != null)
            {
                yield return n.val;
                n = n.next;
            }
        }

    }
}
