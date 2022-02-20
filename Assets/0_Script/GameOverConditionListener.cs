using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.ootii.Messages;
using System;

public class GameOverConditionListener : MonoBehaviour
{
    
    void OnEnable()
    {
        MessageDispatcher.AddListener(Msg.LostGame, DebugLostGame);
        MessageDispatcher.AddListener(Msg.LostGame, DebugWonGame);
    }

    private void DebugLostGame(IMessage rMessage)
    {
        Debug.Log($"You lost the game!");
    }

    private void DebugWonGame(IMessage rMessage)
    {
        Debug.Log($"You escaped the asylum!");
    }

    void OnDisable()
    {
        MessageDispatcher.RemoveListener(Msg.LostGame, DebugLostGame);
        MessageDispatcher.RemoveListener(Msg.LostGame, DebugWonGame);
    }


}
