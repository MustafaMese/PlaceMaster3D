using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpaqueCube : EffectorCube
{
    public override void Effect(BouncerCube cubeController)
    {
        cubeController.SetTile(cubeController.transform.position);

        TileManager.Instance.PopCube();
        TileManager.Instance.TileControl();
    }
}
