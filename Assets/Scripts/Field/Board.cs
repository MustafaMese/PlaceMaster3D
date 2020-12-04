using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static Board Instance = null;
    
    public List<TileTable> tiles = new List<TileTable>();
    public List<EffectorCubeTable> effectorCubes = new List<EffectorCubeTable>();
    public List<BouncerCubeTable> bouncerCubes = new List<BouncerCubeTable>();

    [Header("Board Variables")]
    private bool isTouched = false;
    private BouncerCube cubeController;
    [SerializeField] BouncerCube bouncerCubePrefab;

    public int moveCount = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InitializeTiles();
        InitializeCubes();
        UpdateMoveCount(false);
    }

    private void Update() 
    {
        if (GameManager.Instance.gameState != GameState.PLAY) return;

        if (moveCount <= 0) return;

        if (Input.GetMouseButtonDown(0) && !isTouched)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.transform.CompareTag("Tile"))
                {
                    Tile tile = hit.transform.GetComponent<Tile>();
                    if(tile.IsAvaibleToPut())
                    {
                        isTouched = true;
                        tile.SetIsAvaibleToPut(false);
                        tile.SetMaterial();

                        cubeController = InitializeBouncerCube(tile.transform.position);
                        StartMove();
                        StartCoroutine(MakeTouchedFalse());

                        UpdateMoveCount(true);
                    }
                }
            }
        }
    }

    private void UpdateMoveCount(bool decreaseCount)
    {
        if(decreaseCount)
            moveCount--;
        UIManager.Instance.UpdateMoveCountText(moveCount);
    }

    private IEnumerator MakeTouchedFalse()
    {
        yield return new WaitForSeconds(1.5f);
        isTouched = false;
    }

    private void StartMove()
    {
        Vector3 cubeStartPoint = cubeController.transform.position;
        Vector3 cubeEndPoint = new Vector3(cubeStartPoint.x, cubeStartPoint.y - 4f, cubeStartPoint.z);
        cubeController.Fall(cubeEndPoint);
    }

    private BouncerCube InitializeBouncerCube(Vector3 tilePosition)
    {
        BouncerCube controller;
        Vector3 cubeStartPoint = new Vector3(tilePosition.x, tilePosition.y + 5f, tilePosition.z);
        controller = Instantiate(bouncerCubePrefab, cubeStartPoint, Quaternion.identity);
        controller.SetBoard(this);
        AddToBouncerCubes(controller, controller.transform.position);
        return controller;
    }

    private void InitializeCubes()
    {
        List<EffectorCube> c = new List<EffectorCube>( FindObjectsOfType<EffectorCube>());
        for (int i = 0; i < c.Count; i++)
        {
            EffectorCube cube = c[i];
            Transform cubeT = cube.transform;
            cubeT.SetParent(this.transform);
            AddToEffectorCubes(cube, cubeT.position);
        }
    }

    public void UpdateBouncerCubes(BouncerCube cube, Vector3 cubePos)
    {
        for (int i = 0; i < bouncerCubes.Count; i++)
        {
            if (bouncerCubes[i].GetCube() == cube)
            {
                bouncerCubes[i].SetPosition(cubePos);
            }
        }
    }

    public void AddToBouncerCubes(BouncerCube cube, Vector3 cubePos)
    {
        BouncerCubeTable ct = new BouncerCubeTable(cube, cubePos);
        bouncerCubes.Add(ct);
    }

    public void AddToEffectorCubes(EffectorCube cube, Vector3 cubePos)
    {
        EffectorCubeTable ct = new EffectorCubeTable(cube, cubePos);
        effectorCubes.Add(ct);
    }

    private void InitializeTiles()
    {
        var count = transform.childCount;
        for(int i = 0; i < count; i++)
        {
            Tile t = transform.GetChild(i).GetComponent<Tile>();;
            TileTable tt = new TileTable(t, t.transform.position);
            tiles.Add(tt);
        }
    }

    public void CreateTile(Vector3 currentPos, Tile tilePrefab)
    {
        var tile = PrefabUtility.InstantiatePrefab(tilePrefab as Tile) as Tile;
        tile.transform.position = currentPos;
        tile.transform.SetParent(transform);
        AddToTiles(tile);
    }

    public void AddToTiles(Tile tile)
    {
        TileTable t = new TileTable(tile, tile.transform.position);
        tiles.Add(t);
    }

    public BouncerCube GetBouncerCube(Vector3 pos)
    {
        for (int i = 0; i < bouncerCubes.Count; i++)
        {
            if (bouncerCubes[i].GetPosition() == pos)
                return bouncerCubes[i].GetCube();
        }
        return null;
    }

    public EffectorCube GetEffectorCube(Vector3 pos)
    {
        for (int i = 0; i < effectorCubes.Count; i++)
        {
            if (effectorCubes[i].GetPosition() == pos)
                return effectorCubes[i].GetCube();
        }
        return null;
    }

    public Tile GetTile(Vector3 pos)
    {
        for(int i = 0; i < tiles.Count; i++)
        {
            if(tiles[i].GetPosition() == pos)
                return tiles[i].GetTile(); 
        }
        return null;
    }
}
