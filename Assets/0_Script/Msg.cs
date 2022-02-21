public static class Msg
{
    private const string _lostGame = "LOST_GAME";
    private const string _wonGame = "WON_GAME";

    public static string LostGame { get => _lostGame; }
    public static string WonGame { get => _wonGame; }
}