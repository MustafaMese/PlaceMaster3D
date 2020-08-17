using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionChangerCube : EffectorCube
{
    [SerializeField] Direction direction;

    public override void Effect(BouncerCube cubeController)
    {
        Vector3 oldPosition = cubeController.GetOldPosition(cubeController.GetDirection());
        cubeController.SetTile(oldPosition);
        cubeController.SetDirection(direction);
        Vector3 nextPosition = cubeController.GetNextPosition(direction);
        cubeController.Move(nextPosition);
    }
}
