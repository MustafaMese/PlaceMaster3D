using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { PLAY, START_MENU, END, FAIL, REWARD_MENU, LEVEL_TRANSITION, RESTART_LEVEL, NONE}
public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;
    public static GameManager Instance 
    { 
        get 
        {
            return _instance;
        }
        set { _instance = value; }
    }

    public GameState gameState { get; private set; }
    private bool started = false;

    [SerializeField] UIManager uiManagerPrefab;
    [SerializeField] LoadManager loadManagerPrefab;
    
    private void Awake()
    {
        if (_instance != null ) 
            Destroy(this.gameObject);
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }

        Initialize();
    }

    private void OnLevelWasLoaded(int level)
    {
        if (!started)
        {
            print("11");
            started = true;
            Initialize();
        }
    }

    private void Initialize()
    {
        Instantiate(uiManagerPrefab);
        Instantiate(loadManagerPrefab);

        SetGameState(GameState.START_MENU);
    }

    public void SetGameState(GameState state)
    {
        if (state == GameState.END || state == GameState.FAIL)
            started = false;

        gameState = state;
        UIManager.Instance.UpdateCanvasState(gameState);
    }

    private void OnDestroy()
    {
        gameState = GameState.START_MENU;
        started = false;
    }

    public void OnApplicationQuit()
    {
        GameManager._instance = null;
    }
}
