using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using Tooltip = BehaviorDesigner.Runtime.Tasks.TooltipAttribute;
using com.ootii.Messages;

[TaskCategory("Custom")]
[TaskDescription("Sends a string message through com.ooti.MessageDispatcher")]
public class DispatchMessage : Action
{
    [Tooltip("This string will be sent as a message.")]
    public string Message;

    public override TaskStatus OnUpdate()
    {
        MessageDispatcher.SendMessage(Message);
        return TaskStatus.Success;
    }
}
