namespace LeetCode75StudyPlan.Backtracking;
using Strings = IList<string>;
class NQueens2 : Solution<int, IList<Strings>>
{
    private const string DOTS = ".............";
    private const int PUT = 1;
    private const int REMOVE = -1;
    readonly List<Strings> res = new();
    private int N;
    private int[,] board;
    public IList<Strings> SolveNQueens(int n)
    {
        N = n;
        board = new int[n, n];
        res.Clear();
        BackTrack(0, new int[n]);
        return res;
    }

    private void BackTrack(int k, int[] a)
    {
        if (k >= N)
        {
            var brd = new List<string>();
            Span<char> row = DOTS[..N].ToCharArray();
            for (int i = 0; i < N; i++)
            {
                row[a[i]] = 'Q';
                brd.Add(new(row));
                row[a[i]] = '.';
            }
            res.Add(brd);
        }
        else
        {
            for (int n = 0; n < N; n++)
            {
                if (board[k, n] == 0)
                {
                    a[k] = n;
                    QueenAt(k, n, PUT);
                    BackTrack(k + 1, a);
                    QueenAt(k, n, REMOVE);
                }
            }
        }
    }

    public void QueenAt(int x, int y, int value)
    {

        for (int i = x + 1, j = y + 1; i < N && j < N; i++, j++)
            board[i, j] += value;

        for (int i = x - 1, j = y - 1; i >= 0 && j >= 0; i--, j--)
            board[i, j] += value;

        for (int i = x + 1, j = y - 1; i < N && j >= 0; i++, j--)
            board[i, j] += value;

        for (int i = x - 1, j = y + 1; i >= 0 && j < N; i--, j++)
            board[i, j] += value;

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                if (i == x || j == y)
                    board[i, j] += value;
            }
        }
    }

    protected override string Title => "51.N-Queens";

    protected override IEnumerable<(int, IList<Strings>)> TestCases
    {
        get
        {
            yield return (1, [["Q"]]);
            IList<Strings> empty = [[]];
            yield return (2, empty);
            yield return (3, empty);

            
            Strings q1 = [".Q..", "...Q", "Q...", "..Q."];
            Strings q2 = ["..Q.", "Q...", "...Q", ".Q.."];

            yield return (4, [q1, q2]);


            IList<Strings> q5 = [
                ["Q....", "..Q..", "....Q", ".Q...", "...Q."],
                ["Q....", "...Q.", ".Q...", "....Q", "..Q.."],
                [".Q...", "...Q.", "Q....", "..Q..", "....Q"],
                [".Q...", "....Q", "..Q..", "Q....", "...Q."],
                ["..Q..", "Q....", "...Q.", ".Q...", "....Q"],
                ["..Q..", "....Q", ".Q...", "...Q.", "Q...."],
                ["...Q.", "Q....", "..Q..", "....Q", ".Q..."],
                ["...Q.", ".Q...", "....Q", "..Q..", "Q...."],
                ["....Q", ".Q...", "...Q.", "Q....", "..Q.."],
                ["....Q", "..Q..", "Q....", "...Q.", ".Q..."]];

            yield return (5, q5);
        }
    }

    protected override IList<IList<string>> Solve(int input)
    {       
        return SolveNQueens(input);
    }

    protected override bool IsEqual(IList<Strings> actual, IList<Strings> expected)
    {
        if (actual.Count == expected.Count)
        {
            var set = expected.Select(i => i.ToHashSet()).ToHashSet();
            foreach (var item in actual)
            {
                if (!set.Any(i => i.SetEquals(item)))
                {
                    return false;
                }
            }
            return true;
        }
        else
        {
            return false;
        }
    }
}

internal class NQueens : Solution<int, IList<Strings>>
{
    private const string DOTS = ".............";    
    readonly List<Strings> res = new();

