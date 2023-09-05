namespace LeetCode75StudyPlan.Tries;

public interface ITrie
{
    void Insert(string word);
    bool Search(string word);
    bool StartsWith(string prefix);
}
