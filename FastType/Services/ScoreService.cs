namespace FastType.Services;

public static class ScoreService
{
    private static int GapPenalty { get; set; } = 3;
    private static int MMPenalty { get; set; } = 1;
    public static double Match(string SWord, string WWord)
    {
        double max = Math.Max(SWord.Length * GapPenalty, WWord.Length * GapPenalty);


        int[][] Memo = new int[SWord.Length][];
        for (int i = 0; i < SWord.Length; i++)
            Memo[i] = new int[WWord.Length];

        for (int i = 0; i < WWord.Length; i++)
            Memo[0][i] = GapPenalty * i;

        for (int i = 0; i < SWord.Length; i++)
            Memo[i][0] = GapPenalty * i;

        for (int i = 1; i < SWord.Length; i++)
        {
            for (int j = 1; j < WWord.Length; j++)
            {
                int penalty = 0;
                if (SWord[i] != WWord[j])
                    penalty = MMPenalty;
                Memo[i][j] = Math.Min(penalty + Memo[i - 1][j - 1],
                             Math.Min(GapPenalty + Memo[i - 1][j],
                                      GapPenalty + Memo[i][j - 1]));
                max = Math.Max(max, Memo[i][j]);
            }
        }
        var limiter = (SWord.Length * Math.Min(GapPenalty, MMPenalty)) / 2;
        return limiter - Memo[SWord.Length - 1][WWord.Length - 1];
    }
}
