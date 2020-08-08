using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static Board Instance = null;

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

    public List<TileTable> tiles = new List<TileTable>();

    private void Awake()
    {
        Instance = this;
        InitializeTiles();
    }

    private void InitializeTiles()
    {
        var count = transform.childCount;
        for(int i = 0; i < count; i++)
        {
            Tile t = transform.GetChild(i).GetComponent<Tile>();;
            TileTable tt = new TileTable(t, t.transform.position);
            tiles.Add(tt);
        }
    }

    public void CreateTile(Vector3 currentPos, Tile tilePrefab)
    {
        Tile tile = Instantiate(tilePrefab, currentPos, Quaternion.identity, transform);
        TileTable t = new TileTable(tile, tile.transform.position);
        tiles.Add(t);
    }

}
