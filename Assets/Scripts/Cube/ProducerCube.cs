using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProducerCube : EffectorCube
{
    [SerializeField] Direction direction;
    [SerializeField] BouncerCube bouncerCubePrefab;
    [SerializeField] int maxProduceCount;

    private Board board;

    private int tempProduceCount;

    private void Start()
    {
        tempProduceCount = maxProduceCount;
    }

    public override void Effect(BouncerCube cubeController)
    {
        board = cubeController.GetBoard();

        Vector3 oldPosition = cubeController.GetOldPosition(cubeController.GetDirection());
        cubeController.SetTile(oldPosition);
        cubeController.SetDirection(direction);
        board.UpdateBouncerCubes(cubeController, cubeController.transform.position);

        StartCoroutine(ProduceCube(cubeController));
    }

    private IEnumerator ProduceCube(BouncerCube cubeController)
    {
        while (tempProduceCount > 0)
        {
            Vector3 pos = GetNextPosition();

            EffectorCube ec = board.GetEffectorCube(pos);
            BouncerCube bc = board.GetBouncerCube(pos);

            if (ec == null && bc == null)
            {
                tempProduceCount--;

                BouncerCube cube = InitializeBouncerCube(cubeController);
                SetVariblesOfBouncerCube(cube);
                TileManager.Instance.PushCube(cube);

                yield return new WaitForSeconds(1.2f);
            }
            else
                break;
        }
    }

    private void SetVariblesOfBouncerCube(BouncerCube bouncerCube)
    {
        bouncerCube.SetTile(transform.position);
        bouncerCube.SetDirection(direction);
        Vector3 cubePos = new Vector3(transform.position.x, 1f, transform.position.z);

        board.AddToBouncerCubes(bouncerCube, cubePos);
        Vector3 nextPosition = bouncerCube.GetNextPosition(direction);
        bouncerCube.Move(nextPosition);
    }

    private BouncerCube InitializeBouncerCube(BouncerCube cubeController)
    {
        BouncerCube controller;
        controller = Instantiate(bouncerCubePrefab, transform.position, Quaternion.identity);
        StartCoroutine(ScaleCube(controller));
        controller.SetBoard(cubeController.GetBoard());
        return controller;
    }

    private IEnumerator ScaleCube(BouncerCube cube)
    {
        cube.transform.localScale = Vector3.zero;
        Vector3 v = new Vector3(10, 10, 10);
        while (cube.transform.localScale.x < 10)
        {
            cube.transform.localScale += new Vector3(1, 1, 1) * (Time.deltaTime / 0.2f);
            yield return null;
        }
        cube.transform.localScale = v;
    }

    private Vector3 GetNextPosition()
    {
        Vector3 currentPosition = transform.position;
        Vector3 nextPosition = Vector3.zero;
        switch (direction)
        {
            case Direction.NONE:
                break;
            case Direction.FORWARD:
                nextPosition = new Vector3(currentPosition.x, 1f, currentPosition.z + 1f);
                break;
            case Direction.BACKWARD:
                nextPosition = new Vector3(currentPosition.x, 1f, currentPosition.z - 1f);
                break;
            case Direction.RIGHT:
                nextPosition = new Vector3(currentPosition.x + 1f, 1f, currentPosition.z);
                break;
            case Direction.LEFT:
                nextPosition = new Vector3(currentPosition.x - 1f, 1f, currentPosition.z);
                break;
        }

        return nextPosition;
    }
}
