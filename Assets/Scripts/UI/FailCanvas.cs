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
    public bool b;

    private void OnEnable()
    {
        UIManager.Instance.failCanvas = this;
        print("hob");
    }

    private void Update()
    {
        if (b)
        {
            b = false;
            SetPanelActive(true);
        }
    }

    public void SetPanelActive(bool b)
    {
        panel.SetActive(b);

        if (b)
        {
            StartCoroutine(ActiveTryAgainButton());
            StartCoroutine(RestartLevelByTime());
        }
    }

    private IEnumerator ActiveTryAgainButton()
    {
        yield return new WaitForSeconds(tryButtonTime);
        tryAgainButton.gameObject.SetActive(true);
    }

    public void RestartLevel()
    {
        LoadManager.Instance.RestartLevel();
    }

    public void WatchAd()
    {
        print("Adleri izliyorum.");
    }

    private IEnumerator RestartLevelByTime()
    {
        yield return new WaitForSeconds(tryButtonTime * 2f);
        RestartLevel();
    }
}
