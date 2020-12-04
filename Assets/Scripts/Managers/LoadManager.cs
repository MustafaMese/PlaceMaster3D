using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    private static LoadManager _instance = null;
    public static LoadManager Instance
    {
        get { return _instance; }
        set { _instance = value; }
    }

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance);
        else
            Instance = this;

        DOTween.Init();
    }

    public void RestartLevel()
    {
        StopAllCoroutines();
        GameManager.Instance.SetGameState(GameState.START_MENU);
        DOTween.Clear();
        var scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);
    }

    public void NextLevel()
    {
        GameManager.Instance.SetGameState(GameState.START_MENU);
        DOTween.Clear();
        var scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex + 1);
    }
    
    public void OnApplicationQuit()
    {
        LoadManager._instance = null;
    }
}
