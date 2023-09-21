
namespace LeetCode75StudyPlan.Backtracking;

abstract class Backtracker<T>
{
    protected readonly T input;
    public bool Finished { get; protected set; }

    protected Backtracker(T input) => this.input = input;

    protected void Backtrack(int[] a, int k)
    {
        if(IsASolution(a, k))
        {
            ProcessSolution(a, k);
        }
        else
        {            
            var c = ConstructCondidates(a, k);
            for(int i = 0; i < c.Count; i++)
            {
                a[k] = c[i];
                MakeMove(a, k);
                Backtrack(a, k + 1);
                UnmakeMove(a, k);
                if (Finished) return;
            }
        }
    }

    protected abstract bool IsASolution(int[] a, int k);
    protected abstract void ProcessSolution(int[] a, int k);
    protected abstract IList<int> ConstructCondidates(int[] a, int k);

    protected virtual void MakeMove(int[] a, int k) { }
    protected virtual void UnmakeMove(int[] a, int k) { }

}
