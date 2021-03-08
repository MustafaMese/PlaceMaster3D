using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class CubePlacer : MonoBehaviour
{
    public Transform t;
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
            //var cube = PrefabUtility.InstantiatePrefab(cubePrefab as EffectorCube) as EffectorCube;
            //cube.transform.position = coordinate;
        }
    }

    private bool TileControl()
    {
        Vector3 location = new Vector3(coordinate.x, 0, coordinate.z);
        
        var hits = Physics.OverlapSphere(location, 0.5f);

        if (hits[0] != null)
        {
            Tile t = hits[0].GetComponent<Tile>();
            if (t.IsTransparent() || t.IsAvaibleToPut())
                return true;
        }
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
