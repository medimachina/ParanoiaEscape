using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.ootii.Messages;

public class ButtonMessageSender : MonoBehaviour
{
    [SerializeField]
    private string _message;
    [SerializeField]
    private UnityEngine.Object _optionalData;

    public void DispatchMessage()
    {
        if (_optionalData != null)
        {
            MessageDispatcher.SendMessage(this, _message, _optionalData, 0);
        }
        else
        {
            MessageDispatcher.SendMessage(_message);
        }
        
    }
}
