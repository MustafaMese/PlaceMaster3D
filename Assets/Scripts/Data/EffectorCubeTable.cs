using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EffectorCubeTable
{
    public EffectorCube cube;
    public Vector3 position;

    public EffectorCubeTable(EffectorCube cube, Vector3 position)
    {
        this.cube = cube;
        this.position = position;
    }

    public void SetPosition(Vector3 pos)
    {
        position = pos;
    }

    public void SetCube(EffectorCube cube)
    {
        this.cube = cube;
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
