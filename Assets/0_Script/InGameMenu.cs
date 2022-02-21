using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.ootii.Messages;
using Sirenix.OdinInspector;
using System;

public class InGameMenu : MonoBehaviour
{
    public UiPanel LostGamePanel;
    public UiPanel WonGamePanel;

    private List<UiPanel> _allPanels;

    private void Awake()
    {
        _allPanels = new List<UiPanel>();
        _allPanels.Add(LostGamePanel);
        _allPanels.Add(WonGamePanel);
    }

    private void OnEnable()
    {
        MessageDispatcher.AddListener(Msg.WonGame, OnGameWon);
        MessageDispatcher.AddListener(Msg.LostGame, OnGameLost);
    }

    private void OnDisable()
    {
        MessageDispatcher.RemoveListener(Msg.WonGame, OnGameWon);
        MessageDispatcher.RemoveListener(Msg.LostGame, OnGameLost);
    }

    [Button("Try Lost")]
    private void OnGameLost(IMessage rMessage)
    {
        ActivatePanel(LostGamePanel);
    }

    [Button("Try Won")]
    private void OnGameWon(IMessage rMessage)
    {
        ActivatePanel(WonGamePanel);
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
