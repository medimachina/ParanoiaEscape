using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Tooltip = BehaviorDesigner.Runtime.Tasks.TooltipAttribute;

[TaskCategory("Custom")]
public class AlarmIsActive : Conditional
{
    public string NameOfAlarmObject = "Alarm";
    Alarm _alarm;
    public override void OnAwake()
    {
        GameObject alarmObj = GameObject.Find(NameOfAlarmObject);
        if (alarmObj != null)
        {
            _alarm = alarmObj.GetComponent<Alarm>();
        }
    }

    public override TaskStatus OnUpdate()
    {
        if (_alarm && _alarm.AlarmActive)
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
        _alarm = null;
    }
}
