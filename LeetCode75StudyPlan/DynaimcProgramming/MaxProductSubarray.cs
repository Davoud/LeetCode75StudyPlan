
namespace LeetCode75StudyPlan.DynaimcProgramming;

internal class MaxProductSubarray : Solution<int[], int>
{

    public int MaxProduct(int[] nums)
    {
        
        int l = 1, r = 1;
        int max = nums[0];

        for (int i = 0; i < nums.Length; i++)
        {
            l = l == 0 ? 1 : l;
            r = r == 0 ? 1 : r;

            l *= nums[i];
            r *= nums[^(i+1)];          

            max = Math.Max(max, Math.Max(l, r));

        }

        return max; 
    }

    public int MaxProduct1(int[] nums) // correct but slow
    {
        int[] products = [.. nums];
        int max = products.Max();

        for(int i = 1; i < nums.Length; i++)        
        {
            products.Dump();
            int m = int.MinValue;
            for(int j = 0; j < nums.Length - i; j++)
            {
                products[j] *= nums[i + j];
                m = Math.Max(m, products[j]);
            }            
            max = Math.Max(max, m);
        }        

        return max;
    }

    protected override string Title => "152. Maximum Product Subarray";

    protected override IEnumerable<(int[], int)> TestCases
    {
        get
        {
            yield return ([2, -5, -2, -4, 3], 24);
            yield return ([2, 3, -2, 4], 6);
            yield return ([-2, 0, -1], 0);
        }
    }

    

    protected override int Solve(int[] input) => MaxProduct(input);
    
}
