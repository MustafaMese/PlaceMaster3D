using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCube : EffectorCube
{
    [SerializeField] Material nonGhostMaterial;
    private bool isInGhostForm = true;

    public override void Effect(BouncerCube cubeController)
    {
        if (isInGhostForm)
        {
            GetComponent<MeshRenderer>().material = nonGhostMaterial;
            isInGhostForm = false;

            Vector3 nextPos = cubeController.GetNextPosition(cubeController.GetDirection());
            cubeController.Move(nextPos);
        }
        else
        {
            cubeController.SetTile(cubeController.transform.position);

            TileManager.Instance.PopCube();
            TileManager.Instance.TileControl();
        }
    }
}
