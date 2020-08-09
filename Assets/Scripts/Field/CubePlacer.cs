using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CubePlacer : MonoBehaviour
{
    private Vector3 defaultCoordinate = new Vector3(-1, -1, -1);
    
    [SerializeField] private Vector3 coordinate = Vector3.zero;
    [SerializeField] private EffectorCube cubePrefab = null;
    [SerializeField] private bool create = false;
    [SerializeField] private BoardCreator boardCreator = null;
    [SerializeField] private Board board = null;
    private void Update()
    {
        if (create)
        {
            create = false;
            Create();
        }
    }

    private void Create()
    {
        int row = boardCreator.GetRowLength();
        int col = boardCreator.GetColLength();

        if (coordinate == defaultCoordinate || cubePrefab == null) 
            return;
        if (CubeControl()) 
            return;
        if (TileControl()) 
            return;
        InitializeCube(row, col);
        Reset();
    }

    private void InitializeCube(int row, int col)
    {
        coordinate.y = 1;
        if (coordinate.x >= 0 && coordinate.x < col && coordinate.z >= 0 && coordinate.z < row)
        {
            Instantiate(cubePrefab, coordinate, Quaternion.identity);
        }
    }

    private bool TileControl()
    {
        Tile t = board.GetTile(new Vector3(coordinate.x, 0, coordinate.z));
        if (t.IsTransparent() || t.IsAvaibleToPut())
            return true;
        return false;
    }

    private bool CubeControl()
    {
        foreach (var cube in FindObjectsOfType<EffectorCube>())
        {
            if (cube.transform.position == coordinate)
                return true;
        }
        return false;
    }

    private void Reset()
    {
        coordinate = defaultCoordinate;
        cubePrefab = null;
    }
}
