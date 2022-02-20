using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.ootii.Messages;
using System;

public class DetectLoss : MonoBehaviour
{
    
    void OnEnable()
    {
        MessageDispatcher.AddListener(Msg.LostGame, DebugLostGame);
    }

    private void DebugLostGame(IMessage rMessage)
    {
        Debug.Log($"You lost the game!");
    }

    void OnDisable()
    {
        MessageDispatcher.RemoveListener(Msg.LostGame, DebugLostGame);
    }


}
