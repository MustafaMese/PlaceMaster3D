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
    private CoinsManager coinsManager;
    private Direction direction;

    public Tile tile;
    public Material ownMaterial;

    void Start()
    {
        DOTween.Init();
        coinsManager = FindObjectOfType<CoinsManager>();
        if(ownMaterial != null)
            ownMaterial = GetComponent<MeshRenderer>().material;
    }

    public void Fall(Vector3 target)
    {
        TileManager.Instance.PushCube(this);
        SetTile(target);
        direction = tile.GetDirection();
        Vector3 nextPosition = GetNextPosition(direction);

        if (nextPosition != Vector3.zero)
        {
            transform.DOMove(target, fallTime).SetEase(fallEase).OnComplete(() => Move(nextPosition));
        }
        else
            transform.DOMove(target, fallTime).SetEase(fallEase).OnComplete(() => FallToVoid());
    }

    public void Move(Vector3 target)
    {
        SetTile(target);
        BouncerCube bc = board.GetBouncerCube(target);

        if (bc != null) return;

        if (tile != null)
        {
            EffectorCube ec = board.GetEffectorCube(target);

            if (ec != null)
                ec.Effect(this);
            else
            {
                bool transparency = tile.IsTransparent();
                if (!transparency)
                    MoveToNextPosition(target);
                else
                    MoveToFillSpace(target);
            }
        }
        else
            MoveToFallVoid(target);
    }

    private void MoveToFallVoid(Vector3 target)
    {
        Vector3 tilePos = new Vector3(target.x, 0f, target.z);
        UpdateCubeTable(target, true);
        transform.DOJump(tilePos, jumpPower, 1, moveTime).OnComplete(() => FallToVoid());
    }

    private void MoveToNextPosition(Vector3 target)
    {
        Vector3 nextPosition = GetNextPosition(direction);
        UpdateCubeTable(target, false);
        transform.DOJump(target, jumpPower, 1, moveTime).OnComplete(() => Move(nextPosition));
    }

    private void MoveToFillSpace(Vector3 target)
    {
        Vector3 tilePos = new Vector3(target.x, 0f, target.z);
        UpdateCubeTable(target, true);
        transform.DOJump(tilePos, jumpPower, 1, moveTime).OnComplete(() => FillSpace());
    }

    private void UpdateCubeTable(Vector3 target, bool placeToTile)
    {
        if (placeToTile) 
        {
            Vector3 pos = new Vector3(target.x, 0f, target.z);
            board.UpdateBouncerCubes(this, pos);
        }
        else
        {
            Vector3 pos = new Vector3(target.x, 1f, target.z);
            board.UpdateBouncerCubes(this, pos);
        }
    }

    private void FallToVoid()
    {
        Tile t = gameObject.AddComponent<Tile>();
        this.enabled = false;
        board.AddToTiles(t);

        TileManager.Instance.PopCube();
        TileManager.Instance.TileControl();
    }
    
    private void FillSpace()
    {
        //coinsManager.AddCoins(transform.position, 7);

        tile.SetTransparent(false);
        tile.GetComponent<MeshRenderer>().material = GetComponent<MeshRenderer>().material;
        this.enabled = false;

        TileManager.Instance.PopCube();
        TileManager.Instance.TileControl();
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
