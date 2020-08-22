using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EndGameCanvas : MonoBehaviour
{
    [SerializeField] GameObject panel;

    [SerializeField] Button loseAdsButton;
    [SerializeField] Button adsButton;
    [SerializeField] Button nextButton;

    [SerializeField] float loseButtonTime;

    private void OnEnable()
    {
        UIManager.Instance.endGameCanvas = this;
    }

    public void SetPanelActive(bool b)
    {
        print("hob");
        panel.SetActive(b);
        if(b)
            StartCoroutine(ActiveLoseButton());
    }

    private IEnumerator ActiveLoseButton()
    {
        yield return new WaitForSeconds(loseButtonTime);
        loseAdsButton.gameObject.SetActive(true);
    }

    public void ActiveNextButton()
    {
        loseAdsButton.gameObject.SetActive(false);
        adsButton.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(true);
    }

    public void NextLevel()
    {
        LoadManager.Instance.NextLevel();
    }
}
