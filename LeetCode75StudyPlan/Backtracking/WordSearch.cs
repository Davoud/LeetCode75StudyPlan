
using System.Windows.Markup;

namespace LeetCode75StudyPlan.Backtracking;

internal class WordSearch : Solution<(char[][] board, string word), bool>
{        
    private char[][] board;
    private int N;
    private int M;
    private string word;
    public bool Exist(char[][] board, string word)
    {        
        this.board = board;
        N = board.Length;
        M = board[0].Length;
        this.word = word;
        char w = word[0];

        for(int i = 0; i < N; i++) 
        {
            for (int j = 0; j < M; j++)
            {                
                if (board[i][j] == w)                     
                {                    
                    var visited = new bool[N, M];
                    visited[i,j]= true;
                    if (BackTrack(i, j, 1, visited)) return true;
                }
            }
        }
        return false;
    }

    private bool BackTrack(int i, int j, int k, bool[,] visited)
    {
        if(k == word.Length)
        {
            return true;
        }
        else 
        {            
            char w = word[k];
            
            (int x, int y) = (i - 1, j);
            if (x >= 0 && w == board[x][y] && !visited[x,y]) 
            {
                visited[x,y] = true;
                if (BackTrack(x, y, k + 1, visited)) return true;
                visited[x,y] = false;
            }
                
            (x, y) = (i, j - 1);
            if (y >= 0 && w == board[x][y] && !visited[x, y]) 
            {
                visited[x, y] = true;
                if (BackTrack(x, y, k + 1, visited)) return true;
                visited[x, y] = false;
            }
                
            (x, y) = (i + 1, j);
            if (x < N && w == board[x][y] && !visited[x, y])
            {
                visited[x, y] = true;
                if (BackTrack(x, y, k + 1, visited)) return true;
                visited[x, y] = false;
            }

            (x, y) = (i, j + 1);
            if (y < M && w == board[x][y] && !visited[x, y])
            {
                visited[x, y] = true;
                if (BackTrack(x, y, k + 1, visited)) return true;
                visited[x, y] = false;
            }
            
            return false;
        }        
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
