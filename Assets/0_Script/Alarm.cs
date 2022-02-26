using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.ootii.Messages;
using System;

public class Alarm : MonoBehaviour
{
    [SerializeField, Tooltip("The time in seconds before alarm turns off if the player hasn't been seen")]
    private float _cooldownTime = 2;
    private float _timeOfLastReport;
    private bool _alarmActive = false;

    public bool AlarmActive => _alarmActive;

    void OnEnable()
    {
        MessageDispatcher.AddListener(Msg.ReportSeeingPlayer, OnReportSeeingPlayer);
    }
    void OnDisable()
    {
        MessageDispatcher.RemoveListener(Msg.ReportSeeingPlayer, OnReportSeeingPlayer);
    }

    private void OnReportSeeingPlayer(IMessage rMessage)
    {
        _timeOfLastReport = TimeMgr.UnpausedTime;

        if (!AlarmActive)
        {
            ActivateAlarm();
        }
    }

    private void Update()
    {
        if (AlarmActive && TimeMgr.UnpausedTime > _timeOfLastReport + _cooldownTime)
        {
            DeactivateAlarm();
        }
    }

    private void ActivateAlarm()
    {
        _alarmActive = true;
        Debug.Log("Alarm activated!");
        MessageDispatcher.SendMessage(Msg.AlarmStarted);
    }

    private void DeactivateAlarm()
    {
        _alarmActive = false;
        Debug.Log("Alarm turned off...");
        MessageDispatcher.SendMessage(Msg.AlarmStopped);
    }

}
