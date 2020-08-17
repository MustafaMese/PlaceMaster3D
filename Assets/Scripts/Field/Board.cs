using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static Board Instance = null;
    [SerializeField] BouncerCube bouncerCubePrefab;
    private BouncerCube cubeController;
    public List<TileTable> tiles = new List<TileTable>();
    public List<CubeTable> cubes = new List<CubeTable>();
    public bool isTouched = false;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InitializeTiles();
        InitializeCubes();
    }

    private void Update() 
    {
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
                        cubeController = InitializePlayerCube(tile.transform.position);
                        StartMove();
                    }
                }
            }
        }
    }

    private void StartMove()
    {
        Vector3 cubeStartPoint = cubeController.transform.position;
        Vector3 cubeEndPoint = new Vector3(cubeStartPoint.x, cubeStartPoint.y - 4f, cubeStartPoint.z);
        cubeController.Fall(cubeEndPoint);
    }

    private BouncerCube InitializePlayerCube(Vector3 tilePosition)
    {
        BouncerCube controller;
        Vector3 cubeStartPoint = new Vector3(tilePosition.x, tilePosition.y + 5f, tilePosition.z);
        controller = Instantiate(bouncerCubePrefab, cubeStartPoint, Quaternion.identity);
        controller.SetBoard(this);
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
            CubeTable ct = new CubeTable(cube, cubeT.position);
            cubes.Add(ct);
        }
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
        Tile tile = Instantiate(tilePrefab, currentPos, Quaternion.identity, transform);
        AddToTiles(tile);
    }

    public void AddToTiles(Tile tile)
    {
        TileTable t = new TileTable(tile, tile.transform.position);
        tiles.Add(t);
    }

    public EffectorCube GetCube(Vector3 pos)
    {
        for (int i = 0; i < cubes.Count; i++)
        {
            if (cubes[i].GetPosition() == pos)
                return cubes[i].GetCube();
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
