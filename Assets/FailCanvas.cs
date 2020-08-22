using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailCanvas : MonoBehaviour
{
    [SerializeField] GameObject panel;

    private void OnEnable()
    {
        UIManager.Instance.failCanvas = this;
        print("hob");
    }

    public void SetPanelActive(bool b)
    {
        panel.SetActive(b);
    }
}
