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
    private void Update()
    {
        if (create)
        {
            create = false;
            CreateCube();
        }
    }

    private void CreateCube()
    {
        int row = boardCreator.GetRowLength();
        int col = boardCreator.GetColLength();

        if (coordinate == defaultCoordinate || cubePrefab == null) return;

        foreach (var cube in FindObjectsOfType<EffectorCube>())
        {
            if (cube.transform.position == coordinate)
                return;
        }

        coordinate.y = 1;
        if (coordinate.x >= 0 && coordinate.x < col && coordinate.z >= 0 && coordinate.z < row)
        {
            Instantiate(cubePrefab, coordinate, Quaternion.identity);
        }

        Reset();
    }

    private void Reset()
    {
        coordinate = defaultCoordinate;
        cubePrefab = null;
    }
}
