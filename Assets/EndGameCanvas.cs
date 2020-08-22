using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameCanvas : MonoBehaviour
{
    [SerializeField] GameObject panel;

    private void OnEnable()
    {
        UIManager.Instance.endGameCanvas = this;
    }

    public void SetPanelActive(bool b)
    {
        panel.SetActive(b);
    }
}
