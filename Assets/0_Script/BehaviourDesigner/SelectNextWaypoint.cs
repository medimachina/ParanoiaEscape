using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Tooltip = BehaviorDesigner.Runtime.Tasks.TooltipAttribute;

[TaskCategory("Custom")]
public class SelectNextWaypoint : Action
{
    [Tooltip("The parent object containing all the waypoints")]
    public Transform WaypointParent;
    [Tooltip("This is set by the task, iterating through the waypoints in order and should be shared by all SelectNextWaypoint actions")]
    public SharedInt CurrentWaypointIndex;
    [Tooltip("Position from current waypoint. Updates when current waypoint is changed")]
    public SharedVector3 CurrentWaypointPosition;

    private Transform _currentWaypoint;

    private List<Transform> _waypointTransforms;
    //private int _currentWaypointIndex = 0;

    public override void OnAwake()
    {
        //Debug.Log($"Running OnAwake(). CurrentWaypointIndex => {CurrentWaypointIndex.Value}");
        CurrentWaypointIndex.Value = 0;
        FillWaypointList();
    }

    public override TaskStatus OnUpdate()
    {
        //Debug.Log($"Running SelectNextWaypoint. At start index is {CurrentWaypointIndex.Value}");
        if (_waypointTransforms == null || _waypointTransforms.Count == 0)
        {
            //Debug.LogWarning($"SelectNextWaypoint: _currentWaypoint is null or count 0!");
            return TaskStatus.Failure;
        }

        _currentWaypoint = _waypointTransforms[CurrentWaypointIndex.Value];

        //Debug.Log($"Current waypoint => {_currentWaypoint.name}");

        if (_currentWaypoint != null)
        {
            CurrentWaypointPosition.Value = _currentWaypoint.position;
            //Debug.Log($"Current waypoint => {_currentWaypoint.position}");
        }
        else
        {
            //Debug.LogWarning($"SelectNextWaypoint: _currentWaypoint is null!");
            return TaskStatus.Failure;
        }

        CurrentWaypointIndex.Value += 1;
        if (CurrentWaypointIndex.Value >= _waypointTransforms.Count)
        {
            CurrentWaypointIndex.Value = 0;
        }
        //Debug.Log($"Incrementing CurrentWaypointIndex => {CurrentWaypointIndex.Value}");

        return TaskStatus.Success;
    }

    public override void OnReset()
    {
        FillWaypointList();
        _currentWaypoint = null;
        CurrentWaypointIndex.Value = 0;
        CurrentWaypointPosition = null;
    }

    private void FillWaypointList()
    {
        _waypointTransforms = new List<Transform>();

        foreach (Transform child in WaypointParent)
        {
            _waypointTransforms.Add(child);
        }
    }
}
