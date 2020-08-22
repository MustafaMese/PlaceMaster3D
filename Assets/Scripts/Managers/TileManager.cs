using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    private Board board;
    private List<TileTable> tiles = new List<TileTable>();

    private void Start()
    {
        board = FindObjectOfType<Board>();
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
            StartCoroutine(EndLevel());
    }

    private IEnumerator EndLevel()
    {
        GameManager.Instance.SetGameState(GameState.END);
        print("End level panel shows up!!");
        yield return new WaitForSeconds(1f);
        print("To the next Level!");
        LoadManager.Instance.NextLevel();

    }
}
