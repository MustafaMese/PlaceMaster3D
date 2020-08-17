using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BouncerCubeTable
{
    public BouncerCube cube;
    public Vector3 position;

    public BouncerCubeTable(BouncerCube cube, Vector3 position)
    {
        this.cube = cube;
        this.position = position;
    }

    public void SetPosition(Vector3 pos)
    {
        position = pos;
        Debug.Log(position);
    }

    public void SetCube(BouncerCube cube)
    {
        this.cube = cube;
    }

    public BouncerCube GetCube()
    {
        return cube;
    }

    public Vector3 GetPosition()
    {
        return position;
    }
}
