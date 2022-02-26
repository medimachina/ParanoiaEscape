using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Tooltip = BehaviorDesigner.Runtime.Tasks.TooltipAttribute;

[TaskCategory("Custom")]
public class ObjectIsNear : Conditional
{
    [Tooltip("Max distance to player to count as near")]
    public float MaxDistance = 15;
    public SharedTransform _objectTransform;
    public Transform _agentTransform;

    public override void OnAwake()
    {
    }

    public override TaskStatus OnUpdate()
    {
        if (Vector3.Distance(_objectTransform.Value.position, _agentTransform.position) < MaxDistance)
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
        _objectTransform = null;
        _agentTransform = null;
    }
}
