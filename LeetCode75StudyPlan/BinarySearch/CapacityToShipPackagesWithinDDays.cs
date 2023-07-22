namespace LeetCode75StudyPlan.BinarySearch;

internal class CapacityToShipPackagesWithinDDays : Solution<(int[] weights, int D), int>
{
    protected override string Title => "1011. Capacity To Ship Packages Within D Days";

    protected override IEnumerable<((int[] weights, int D), int)> TestCases
    {
        get
        {
            yield return ((@int[1, 2, 3, 4, 5, 6, 7, 8, 9, 10], 5), 15);
        }
    }

    protected override int Solve((int[] weights, int D) input)
    {
        BinarySearcher bs = new(capacity =>
        {
            (int days, int total) = (1, 0);
            foreach(var weight in input.weights)
            {
                total += weight;
                if(total > capacity)
                {
                    total = weight;                    
                    if(++days > input.D) return false;                    
                }
            }
            return true;
        });

        return bs.SearchIn(input.weights.Max(), input.weights.Sum());
    }
}
