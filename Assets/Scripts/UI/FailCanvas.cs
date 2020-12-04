using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FailCanvas : MonoBehaviour
{
    [SerializeField] GameObject panel;

    [SerializeField] Button tryAgainButton;

    [SerializeField] float tryButtonTime = 1.5f;

    public void SetPanelActive(bool b)
    {
        panel.SetActive(b);

        if(b)
            ChoicePanel();
    }

    //TODO Belki burda bölüm kayıtlanabailir ve benzeri bişi yapılabilir.
    private void ChoicePanel()
    {
        StartCoroutine(ActiveTryAgainButton());
        StartCoroutine(RestartLevelByTime());
    }

    private IEnumerator ActiveTryAgainButton()
    {
        yield return new WaitForSeconds(tryButtonTime);
        tryAgainButton.gameObject.SetActive(true);
    }


    public void WatchAd()
    {
        print("Adleri izliyorum.");
    }

    private IEnumerator RestartLevelByTime()
    {
        yield return new WaitForSeconds(tryButtonTime * 2f);
        GameManager.Instance.SetGameState(GameState.RESTART_LEVEL);
    }
}
