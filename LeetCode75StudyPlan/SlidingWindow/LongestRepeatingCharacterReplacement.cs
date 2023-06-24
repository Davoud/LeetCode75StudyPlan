
namespace LeetCode75StudyPlan.SlidingWindow;

internal class LongestRepeatingCharacterReplacement: ITestable
{
    void ITestable.RunTests()
    {
        Console.WriteLine("424. Longest Repeating Character Replacement");

        var cases = new[]
        {
            (("ABAB",       2), 4),
            (("AABABBA",    1), 4)
        };

        cases.RunTests(input => CharacterReplacement(input.Item1, input.Item2));
    }
    public int CharacterReplacement(string s, int k)
    {
        int left = 0, maxFrequentLetter = 0, maxLength = 0;
        Span<int> counter = stackalloc int[26];
        for (int right = 0; right < s.Length; right++)
        {
            maxFrequentLetter = Math.Max(maxFrequentLetter, ++counter[s[right] - 'A']);
            int lettersToChange = (right - left + 1) - maxFrequentLetter;
            if (lettersToChange > k)
            {
                counter[s[left] - 'A']--;
                left++;
            }
            maxLength = Math.Max(maxLength, right - left + 1);
        }
        return maxLength;
    }
}
