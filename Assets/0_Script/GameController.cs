using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.ootii.Messages;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public void OnEnable()
    {
        MessageDispatcher.AddListener(Msg.RestartLevel, RestartLevel);
        MessageDispatcher.AddListener(Msg.NextLevel, NextLevel);
    }

    public void OnDisable()
    {
        MessageDispatcher.RemoveListener(Msg.RestartLevel, RestartLevel);
        MessageDispatcher.RemoveListener(Msg.NextLevel, NextLevel);
    }

    public void RestartLevel(IMessage rMessage)
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void NextLevel(IMessage rMessage)
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
