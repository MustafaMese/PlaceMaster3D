using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { PLAY, START_MENU, END, FAIL, REWARD_MENU, NONE}
public class GameManager : MonoBehaviour
{
    protected GameManager() { }

    private static GameManager _instance = null;
    public static GameManager Instance 
    { 
        get 
        {
            return _instance;
        }
    }

    public GameState gameState { get; private set; }
    private UIManager uiManager;
    
    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (_instance != null && _instance != this) 
        {
            Destroy(this.gameObject);
        }
 
        _instance = this;
        DontDestroyOnLoad( this.gameObject );
        gameState = GameState.START_MENU;
        uiManager = FindObjectOfType <UIManager>();
    }

    public void SetGameState(GameState state)
    {
        gameState = state;
        uiManager.UpdateCanvasState(gameState);
    }

    public void OnApplicationQuit()
    {
        GameManager._instance = null;
    }
}
