namespace FastType.Services;

public static class ScoreService
{
    private static int GapPenalty { get; set; } = 3;
    private static int MMPenalty { get; set; } = 1;
    public static double Match(string FWord, string SWord)
    {
        return 0.0;
    }
}
