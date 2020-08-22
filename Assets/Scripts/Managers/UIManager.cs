using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    protected UIManager() { }
    private static GameManager _instance = null;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<GameManager>();
            return _instance;
        }
    }
    private StartCanvas startCanvas;
    private InGameCanvas inGameCanvas;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Initialize();
    }

    private void Initialize()
    {
        startCanvas = FindObjectOfType<StartCanvas>();
        inGameCanvas = FindObjectOfType<InGameCanvas>();
        inGameCanvas.SetSliderProgress();
        print("Buradayım");
    }

    private void Update()
    {
        if (GameManager.Instance.gameState == GameState.MAIN_MENU && Input.anyKey)
            startCanvas.StartGame();
    }
}
