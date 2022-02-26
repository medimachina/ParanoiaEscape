using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.ootii.Messages;

public class LevelStartMessenger : MonoBehaviour
{
    public string OptionalCustomMessage;
    private string _message;

    void Start()
    {
        if (OptionalCustomMessage == "")
        {
            _message = Msg.LevelStarted;
        }
        else
        {
            _message = OptionalCustomMessage;
        }

        StartCoroutine(SendMessageNextFrame(Msg.LevelStarted));
    }

    IEnumerator SendMessageNextFrame(string msg)
    {
        yield return 0;
        MessageDispatcher.SendMessage(_message);
    }
}
