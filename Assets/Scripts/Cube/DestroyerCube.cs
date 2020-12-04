using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerCube : EffectorCube
{
    public override void Effect(BouncerCube cubeController)
    {
        TileManager.Instance.PopCube();
        
        Destroy(cubeController.gameObject);
        Destroy(this.gameObject);

        TileManager.Instance.TileControl();
    }
}
