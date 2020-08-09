using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CubeController : MonoBehaviour
{
    [SerializeField] float fallTime = 0f;
    [SerializeField] Ease fallEase;

    [SerializeField] private float jumpPower = 1f;
    [SerializeField] private float moveTime = 1f;
    
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
            transform.DOMove(target, fallTime).SetEase(fallEase).OnComplete(() => FallToVoid());
    }

    private void Move(Vector3 target)
    {
        SetTile(target);
        if (tile != null)
        {
            bool transparency = tile.IsTransparent();
            if (!transparency)
            {
                // Diğer pozisyona ilerle.
                Vector3 nextPosition = GetNextPosition(direction);
                transform.DOJump(target, jumpPower, 1, moveTime).OnComplete(() => Move(nextPosition));
            }
            else
            {
                // Boşluğa yerleş.
                Vector3 tilePos = new Vector3(target.x, 0f, target.z);
                transform.DOJump(tilePos, jumpPower, 1, moveTime).OnComplete(() => FillSpace());
                // Yerleştin.
            }
        }
        else
        {
            Vector3 tilePos = new Vector3(target.x, 0f, target.z);
            transform.DOJump(tilePos, jumpPower, 1, moveTime).OnComplete(() => FallToVoid());
        }
    }

    private void FallToVoid()
    {
        print("Boşluğa Düştün");
    }
    
    private void FillSpace()
    {
        print("Boşluğa yerleştin");
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
