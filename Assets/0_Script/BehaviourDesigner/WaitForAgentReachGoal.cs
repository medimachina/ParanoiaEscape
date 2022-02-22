using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Tooltip = BehaviorDesigner.Runtime.Tasks.TooltipAttribute;
using UnityEngine.AI;

[TaskCategory("Custom")]
public class WaitForNavmeshReachGoal : Action
{
    [Tooltip("The goal position the navmesh should reach")]
    public SharedVector3 GoalPosition;
    [Tooltip("The navmesh agent to wait for")]
    public Transform NavmeshAgentTransform;
    [Tooltip("How close does the agent have to get to count as having reached the goal")]
    public float DistanceTolerance = 0.1f;
    [Tooltip("If the agent doesn't move, how long until it gives up and fails. (-1 = never give up)")]
    public float MaxTimeNotMoving = 3;

    private NavMeshAgent _agent;
    public float _waitTimeSinceStopped = 0;
    private bool _triedRestarting;

    public override void OnAwake()
    {
        _agent = NavmeshAgentTransform.GetComponentInChildren<NavMeshAgent>();
        ResetVariables();
    }

    public override void OnStart()
    {
        _triedRestarting = false;
        _waitTimeSinceStopped = 0;
    }

    public override TaskStatus OnUpdate()
    {
        if (NavmeshAgentTransform == null || GoalPosition == null)
        {
            return TaskStatus.Failure;
        }

        float distance = Vector3.Distance(NavmeshAgentTransform.position, GoalPosition.Value);

        if (_agent.velocity.magnitude < 0.01f)
        {
            _waitTimeSinceStopped += Time.deltaTime;
        }
        else
        {
            _waitTimeSinceStopped = 0;
        }

        if (!_triedRestarting && _waitTimeSinceStopped > MaxTimeNotMoving / 2)
        {
            Debug.Log($"Halted... Resetting destination to {GoalPosition.Value}");
            _agent.SetDestination(GoalPosition.Value);
            _triedRestarting = true;
        }
        else if (_waitTimeSinceStopped > MaxTimeNotMoving)
        {
            Debug.Log($"Waited maxtime. Returning Success to continue.");
            return TaskStatus.Success;
        }

        if (distance < DistanceTolerance)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Running;
        }
    }

    public override void OnReset()
    {
        ResetVariables();
    }

    private void ResetVariables()
    {
        _waitTimeSinceStopped = 0;
    }

}
