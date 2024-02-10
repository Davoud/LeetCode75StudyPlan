namespace LeetCode75StudyPlan.Graphs.Imp;
public interface IGraph<T> : IEnumerable<T>
{
    public int VertexCount { get; }
    public IEnumerable<T> this[T index] { get; }

    public GraphType Type { get; }

    public IGraph<T> Reverse();
}

public enum GraphType
{
    Directed,
    Undirected
}

public enum EdgeType
{
    Tree,
    Forward,
    Backward,
    Cross
}