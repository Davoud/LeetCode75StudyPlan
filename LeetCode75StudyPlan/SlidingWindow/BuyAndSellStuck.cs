
namespace LeetCode75StudyPlan.SlidingWindow;

internal class BuyAndSellStuck: ITestable
{
    void ITestable.RunTests()
    {
        Console.WriteLine("121. Best Time to Buy and Sell Stock");
        (int[], int)[] cases =
        [
            ( [7,1,5,3,6,4], 5 ),
            ( [7,6,4,3,1], 0 ),
            ( [1,6,7,1,8], 7 ),
            ( [], 0 ),
            ( [10], 0 )
        ];

        cases.RunTests(MaxProfit);
    }

    public int MaxProfit(int[] prices)
    {       
        (int low, int hi) = (0, 1);
        int max = 0;

        while(hi < prices.Length)
        {
            var profit = prices[hi] - prices[low];         
            
            if (profit >= 0)
                max = Math.Max(max, profit);                            
            else            
                low = hi;  
            
            hi++;
        }

        return max;       
    }
}
