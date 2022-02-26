using UnityEngine;
using System;
using com.ootii.Messages;


public static class TimeMgr
{
    private static bool _paused = false;

    public static bool Paused { get => _paused; }

    public static Action<bool> PausedAction;

    private static float _pausedTime = 0;

    private static float _latestPauseStart = 0;
    private static float _latestPauseStartInUnpausedTime = 0;

    public static float UnpausedTime
    {
        get
        {
            if (Paused)
            {
                return _latestPauseStartInUnpausedTime;
            }
            else
            {
                return Time.time - _pausedTime;
            }
        }
    }

    public static void Pause()
    {
        if (!Paused)
        {
            _latestPauseStartInUnpausedTime = UnpausedTime;
            _paused = true;
            _latestPauseStart = Time.time;
            if (PausedAction != null)
            {
                PausedAction.Invoke(_paused);
            }
            MessageDispatcher.SendMessage(Msg.PauseGame);
        }
    }

    public static void Resume()
    {
        if (Paused)
        {
            _paused = false;
            _pausedTime += Time.time - _latestPauseStart;
            if (PausedAction != null)
            {
                PausedAction.Invoke(_paused);
            }
            MessageDispatcher.SendMessage(Msg.ResumeGame);
        }
    }

    public static void Toggle()
    {
        if (Paused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

}
