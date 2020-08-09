﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TileOnEditor : MonoBehaviour
{
    [SerializeField] private Tile tile;

    void Update()
    {
        tile.SetGlow();
        tile.SetTransparent();
    }
}
