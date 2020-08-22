using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StartCanvas : MonoBehaviour
{
    [SerializeField] GameObject tapToStartPanel = null;

    private void OnEnable()
    {
        UIManager.Instance.startCanvas = this;
    }

    public void StartGame()
    {
        SetPanelActive(false);
        GameManager.Instance.SetGameState(GameState.PLAY);
    }

    public void SetPanelActive(bool b)
    {
        tapToStartPanel.SetActive(b);
    }
    public void Tap()
    {
        StartGame();
    }
}
