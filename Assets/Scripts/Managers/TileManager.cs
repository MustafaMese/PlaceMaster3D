using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public static TileManager Instance = null;

    [SerializeField] Board board;
    private List<TileTable> tiles = new List<TileTable>();

    public Stack<BouncerCube> cubes = new Stack<BouncerCube>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        tiles = board.tiles;
    }
    
    public void TileControl()
    {
        int count = 0;
        for (int i = 0; i < tiles.Count; i++)
        {
            if (tiles[i].GetTile().IsTransparent())
                count++;
        }

        if (count <= 0)
        {
            EndLevel();
            return;
        }

        if (board.moveCount <= 0 && GetCubeCount() <= 0)
            Fail();
    }

    public int GetCubeCount()
    {
        return cubes.Count;
    }

    public void PushCube(BouncerCube gameObject)
    {
        cubes.Push(gameObject);
    }

    public void PopCube()
    {
        cubes.Pop();
    }

    private void Fail()
    {
        GameManager.Instance.SetGameState(GameState.FAIL);
    }

    private void EndLevel()
    {
        GameManager.Instance.SetGameState(GameState.END);
    }
}
