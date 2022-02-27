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

    private CardTopBar _topBar;

    public int CurrentLevel => _currentLevel;

    private Coroutine _loadingSceneNextFrame;

    private CardDeck _deck;
    private Transform _floorContainer;
    private List<ColoredFloor> _coloredFloors;


    public void OnEnable()
    {
        MessageDispatcher.AddListener(Msg.RestartLevel, RestartLevel);
        MessageDispatcher.AddListener(Msg.NextLevel, SelectParanoias);
        MessageDispatcher.AddListener(Msg.StartTutorial, StartTutorial);
        MessageDispatcher.AddListener(Msg.RestartGame, RestartGame);
        MessageDispatcher.AddListener(Msg.WonGame, OnLevelWon);
        MessageDispatcher.AddListener(Msg.AllParanoiasSelected, NextLevel);
        MessageDispatcher.AddListener(Msg.LevelStarted, OnLevelStarted);
    }

    private void OnLevelStarted(IMessage rMessage)
    {
        Debug.Log("Cards: Level started");
        SetActiveParanoias();
    }

    private void SetActiveParanoias()
    {
        _topBar = GameObject.Find("TopBar")?.GetComponent<CardTopBar>();

        Debug.Log($"Cards: _topBar: {_topBar != null}");
        Debug.Log($"Cards: _deck: {_deck != null}");
        if (_topBar != null && _deck != null)
        {
            _topBar.SetDeck(_deck);
            _topBar.ActivateParanoias();
        }
    }

    public void OnDisable()
    {
        MessageDispatcher.RemoveListener(Msg.RestartLevel, RestartLevel);
        MessageDispatcher.RemoveListener(Msg.NextLevel, SelectParanoias);
        MessageDispatcher.RemoveListener(Msg.StartTutorial, StartTutorial);
        MessageDispatcher.RemoveListener(Msg.RestartGame, RestartGame);
        MessageDispatcher.RemoveListener(Msg.WonGame, OnLevelWon);
        MessageDispatcher.RemoveListener(Msg.AllParanoiasSelected, NextLevel);
        MessageDispatcher.RemoveListener(Msg.LevelStarted, OnLevelStarted);
    }

    internal void SetParanoias(CardDeck deck)
    {
        _deck = deck;
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

    public void SelectParanoias(IMessage rMessage)
    {
        LoadSceneNextFrame("SelectParanoias");
    }

    private void StartTutorial(IMessage rMessage)
    {
        LoadSceneNextFrame("Tutorial");
    }

    private void RestartGame(IMessage rMessage)
    {
        ResetForNewGame();
        LoadSceneNextFrame("SelectParanoias");
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
            ResetForNewGame();
            MessageDispatcher.SendMessage(Msg.ShowFinishedGameMenu);
        }
        else
        {
            MessageDispatcher.SendMessage(Msg.ShowFinishedLevelMenu);
        }
    }

    public void SetFirstLevel()
    {
        _currentLevel = 1;
    }

    private void CleanupBeforeNext()
    {
        TimeMgr.Resume();
    }


    private void ResetForNewGame()
    {
        _currentLevel = 1;
        //_deck = new CardDeck();
        _floorContainer = null;
        _coloredFloors = new List<ColoredFloor>();
        _topBar = null;
    }

}
