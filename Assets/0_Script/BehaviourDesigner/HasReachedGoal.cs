using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Tooltip = BehaviorDesigner.Runtime.Tasks.TooltipAttribute;
using UnityEngine.AI;

[TaskCategory("Custom")]
public class HasReachedGoal : Conditional
{
    public Transform NavmeshAgentTransform;
    public SharedVector3 GoalPosition;
    public float DistanceTolerance = 0.1f;

    public override void OnAwake()
    {
        
    }

    public override TaskStatus OnUpdate()
    {
        float distance = Vector3.Distance(NavmeshAgentTransform.position, GoalPosition.Value);

        if (distance < DistanceTolerance)
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
        
    }
}
