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
            return _instance;
        }
    }

    
    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (_instance != null && _instance != this) 
        {
            Destroy(this.gameObject);
        }
 
        _instance = this;
        DontDestroyOnLoad( this.gameObject );
        
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
