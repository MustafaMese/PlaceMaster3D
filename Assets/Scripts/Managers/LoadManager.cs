using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    protected LoadManager() { }

    private static LoadManager _instance = null;
    public static LoadManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<LoadManager>();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        DOTween.Init();
    }

    public void RestartLevel()
    {
        GameManager.Instance.SetGameState(GameState.MAIN_MENU);
        DOTween.Clear();
        var scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);
    }

    public void NextLevel()
    {
        GameManager.Instance.SetGameState(GameState.MAIN_MENU);
        DOTween.Clear();
        var scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex + 1);
    }
}
