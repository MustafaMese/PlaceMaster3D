using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BoardCreator : MonoBehaviour
{
    private const int xSpace = 1;
    private const int ySpace = 1;

    [SerializeField] int rowLength;
    [SerializeField] int colLength;
    [SerializeField] Tile tilePrefab;
    [SerializeField] Board board;
    private Vector3 startPoint = new Vector3(0, 0, 0);

    public bool create = false;
    public bool delete = false;

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

    private void CreateBoard()
    {
        Vector3 currentPos = startPoint;
        for (int i = 0; i < rowLength; i++)
        {
            for (int j = 0; j < colLength; j++)
            {
                board.CreateTile(currentPos, tilePrefab);
                currentPos.x += xSpace;
            }
            currentPos.x = startPoint.x;
            currentPos.z += ySpace;
        }
    }

    private void DeleteBoard()
    {
        foreach (Transform child in transform)
        {
            DestroyImmediate(child.gameObject);
        }
        board.tiles.Clear();
    }
}
