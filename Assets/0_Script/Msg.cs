public static class Msg
{
    private const string _lostGame = "LOST_GAME";
    private const string _wonGame = "WON_GAME";
    private const string _nextLevel = "NEXT_LEVEL";
    private const string _restartLevel = "RESTART_LEVEL";
    private const string _reportSeeingPlayer = "REPORT_SEEING_PLAYER";
    private const string _alarmStarted = "ALARM_STARTED";
    private const string _alarmStopped = "ALARM_STOPPED";
    private const string _levelStarted = "LEVEL_STARTED";

    public static string LostGame => _lostGame;
    public static string WonGame => _wonGame;
    public static string NextLevel => _nextLevel;
    public static string RestartLevel => _restartLevel;
    public static string ReportSeeingPlayer => _reportSeeingPlayer;
    public static string AlarmStarted => _alarmStarted;
    public static string AlarmStopped => _alarmStopped;
    public static string LevelStarted => _levelStarted;
}