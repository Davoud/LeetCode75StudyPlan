namespace LeetCode75StudyPlan.Graphs.Imp;
public interface IGraph<T>
{
    public int VertexCount { get; }
    public IEnumerable<T> this[T index] { get; }

    public GraphType Type { get; }
}

public enum GraphType
{
    Directed,
    Undirected
}