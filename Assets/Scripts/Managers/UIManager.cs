using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance = null;
    public static UIManager Instance
    {
        get { return _instance; }
        set { _instance = value; }
    }

    [SerializeField] StartCanvas startCanvasPrefab;
    [SerializeField] InGameCanvas inGameCanvasPrefab;
    [SerializeField] EndGameCanvas endGameCanvasPrefab;
    [SerializeField] FailCanvas failCanvasPrefab;
    [SerializeField] RewardRoomCanvas rewardRoomCanvasPrefab;

    private StartCanvas _startCanvas;
    private InGameCanvas _inGameCanvas;
    private EndGameCanvas _endGameCanvas;
    private FailCanvas _failCanvas;
    private RewardRoomCanvas _rewardRoomCanvas;

    private bool ended = false;

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance);
        else
            Instance = this;

        Initialize();
    }

    private void Initialize()
    {
        InitializeCanvases();
    }

    public void UpdateCanvasState(GameState state)
    {
        switch (state)
        {
            case GameState.END:
                _inGameCanvas.SetPanelActive(false);

                _endGameCanvas.SetPanelActive(true);
                break;
            case GameState.FAIL:
                if(!ended)
                    _failCanvas.SetPanelActive(true);
                break;
            case GameState.PLAY:
                _startCanvas.SetPanelActive(false);

                _inGameCanvas.SetPanelActive(true);
                break;
            case GameState.START_MENU:
                _startCanvas.SetPanelActive(true);
                break;
            case GameState.LEVEL_TRANSITION:
                _endGameCanvas.SetPanelActive(false);

                LoadManager.Instance.NextLevel();
                break;
            case GameState.RESTART_LEVEL:
                _endGameCanvas.SetPanelActive(false);

                LoadManager.Instance.RestartLevel();
                break;
            case GameState.REWARD_MENU:
                _rewardRoomCanvas.SetPanelActive(true);
                break;
        }
    }
    
    private void InitializeCanvases()
    {
        if (_startCanvas == null)
            _startCanvas = Instantiate(startCanvasPrefab);

        if (_inGameCanvas == null)
            _inGameCanvas = Instantiate(inGameCanvasPrefab);

        if (_endGameCanvas == null)
            _endGameCanvas = Instantiate(endGameCanvasPrefab);

        if (_failCanvas == null)
            _failCanvas = Instantiate(failCanvasPrefab);

        if (_rewardRoomCanvas == null)
            _rewardRoomCanvas = Instantiate(rewardRoomCanvasPrefab);
    }

    private void OnDestroy()
    {
        if (_startCanvas != null)
            Destroy(_startCanvas);

        if (_inGameCanvas != null)
            Destroy(_inGameCanvas);

        if (_endGameCanvas != null)
            Destroy(_endGameCanvas);

        if (_failCanvas != null)
            Destroy(_failCanvas);

        if (_rewardRoomCanvas != null)
            Destroy(_rewardRoomCanvas);
    }

    public void OnApplicationQuit()
    {
        UIManager._instance = null;
    }

    public void UpdateMoveCountText(int number)
    {
        _inGameCanvas.UpdateMoveCountText(number);
    }
}
