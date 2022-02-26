using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.ootii.Messages;
using UnityEngine.SceneManagement;
using System;

public class GameController : SingletonBase<GameController>
{
    [SerializeField]
    private int _currentLevel = 0;
    [SerializeField]
    private int _levelCount = 2;

    public StringEvent LevelWon;
    public StringEvent LevelLost;

    public int CurrentLevel => _currentLevel;

    private Coroutine _loadingSceneNextFrame;

    public void OnEnable()
    {
        MessageDispatcher.AddListener(Msg.RestartLevel, RestartLevel);
        MessageDispatcher.AddListener(Msg.NextLevel, NextLevel);
        MessageDispatcher.AddListener(Msg.StartTutorial, StartTutorial);
        MessageDispatcher.AddListener(Msg.RestartGame, RestartGame);
        MessageDispatcher.AddListener(Msg.WonGame, OnLevelWon);
        //MessageDispatcher.AddListener(Msg.LevelStarted, OnLevelStarted);
    }

    public void OnDisable()
    {
        MessageDispatcher.RemoveListener(Msg.RestartLevel, RestartLevel);
        MessageDispatcher.RemoveListener(Msg.NextLevel, NextLevel);
        MessageDispatcher.RemoveListener(Msg.StartTutorial, StartTutorial);
        MessageDispatcher.RemoveListener(Msg.RestartGame, RestartGame);
        MessageDispatcher.RemoveListener(Msg.WonGame, OnLevelWon);
        //MessageDispatcher.RemoveListener(Msg.LevelStarted, OnLevelStarted);
    }

    public void RestartLevel(IMessage rMessage)
    {
        Scene scene = SceneManager.GetActiveScene();
        LoadSceneNextFrame(scene.name);
    }

    public void NextLevel(IMessage rMessage)
    {
        LoadSceneNextFrame("GameLevel");
    }

    private void StartTutorial(IMessage rMessage)
    {
        LoadSceneNextFrame("Tutorial");
    }

    private void RestartGame(IMessage rMessage)
    {
        ResetForNewGame();
        LoadSceneNextFrame("GameLevel");
    }

    private void LoadSceneNextFrame(string sceneName)
    {
        if (_loadingSceneNextFrame == null)
        {
            CleanupBeforeNext();
            StartCoroutine(LoadSceneNextFrameCo(sceneName));
        }
    }

    private IEnumerator LoadSceneNextFrameCo(string sceneName)
    {
        yield return 0;
        SceneManager.LoadScene(sceneName);
    }

    private void OnLevelWon(IMessage rMessage)
    {
        _currentLevel++;
        if (_currentLevel > _levelCount)
        {
            MessageDispatcher.SendMessage(Msg.ShowFinishedGameMenu);
        }
        else
        {
            MessageDispatcher.SendMessage(Msg.ShowFinishedLevelMenu);
        }
    }

    private void CleanupBeforeNext()
    {
        TimeMgr.Resume();
    }


    private void ResetForNewGame()
    {
        _currentLevel = 0;
    }

}
