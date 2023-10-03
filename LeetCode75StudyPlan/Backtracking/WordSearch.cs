
using System.Windows.Markup;

namespace LeetCode75StudyPlan.Backtracking;

internal class WordSearch : Solution<(char[][] board, string word), bool>
{
    private record Cell(char? Value, int X, int Y);
    private Func<Cell, IReadOnlyList<Cell>> adjOf;

    public bool Exist(char[][] board, string word)
    {
        adjOf = AdjHelper(board);                
        for(int i = 0; i < board.Length; i++) 
        {
            for (int j = 0; j < board[0].Length; j++)
            {
                Cell c = new(board[i][j], i, j);                
                if (c.Value == word[0])                     
                {
                    HashSet<Cell> visited = new() { c };
                    if (BackTrack(c, 1, word, visited)) return true;
                }
            }
        }
        return false;
    }

    private bool BackTrack(Cell c, int k, string word, ISet<Cell> visited)
    {
        if(k == word.Length)
        {
            return true;
        }
        else 
        {
            foreach(Cell cell in adjOf(c))
            {
                if (cell.Value == word[k] && visited.Add(cell))
                {
                    if (BackTrack(cell, k + 1, word, visited)) return true;
                    visited.Remove(cell);                    
                }
            }
            return false;
        }        
    }

    private Func<Cell, IReadOnlyList<Cell>> AdjHelper(char[][] board)
    {
        int N = board.Length;
        int M = board[0].Length;
        return (Cell c) =>
        {
            List<Cell> adj = new();
            
            (int x, int y) = (c.X - 1, c.Y);
            if (x >= 0) 
                adj.Add(new(board[x][y], x, y));

            (x, y) = (c.X, c.Y - 1);
            if (y >= 0) 
                adj.Add(new(board[x][y], x, y));

            (x, y) = (c.X + 1, c.Y);
            if (x < N) 
                adj.Add(new(board[x][y], x, y));

            (x, y) = (c.X, c.Y + 1);
            if (y < M) 
                adj.Add(new(board[x][y], x, y));

            return adj;
        };
    }

    protected override string Title => "79. Word Search";

    protected override IEnumerable<((char[][] board, string word), bool)> TestCases
    {
        get
        {  
            var board3 = Arr(
                @char['A', 'B', 'C', 'E'],
                @char['S', 'F', 'E', 'S'],
                @char['A', 'D', 'E', 'E']);

            yield return ((board3, "ABCESEEEFS"), true);

            //yield break;

            var board1 = Arr(
                @char['A', 'B', 'C', 'E'],
                @char['S', 'F', 'C', 'S'],
                @char['A', 'D', 'E', 'E']);
           
            yield return ((board1, "ABCCED"), true);           
            yield return ((board1, "SEE"), true);
            yield return ((board1, "ABCB"), false);

            var board2 = Arr(
                @char['a', 'b'], 
                @char['c', 'd']);

            yield return ((board2, "cdba"), true);



        }
    }

    protected override bool Solve((char[][] board, string word) input) => Exist(input.board, input.word);
    
}
