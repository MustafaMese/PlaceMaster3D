using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    protected UIManager() { }
    private static UIManager _instance = null;
    public static UIManager Instance
    {
        get
        {
            return _instance;
        }
    }
    public StartCanvas startCanvas;
    public InGameCanvas inGameCanvas;
    public EndGameCanvas endGameCanvas;
    public FailCanvas failCanvas;
    public RewardRoomCanvas rewardRoomCanvas;

    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (_instance != null && _instance != this) 
        {
            Destroy(this.gameObject);
        }
 
        _instance = this;
        
        DontDestroyOnLoad( this.gameObject );
        
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        UpdateCanvasState(GameManager.Instance.gameState);
        print("Buradayım");
    }

    private void Update()
    {
        // if (GameManager.Instance.gameState == GameState.START_MENU && Input.anyKey)
        //     startCanvas.StartGame();
    }

    public void UpdateCanvasState(GameState state)
    {
        ResetCanvases();
        switch (state)
        {
            case GameState.END:
                endGameCanvas.SetPanelActive(true);
                break;
            case GameState.FAIL:
                failCanvas.SetPanelActive(true);
                break;
            case GameState.PLAY:
                inGameCanvas.SetPanelActive(true);
                break;
            case GameState.START_MENU:
                startCanvas.SetPanelActive(true);
                break;
            case GameState.REWARD_MENU:
                rewardRoomCanvas.SetPanelActive(true);
                break;
        }
    }
    
    private void ResetCanvases()
    {
        if (startCanvas == null)
            startCanvas = FindObjectOfType<StartCanvas>();
        
        if (inGameCanvas == null)
            inGameCanvas = FindObjectOfType<InGameCanvas>();
        
        if (endGameCanvas == null)
            endGameCanvas = FindObjectOfType<EndGameCanvas>();
        
        if (failCanvas == null)
            failCanvas = FindObjectOfType<FailCanvas>();
        
        if (rewardRoomCanvas == null)
            rewardRoomCanvas = FindObjectOfType<RewardRoomCanvas>();
        
        if (startCanvas != null)
            startCanvas.SetPanelActive(false);
        
        if (inGameCanvas != null)
            inGameCanvas.SetPanelActive(false);
        
        if(endGameCanvas != null)
            endGameCanvas.SetPanelActive(false);
        
        if(failCanvas != null)
            failCanvas.SetPanelActive(false);
        
        if(rewardRoomCanvas != null)
            rewardRoomCanvas.SetPanelActive(false);
    }
    
    public void OnApplicationQuit()
    {
        UIManager._instance = null;
    }
}
