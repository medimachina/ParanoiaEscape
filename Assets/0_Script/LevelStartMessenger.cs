using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.ootii.Messages;

public class LevelStartMessenger : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(SendMessageNextFrame(Msg.LevelStarted));
    }

    IEnumerator SendMessageNextFrame(string msg)
    {
        yield return 0;
        Debug.Log($"Music: {name} Sending message {Msg.LevelStarted}");
        MessageDispatcher.SendMessage(msg);
    }
}
