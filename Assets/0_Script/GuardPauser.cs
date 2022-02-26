using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.ootii.Messages;
using System;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;

public class GuardPauser : MonoBehaviour
{
    [SerializeField]
    NavMeshAgent _agent;
    [SerializeField]
    BehaviorTree _behaviourTree;

    Vector3 _savedDestination;

    private void OnEnable()
    {
        MessageDispatcher.AddListener(Msg.PauseGame, OnPause);
        MessageDispatcher.AddListener(Msg.ResumeGame, OnResume);
    }

    private void OnDisable()
    {
        MessageDispatcher.RemoveListener(Msg.PauseGame, OnPause);
        MessageDispatcher.RemoveListener(Msg.ResumeGame, OnResume);
    }


    private void OnResume(IMessage rMessage)
    {
        _behaviourTree.EnableBehavior();
        _agent.SetDestination(_savedDestination);
    }

    private void OnPause(IMessage rMessage)
    {
        _behaviourTree.DisableBehavior(pause: true);
        _savedDestination = _agent.destination;
        _agent.SetDestination(_agent.transform.position);
    }
}
