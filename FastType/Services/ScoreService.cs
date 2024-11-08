namespace FastType.Services;

public static class ScoreService
{
    private static int GapPenalty { get; set; } = 3;
    private static int MMPenalty { get; set; } = 1;
    public static double Match(string SWord, string WWord)
    {
        List<List<int>> Memo = [];
        for (int i = 0; i < SWord.Length; i++)
            Memo[0][i] = GapPenalty * i;

        for (int i = 0; i < WWord.Length; i++)
            Memo[i][0] = GapPenalty * i;

        for (int i = 0; i < SWord.Length; i++)
        {
            for (int j = 0; j < WWord.Length; j++)
            {
                int penalty = 0;
                if (SWord[i] != WWord[j])
                    penalty = MMPenalty;
                Memo[i][j] = Math.Min(penalty + Memo[i - 1][j - 1],
                             Math.Min(GapPenalty + Memo[i - 1][j],
                                      GapPenalty + Memo[i][j - 1]));
            }
        }
        return Memo[SWord.Length][WWord.Length];
    }
}
