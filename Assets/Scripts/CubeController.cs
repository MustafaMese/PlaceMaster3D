using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CubeController : MonoBehaviour
{
    [SerializeField] float fallTime = 0f;
    [SerializeField] Ease fallEase;

    private Board board;
    private Direction direction;
    private Tile tile;

    void Start()
    {
        DOTween.Init();
    }

    public void Fall(Vector3 target)
    {
        SetTile(target);
        direction = tile.GetDirection();
        Vector3 nextPosition = GetNextPosition(direction);

        if(nextPosition != Vector3.zero)
            transform.DOMove(target, fallTime).SetEase(fallEase).OnComplete(() => Move(nextPosition));
        else
            print("Boşluğa atlayacaksın");
    }

    private void Move(Vector3 target)
    {
        SetTile(target);
        if (tile != null)
        {
            Vector3 nextPosition = GetNextPosition(direction);
            transform.DOJump(target, 1f, 1, 0.5f).OnComplete(() => Move(nextPosition));
        }
        else
        {
            print("Boşluk");
            transform.DOJump(new Vector3(target.x, -1f, target.z), 3f, 1, 1f);
        }
    }

    public void SetBoard(Board board)
    {
        this.board = board;
    }

    private void SetTile(Vector3 target)
    {
        Vector3 tilePos = new Vector3(target.x, 0f, target.z);
        tile = board.GetTile(tilePos);
    }

    private Vector3 GetNextPosition(Direction d)
    {
        Vector3 currentPosition = tile.transform.position;
        Vector3 nextPosition = Vector3.zero;
        switch(d)
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
