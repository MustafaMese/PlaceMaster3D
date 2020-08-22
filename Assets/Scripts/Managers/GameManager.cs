using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { PLAY, MAIN_MENU, END}
public class GameManager : MonoBehaviour
{
    protected GameManager() { }

    private static GameManager _instance = null;
    public static GameManager Instance 
    { 
        get 
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
            }

            return _instance;
        }
    }

    public GameState gameState { get; private set; }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        gameState = GameState.MAIN_MENU;
    }

    public void SetGameState(GameState state)
    {
        this.gameState = state;
    }

    public void OnApplicationQuit()
    {
        GameManager._instance = null;
    }
}
