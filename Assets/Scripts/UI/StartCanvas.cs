using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartCanvas : MonoBehaviour
{
    [SerializeField] GameObject tapToStartPanel = null;

    private void Update()
    {
        
    }

    public void StartGame()
    {
        tapToStartPanel.SetActive(false);
        GameManager.Instance.SetGameState(GameState.PLAY);
    }
}
