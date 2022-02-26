public static class Msg
{
    private const string _lostGame = "LOST_GAME";
    private const string _wonGame = "WON_GAME";
    private const string _finishedGame = "FINISHED_GAME";
    private const string _nextLevel = "NEXT_LEVEL";
    private const string _restartLevel = "RESTART_LEVEL";
    private const string _reportSeeingPlayer = "REPORT_SEEING_PLAYER";
    private const string _alarmStarted = "ALARM_STARTED";
    private const string _alarmStopped = "ALARM_STOPPED";
    private const string _levelStarted = "LEVEL_STARTED";
    private const string _startMenuMusic = "START_MENU_MUSIC";
    private const string _startTutorial = "START_TUTORIAL";
    private const string _restartGame = "RESTART_GAME";
    private const string _showFinishedLevelMenu = "SHOW_FINISHED_LEVEL_MENU";
    private const string _showFinishedGameMenu = "SHOW_FINISHED_GAME_MENU";


    public static string LostGame => _lostGame;
    public static string WonGame => _wonGame;
    public static string NextLevel => _nextLevel;
    public static string RestartLevel => _restartLevel;
    public static string ReportSeeingPlayer => _reportSeeingPlayer;
    public static string AlarmStarted => _alarmStarted;
    public static string AlarmStopped => _alarmStopped;
    public static string LevelStarted => _levelStarted;
    public static string StartMenuMusic => _startMenuMusic;
    public static string StartTutorial => _startTutorial;
    public static string RestartGame => _restartGame;
    public static string FinishedGame => _finishedGame;
    public static string ShowFinishedLevelMenu => _showFinishedLevelMenu;
    public static string ShowFinishedGameMenu => _showFinishedGameMenu;
}

