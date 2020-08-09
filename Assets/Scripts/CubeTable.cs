using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CubeTable
{
    public EffectorCube cube;
    public Vector3 position;

    public CubeTable(EffectorCube cube, Vector3 position)
    {
        this.cube = cube;
        this.position = position;
    }

    public EffectorCube GetCube()
    {
        return cube;
    }

    public Vector3 GetPosition()
    {
        return position;
    }
}
