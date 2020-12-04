using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameCanvas : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] Slider levelProgressSlider;
    [SerializeField] TextMeshProUGUI levelCountText;
    [SerializeField] TextMeshProUGUI moveCountText;

    private void SetSliderProgress()
    {
        levelProgressSlider.value = 0;
        int levelNumber = GetLevelNumber();
        float v = (levelNumber % 10);
        float levelProgress = v / 10;
        levelProgressSlider.value = levelProgress;
    }

    private int GetLevelNumber()
    {
        int levelNumber = SceneManager.GetActiveScene().buildIndex + 1;
        return levelNumber;
    }

    private void SetLevelCountText()
    {
        var levelNumber = GetLevelNumber();
        levelCountText.text = levelNumber.ToString();
    }
    
    public void SetPanelActive(bool b)
    {
        panel.SetActive(b);

        if (b)
        {
            SetSliderProgress();
            SetLevelCountText();
        }
    }

    public void RestartButton()
    {
        GameManager.Instance.SetGameState(GameState.RESTART_LEVEL);
    }
    
    public void UpdateMoveCountText(int number)
    {
        moveCountText.text = number.ToString();
    }
    
}
