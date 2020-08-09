using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionChangerCube : EffectorCube
{
    [SerializeField] Direction direction;

    public override void Effect(CubeController cubeController)
    {
        // Bir adım ileri gidiyo bunu düzelt.
        cubeController.SetDirection(direction);
        Vector3 nextPosition = cubeController.GetNextPosition(direction);
        cubeController.Move(nextPosition);
    }
}
