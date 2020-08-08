using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Board : MonoBehaviour
{
    public static Board Instance = null;

    [System.Serializable]
    public struct Tile
    {
        GameObject tile;
        Vector3 position;

        public Tile(GameObject tile, Vector3 position)
        {
            this.tile = tile;
            this.position = position;
        }

        public GameObject GetTile()
        {
            return tile;
        }

        public Vector3 GetPosition()
        {
            return position;
        }
    }

    public List<Tile> tiles = new List<Tile>();
    private const int xSpace = 1;
    private const int ySpace = 1;

    [SerializeField] int rowLength;
    [SerializeField] int colLength;
    [SerializeField] GameObject tilePrefab;

    private Vector3 startPoint = new Vector3(0, 0, 0);

    public bool create = false;
    public bool delete = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (create)
        {
            create = false;
            CreateBoard();
        }

        if (delete)
        {
            delete = false;
            DeleteBoard();
        }
    }

    public void CreateBoard()
    {
        Vector3 currentPos = startPoint;
        for (int i = 0; i < rowLength; i++)
        {
            for (int j = 0; j < colLength; j++)
            {
                var gameObject = Instantiate(tilePrefab, currentPos, Quaternion.identity, transform);
                Tile t = new Tile(gameObject, gameObject.transform.position);
                tiles.Add(t);
                currentPos.x += xSpace;
            }
            currentPos.x = startPoint.x;
            currentPos.z += ySpace;
        }
    }

    public void DeleteBoard()
    {
        foreach (var tile in tiles)
        {
            DestroyImmediate(tile.GetTile());
        }

        tiles.Clear();
    }
}
