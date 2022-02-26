using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.ootii.Messages;
using Sirenix.OdinInspector;
using System;

public class InGameMenu : MonoBehaviour
{
    public UiPanel LostGamePanel;
    public UiPanel WonLevelPanel;
    public UiPanel FinishedGamePanel;

    private List<UiPanel> _allPanels;

    private void Awake()
    {
        _allPanels = new List<UiPanel>();
        _allPanels.Add(LostGamePanel);
        _allPanels.Add(WonLevelPanel);
        _allPanels.Add(FinishedGamePanel);
    }

    private void OnEnable()
    {
        MessageDispatcher.AddListener(Msg.ShowFinishedLevelMenu, OnLevelWon);
        MessageDispatcher.AddListener(Msg.LostGame, OnGameLost);
        MessageDispatcher.AddListener(Msg.ShowFinishedGameMenu, OnGameFinished);
    }

    private void OnDisable()
    {
        MessageDispatcher.RemoveListener(Msg.ShowFinishedLevelMenu, OnLevelWon);
        MessageDispatcher.RemoveListener(Msg.LostGame, OnGameLost);
        MessageDispatcher.RemoveListener(Msg.ShowFinishedGameMenu, OnGameFinished);
    }

    [Button("Try Lost")]
    private void OnGameLost(IMessage rMessage)
    {
        ActivatePanel(LostGamePanel);
    }

    [Button("Try Won")]
    private void OnLevelWon(IMessage rMessage)
    {
        ActivatePanel(WonLevelPanel);
    }

    [Button("Try Finished")]
    private void OnGameFinished(IMessage rMessage)
    {
        ActivatePanel(FinishedGamePanel);
    }

    private void ActivatePanel(UiPanel panel)
    {
        HideAllPanels();
        panel.gameObject.SetActive(true);
        panel.Show();
    }

    private void HideAllPanels()
    {
        foreach (var panel in _allPanels)
        {
            panel.gameObject.SetActive(false);
        }
    }

}

