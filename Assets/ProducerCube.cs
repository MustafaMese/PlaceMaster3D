using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProducerCube : EffectorCube
{
    [SerializeField] Direction direction;
    [SerializeField] BouncerCube bouncerCubePrefab;


    public override void Effect(BouncerCube cubeController)
    {
        StartCoroutine(ProduceCube(cubeController));
    }

    private IEnumerator ProduceCube(BouncerCube cubeController)
    {
        while (true)
        {
            BouncerCube cube = InitializeBouncerCube(cubeController);
            SetVariblesOfBouncerCube(cube);
            yield return new WaitForSeconds(1.2f);
        }
    }

    private void SetVariblesOfBouncerCube(BouncerCube bouncerCube)
    {
        bouncerCube.SetTile(transform.position);
        bouncerCube.SetDirection(direction);
        Vector3 nextPosition = bouncerCube.GetNextPosition(direction);
        bouncerCube.Move(nextPosition);
    }

    private BouncerCube InitializeBouncerCube(BouncerCube cubeController)
    {
        BouncerCube controller;
        controller = Instantiate(bouncerCubePrefab, transform.position, Quaternion.identity);
        controller.SetBoard(cubeController.GetBoard());

        return controller;
    }

}
