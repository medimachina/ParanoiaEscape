using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Tooltip = BehaviorDesigner.Runtime.Tasks.TooltipAttribute;

[TaskCategory("Custom")]
public class StartingPointIsNear : Conditional
{
    [Tooltip("Max distance from start point to go")]
    public float MaxDistance = 15;
    public Transform _agentTransform;
    private Vector3 _startPoint;

    public override void OnAwake()
    {
        _startPoint = _agentTransform.position;
    }

    public override TaskStatus OnUpdate()
    {
        if (Vector3.Distance(_startPoint, _agentTransform.position) < MaxDistance)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }

    public override void OnReset()
    {
        _agentTransform = null;
    }
}
