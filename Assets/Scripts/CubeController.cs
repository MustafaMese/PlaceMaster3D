using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CubeController : MonoBehaviour
{
    [SerializeField] float fallTime = 0f;
    [SerializeField] Ease fallEase;
    private Board board;

    void Start()
    {
        DOTween.Init();
    }

    public void Fall(Vector3 target)
    {
        Direction d = GetDirection(target);
        Vector3 nextPosition = GetNextPosition(d);

        if(nextPosition != Vector3.zero)
            transform.DOMove(target, fallTime).SetEase(fallEase).OnComplete(() => Move(nextPosition));
        else
            print("Boşluğa atlayacaksın");
    }

    private void Move(Vector3 target)
    {
        Direction d = GetDirection(target);
        Vector3 nextPosition = GetNextPosition(d);

        if(nextPosition != Vector3.zero)
            transform.DOJump(target, 1f, 1f, 1f).OnComplete(() => Move(nextPosition));
        else
            print("Boşluğa atlayacaksın");
    }

    public void SetBoard(Board board)
    {
        this.board = board;
    }

    private Direction GetDirection(Vector3 target)
    {
        Vector3 tilePos = new Vector3(target.x, target.y - 1f, target.z);
        Tile t = board.GetTile(tilePos);
        if(t != null)
        {
            Direction d = t.GetDirection();
            return d;
        }
        else
            return Direction.NONE;
    }

    private Vector3 GetNextPosition(Direction d)
    {
        Vector3 currentPosition = transform.position;
        Vector3 nextPosition = Vector3.zero;
        switch(d)
        {
            case Direction.NONE:
                break;
            case Direction.FORWARD:
                nextPosition = new Vector3(currentPosition.x, currentPosition.y, currentPosition.z + 1f);
                break;
            case Direction.BACKWARD:
                nextPosition = new Vector3(currentPosition.x, currentPosition.y, currentPosition.z - 1f);
                break;
            case Direction.RIGHT:
                nextPosition = new Vector3(currentPosition.x + 1f, currentPosition.y, currentPosition.z);
                break;
            case Direction.LEFT:
                nextPosition = new Vector3(currentPosition.x - 1f, currentPosition.y, currentPosition.z);
                break;
        }

        return nextPosition;
    }
}