    public IList<Strings> SolveNQueens(int n)
    {
        res.Clear();
        BackTrack(0, new Chessboard(n), new int[n]);       
        return res;
    }
    private void BackTrack(int k, Chessboard cb, int[] a)
    {
        if (k >= cb.N)
        {
            var brd = new List<string>();
            Span<char> row = DOTS[..cb.N].ToCharArray();
            for (int i = 0; i < cb.N; i++)
            {
                row[a[i]] = 'Q';
                brd.Add(new(row));
                row[a[i]] = '.';
            }
            res.Add(brd);
        }
        else
        {
            for (int n = 0; n < cb.N; n++)
            {
                if (!cb.board[k, n])
                {
                    a[k] = n;
                    BackTrack(k + 1, cb.WithQueenAt(k, n), a);
                }
            }
        }
    }

    

   

    private class Chessboard
    {
        public readonly int N;
        public readonly bool[,] board;

        public Chessboard(int n)
        {
            N = n;
            board = new bool[n, n];
        }

        public Chessboard WithQueenAt(int x, int y)
        {
            var cb = new Chessboard(N);

            for (int i = x + 1, j = y + 1; i < N && j < N; i++, j++)
                cb.board[i, j] = true;

            for (int i = x - 1, j = y - 1; i >= 0 && j >= 0; i--, j--)
                cb.board[i, j] = true;

            for (int i = x + 1, j = y - 1; i < N && j >= 0; i++, j--)
                cb.board[i, j] = true;

            for (int i = x - 1, j = y + 1; i >= 0 && j < N; i--, j++)
                cb.board[i, j] = true;

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (!cb.board[i, j])
                        cb.board[i, j] = board[i, j] || i == x || j == y;
                }
            }
            return cb;
        }



        public override string ToString()
        {
            StringBuilder sb = new("\n");
            for (int i = 0; i < N; i++)
            {
                sb.Append(i).Append(' ');
                for (int j = 0; j < N; j++)
                {
                    sb.Append(board[i, j] ? "■ " : "- ");
                }
                sb.Append('\n');
            }
            sb.Append("  ");
            for (int j = 0; j < N; j++)
            {
                sb.Append(j).Append(' ');
            }
            return sb.ToString();
        }
    }

    protected override string Title => "51.N-Queens";

    protected override IEnumerable<(int, IList<IList<string>>)> TestCases
    {
        get
        {
            yield return (1, [["Q"]]);
            IList<Strings> empty = [[]];
            yield return (2, empty);
            yield return (3, empty);


            Strings q1 = [".Q..", "...Q", "Q...", "..Q."];
            Strings q2 = ["..Q.", "Q...", "...Q", ".Q.."];

            yield return (4, [q1, q2]);


            IList<Strings> q5 = [
                ["Q....", "..Q..", "....Q", ".Q...", "...Q."],
                ["Q....", "...Q.", ".Q...", "....Q", "..Q.."],
                [".Q...", "...Q.", "Q....", "..Q..", "....Q"],
                [".Q...", "....Q", "..Q..", "Q....", "...Q."],
                ["..Q..", "Q....", "...Q.", ".Q...", "....Q"],
                ["..Q..", "....Q", ".Q...", "...Q.", "Q...."],
                ["...Q.", "Q....", "..Q..", "....Q", ".Q..."],
                ["...Q.", ".Q...", "....Q", "..Q..", "Q...."],
                ["....Q", ".Q...", "...Q.", "Q....", "..Q.."],
                ["....Q", "..Q..", "Q....", "...Q.", ".Q..."]];

            yield return (5, q5);
        }
    }

    protected override IList<IList<string>> Solve(int input)
    {      
        return SolveNQueens(input);
    }

    protected override bool IsEqual(IList<IList<string>> actual, IList<IList<string>> expected)
    {
        if (actual.Count == expected.Count)
        {
            var set = expected.Select(i => i.ToHashSet()).ToHashSet();
            foreach (var item in actual)
            {
                if (!set.Any(i => i.SetEquals(item)))
                {
                    return false;
                }
            }
            return true;
        }
        else
        {
            return false;
        }
    }

}
