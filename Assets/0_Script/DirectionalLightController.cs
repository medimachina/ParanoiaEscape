using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using com.ootii.Messages;
using System;

public class DirectionalLightController : MonoBehaviour
{
    [SerializeField]
    private Light _lightRef;
    [SerializeField]
    private Color _neutralColor, AlarmColor;
    [SerializeField]
    private float _lowIntensityOnAlarm;

    private float _standardIntensity;

    Tween _intensityLooper;

    public void OnEnable()
    {
        MessageDispatcher.AddListener(Msg.AlarmStarted, OnAlarmStart);
        MessageDispatcher.AddListener(Msg.AlarmStopped, OnAlarmStop);
    }

 
    public void OnDisable()
    {
        MessageDispatcher.RemoveListener(Msg.AlarmStarted, OnAlarmStart);
        MessageDispatcher.RemoveListener(Msg.AlarmStopped, OnAlarmStop);
    }


    [Button("StartAlarm")]
    public void OnAlarmStart(IMessage rMessage)
    {
        _standardIntensity = _lightRef.intensity;
        _lightRef.DOColor(AlarmColor, 1);
        _intensityLooper = _lightRef.DOIntensity(_lowIntensityOnAlarm, 0.5f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    [Button("StopAlarm")]
    public void OnAlarmStop(IMessage rMessage)
    {
        _lightRef.DOColor(_neutralColor, 1);
        _intensityLooper.Kill();
        _lightRef.DOIntensity(_standardIntensity, 1);
    }
}
