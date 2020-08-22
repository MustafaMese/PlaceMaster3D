using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameCanvas : MonoBehaviour
{
    [SerializeField] Slider levelProgressSlider;

    public void SetSliderProgress()
    {
        levelProgressSlider.value = 0;
        int levelNumber = SceneManager.GetActiveScene().buildIndex + 1;
        float v = (levelNumber % 10);
        float levelProgress = v / 10;
        levelProgressSlider.value = levelProgress;
    }
}
