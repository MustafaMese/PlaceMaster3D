using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardRoomCanvas : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    public void SetPanelActive(bool b)
    {
        panel.SetActive(b);
    }
}
