
using System.Collections;
using System.Windows.Markup;

namespace LeetCode75StudyPlan.Backtracking;

internal class WordSearch : Solution<(char[][] board, string word), bool>
{        
    private char[][] board;
    private int N;
    private int M;
    private string word;
    private BitArray path;
    public bool Exist(char[][] board, string word)
    {        
        this.board = board;
        N = board.Length;
        M = board[0].Length;
        this.word = word;
        char w = word[0];
        this.path = new BitArray(N * M);

        for (int i = 0; i < N; i++) 
        {
            for (int j = 0; j < M; j++)
            {                
                if (board[i][j] == w)                     
                {
                    path.SetAll(false);
                    path.Set((i * M) + j, true);
                    if (BackTrack(i, j, 1)) return true;
                }
            }
        }
        return false;
    }

    private bool BackTrack(int i, int j, int k)
    {
        if(k == word.Length)
        {
            return true;
        }
        else 
        {            
            char w = word[k++];
            int z;
            
            int x = i - 1;
            if (x >= 0 && w == board[x][j] && !path.Get(z = (x * M) + j)) 
            {                
                path.Set(z, true);
                if (BackTrack(x, j, k)) return true;
                path.Set(z, false);
            }
                
            int y = j - 1;            
            if (y >= 0 && w == board[i][y] && !path.Get(z = (i * M) + y)) 
            {
                path.Set(z, true);
                if (BackTrack(i, y, k)) return true;
                path.Set(z, false);
            }
                
            x = i + 1;            
            if (x < N && w == board[x][j] && !path.Get(z = (x * M) + j))
            {
                path.Set(z, true);
                if (BackTrack(x, j, k)) return true;
                path.Set(z, false);
            }

            y = j + 1;            
            if (y < M && w == board[i][y] && !path.Get(z = (i * M) + y))
            {
                path.Set(z, true);
                if (BackTrack(i, y, k)) return true;
                path.Set(z, false);
            }
            
            return false;
        }        
    }
   
    protected override string Title => "79. Word Search";

    protected override IEnumerable<((char[][] board, string word), bool)> TestCases
    {
        get
        {
            char[][] board3 = [
                ['A', 'B', 'C', 'E'],
                ['S', 'F', 'E', 'S'],
                ['A', 'D', 'E', 'E']];

            yield return ((board3, "ABCESEEEFS"), true);

            //yield break;

            char[][] board1 = [
                ['A', 'B', 'C', 'E'],
                ['S', 'F', 'C', 'S'],
                ['A', 'D', 'E', 'E']];
           
            yield return ((board1, "ABCCED"), true);           
            yield return ((board1, "SEE"), true);
            yield return ((board1, "ABCB"), false);

            char[][] board2 = [
                ['a', 'b'], 
                ['c', 'd']];

            yield return ((board2, "cdba"), true);



        }
    }

    protected override bool Solve((char[][] board, string word) input) => Exist(input.board, input.word);
    
}
