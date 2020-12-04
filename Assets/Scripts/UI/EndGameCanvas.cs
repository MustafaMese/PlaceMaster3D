using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine;

public class EndGameCanvas : MonoBehaviour
{
    [SerializeField] GameObject panel;

    [SerializeField] Button loseAdsButton;
    [SerializeField] Button adsButton;
    [SerializeField] Button nextButton;

    [SerializeField] float loseButtonTime;

    public void SetPanelActive(bool b)
    {
        panel.SetActive(b);
        if (b)
        {
            DOTween.KillAll();
            StartCoroutine(ActiveLoseButton());
        }
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
        GameManager.Instance.SetGameState(GameState.LEVEL_TRANSITION);
    }
}
