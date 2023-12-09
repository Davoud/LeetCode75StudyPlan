
namespace LeetCode75StudyPlan.Graphs;

internal class SurroundedRegions : Solution<char[][], char[][]>
{
    public void Capture(char[][] board)
    {
        int ROWS = board.Length;
        int COLS = board[0].Length;
        const char CAPITALO = 'O'; 
        const char CAPTURED = 'X';
        const char FLIPPED = '-';

        for(int row = 0; row < ROWS; row++)
        {
            if (board[row][0] == CAPITALO) Dfs(row, 0);
            if (board[row][COLS - 1] == CAPITALO) Dfs(row, COLS - 1);           
        }        

        for (int col = 1; col < COLS - 1; col++)
        {
            if (board[0][col] == CAPITALO) Dfs(0, col);
            if (board[ROWS - 1][col] == CAPITALO) Dfs(ROWS - 1, col);
        }
        
        for (int i = 0; i < ROWS; i++)
        {
            for (int j = 0; j < COLS; j++)
            {
                switch (board[i][j])
                {
                    case FLIPPED: board[i][j] = CAPITALO; break;
                    case CAPITALO: board[i][j] = CAPTURED; break;
                }
            }
        }
        
        void Dfs(int i, int j)
        {
            if (0 <= i && i < ROWS && 0 <= j && j < COLS && board[i][j] == CAPITALO)
            {
                board[i][j] = FLIPPED;
                Dfs(i - 1, j);
                Dfs(i + 1, j);
                Dfs(i, j - 1);
                Dfs(i, j + 1);
            }
        }
    }
    
    protected override string Title => "130. Surrounded Regions";

    protected override IEnumerable<(char[][], char[][])> TestCases
    {
        get
        {
            yield return (Char2D("OOO", "OOO", "OOO"), Char2D("OOO", "OOO", "OOO"));
            yield return (Char2D("XXXX", "XOOX", "XXOX", "XOXX"), Char2D("XXXX", "XXXX", "XXXX", "XOXX"));
            yield return (Char2D("X"), Char2D("X"));
        }
    }

    protected override bool IsEqual(char[][] actual, char[][] expected)
    {
        if (actual.Length != expected.Length) return false;

        for (int i = 0; i < actual.Length; i++)
        {
            if (expected.Length < i - 1 || actual[i].Length != expected[i].Length) return false;
            for (int j = 0; j < actual[i].Length; j++)
            {
                if (actual[i][j] != expected[i][j]) return false;
            }
        }
        return true;
    }

    protected override char[][] Solve(char[][] input)
    {
        Capture(input);
        return input;
    }

}

