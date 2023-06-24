namespace LeetCode75StudyPlan.ArraysAndHashing;

public class SudokuValidator
{
    public static class Tests
    {
        public static void Run()
        {
            char[][] intpu = new[] {
                new[] { '5', '3', '.', '.', '7', '.', '.', '.', '.' },
                new[] { '6', '.', '.', '1', '9', '5', '.', '.', '.' },
                new[] { '.', '9', '8', '.', '.', '.', '.', '6', '.' },
                new[] { '8', '.', '.', '.', '6', '.', '.', '.', '3' },
                new[] { '4', '.', '.', '8', '.', '3', '.', '.', '1' },
                new[] { '7', '.', '.', '.', '2', '.', '.', '.', '6' },
                new[] { '.', '6', '.', '.', '.', '.', '2', '8', '.' },
                new[] { '.', '.', '.', '4', '1', '9', '.', '.', '5' },
                new[] { '.', '.', '.', '.', '8', '.', '5', '7', '9' }
            };
            var solution = new SudokuValidator();
            Console.WriteLine(solution.IsValidSudoku(intpu));
        }
    }

    public struct BitwiseSet
    {
        private int _value;
        // public bool Contains(int element) => (_value & (0b1 << element)) > 0;
        // public void Add(int element) => _value |= (0b1 << element);

        public override string ToString() => string.Format(Convert.ToString(_value, 2));

        public void Clear() => _value = 0;
        public bool TryAdd(int value)
        {
            if ((_value & (0b1 << value)) > 0) return false;
            _value |= 1 << value;
            return true;
        }
    }

    public bool IsValidSudoku2(char[][] board)
    {
        for (int i = 0; i < 9; i++)
        {
            int value, row = 0, col = 0;
            for (int j = 0; j < 9; j++)
            {
                value = board[i][j] - '0';
                if (value != -2)
                {
                    if ((row & (0b1 << value)) > 0) return false;
                    row |= 1 << value;
                }

                value = board[j][i] - '0';
                if (value != -2)
                {
                    if ((col & (0b1 << value)) > 0) return false;
                    col |= 1 << value;
                }
            }
        }

        var xy = new[]
        {
            (0, 0), (0, 3), (0, 6),(3, 0), (3, 3), (3, 6),(6, 0), (6, 3), (6, 6),
        };

        foreach (var (x, y) in xy)
        {
            int box = 0;
            for (int i = x; i < x + 3; i++)
            {
                for (int j = y; j < y + 3; j++)
                {
                    int value = board[i][j] - '0';
                    if (value == -2) continue;
                    if ((box & (0b1 << value)) > 0) return false;
                    box |= 1 << value;
                }
            }
        }

        return true;
    }

    public bool IsValidSudoku(char[][] board)
    {
        var boxes = new BitwiseSet[3, 3];
        for (int i = 0; i < 9; i++)
        {
            int value;
            (BitwiseSet row, BitwiseSet col) = (new(), new());
            for (int j = 0; j < 9; j++)
            {
                value = board[i][j] - '0';
                if (value != -2)
                {
                    if (!row.TryAdd(value)) return false;
                    if (!boxes[i / 3, j / 3].TryAdd(value)) return false;
                }
                value = board[j][i] - '0';
                if (value != -2 && !col.TryAdd(value)) return false;
            }
        }
        return true;
    }


    public bool IsValidSudoku1(char[][] board)
    {


        for (int i = 0; i < 9; i++)
        {
            int value;
            BitwiseSet row = new();
            BitwiseSet col = new();
            for (int j = 0; j < 9; j++)
            {
                value = board[i][j] - '0';
                if (value != -2 && !row.TryAdd(value)) return false;

                value = board[j][i] - '0';
                if (value != -2 && !col.TryAdd(value)) return false;
            }
        }


        var xy = new[]
        {
            (0, 0), (0, 3), (0, 6),(3, 0), (3, 3), (3, 6),(6, 0), (6, 3), (6, 6),
        };

        foreach (var (x, y) in xy)
        {
            BitwiseSet set = new();
            for (int i = x; i < x + 3; i++)
            {
                for (int j = y; j < y + 3; j++)
                {
                    int value = board[i][j] - '0';
                    if (value == -2) continue;
                    if (!set.TryAdd(value)) return false;
                }
            }
        }

        return true;
    }
}
