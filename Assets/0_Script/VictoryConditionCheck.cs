using com.ootii.Messages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class VictoryConditionCheck : MonoBehaviour
{
    [SerializeField]
    private GameObject _bars;
    private Transform _barsTransform;

    public StringEvent Exited;
    public StringEvent FailedToExit;

    private bool _open = true;

    private void Start()
    {
        _barsTransform = _bars.transform;
    }

    private void OnEnable()
    {
        MessageDispatcher.AddListener(Msg.AlarmStarted, OnAlarmStarted);
        MessageDispatcher.AddListener(Msg.AlarmStopped, OnAlarmStopped);
    }

    private void OnDisable()
    {
        MessageDispatcher.RemoveListener(Msg.AlarmStarted, OnAlarmStarted);
        MessageDispatcher.RemoveListener(Msg.AlarmStopped, OnAlarmStopped);
    }

    private void OnAlarmStarted(IMessage rMessage)
    {
        _open = false;
        _barsTransform.DOScaleY(1, 0.5f);
    }

    private void OnAlarmStopped(IMessage rMessage)
    {
        _barsTransform.DOScaleY(0, 0.5f);
        _open = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (_open)
            {
                Exited.Invoke("");
                MessageDispatcher.SendMessage(Msg.WonGame);
            }
            else
            {
                FailedToExit.Invoke("");
            }
        }
    }
}
