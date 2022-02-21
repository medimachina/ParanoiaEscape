public static class Msg
{
    private const string _lostGame = "LOST_GAME";
    private const string _wonGame = "WON_GAME";
    private const string _nextLevel = "NEXT_LEVEL";
    private const string _restartLevel = "RESTART_LEVEL";

    public static string LostGame { get => _lostGame; }
    public static string WonGame { get => _wonGame; }
    public static string NextLevel { get => _nextLevel; }
    public static string RestartLevel { get => _restartLevel; }
}