using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BouncerCube : MonoBehaviour
{
    [SerializeField] float fallTime = 2f;
    [SerializeField] Ease fallEase = Ease.OutBounce;

    [SerializeField] private float jumpPower = 1f;
    [SerializeField] private float moveTime = 1f;
    
    private Board board;
    private Direction direction;

    private Tile tile;
    public Material ownMaterial;

    void Start()
    {
        DOTween.Init();
        if(ownMaterial != null)
            ownMaterial = GetComponent<MeshRenderer>().material;
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

    public void Move(Vector3 target)
    {
        SetTile(target);

        if (tile != null)
        {
            EffectorCube cube = board.GetCube(target);
            if(cube != null)
            {
                cube.Effect(this);
                return;
            }

            bool transparency = tile.IsTransparent();
            if (!transparency)
                MoveToNextPosition(target);
            else
                MoveToFillSpace(target);
        }
        else
            MoveToFallVoid(target);
    }

    private void MoveToFallVoid(Vector3 target)
    {
        Vector3 tilePos = new Vector3(target.x, 0f, target.z);
        transform.DOJump(tilePos, jumpPower, 1, moveTime).OnComplete(() => FallToVoid());
    }

    private void MoveToNextPosition(Vector3 target)
    {
        Vector3 nextPosition = GetNextPosition(direction);
        transform.DOJump(target, jumpPower, 1, moveTime).OnComplete(() => Move(nextPosition));
    }

    private void MoveToFillSpace(Vector3 target)
    {
        Vector3 tilePos = new Vector3(target.x, 0f, target.z);
        transform.DOJump(tilePos, jumpPower, 1, moveTime).OnComplete(() => FillSpace());
    }

    private void FallToVoid()
    {
        Tile t = gameObject.AddComponent<Tile>();
        board.AddToTiles(t);
    }
    
    private void FillSpace()
    {
        tile.SetTransparent(false);
        tile.GetComponent<MeshRenderer>().material = GetComponent<MeshRenderer>().material;
        this.gameObject.SetActive(false);
    }
    
    public void SetBoard(Board board)
    {
        this.board = board;
    }

    public void SetTile(Vector3 target)
    {
        Vector3 tilePos = new Vector3(target.x, 0f, target.z);
        tile = board.GetTile(tilePos);
    }

    public Vector3 GetNextPosition(Direction d)
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

    public Vector3 GetOldPosition(Direction d)
    {
        Vector3 currentPosition = tile.transform.position;
        Vector3 nextPosition = Vector3.zero;
        switch (d)
        {
            case Direction.NONE:
                break;
            case Direction.FORWARD:
                nextPosition = new Vector3(currentPosition.x, 1f, currentPosition.z - 1f);
                break;
            case Direction.BACKWARD:
                nextPosition = new Vector3(currentPosition.x, 1f, currentPosition.z + 1f);
                break;
            case Direction.RIGHT:
                nextPosition = new Vector3(currentPosition.x - 1f, 1f, currentPosition.z);
                break;
            case Direction.LEFT:
                nextPosition = new Vector3(currentPosition.x + 1f, 1f, currentPosition.z);
                break;
        }

        return nextPosition;
    }

    public void SetDirection(Direction d)
    {
        direction = d;
    }

    public Direction GetDirection()
    {
        return direction;
    }

    public Tile GetTile()
    {
        return tile;
    }

    public Board GetBoard()
    {
        return board;
    }
}
