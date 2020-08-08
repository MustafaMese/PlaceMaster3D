using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct TileTable
{
    Tile tile;
    Vector3 position;

    public TileTable(Tile tile, Vector3 position)
    {
        this.tile = tile;
        this.position = position;
    }

    public Tile GetTile()
    {
        return tile;
    }

    public Vector3 GetPosition()
    {
        return position;
    }
}
