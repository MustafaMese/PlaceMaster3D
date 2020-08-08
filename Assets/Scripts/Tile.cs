using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] bool isAvaible;
    [SerializeField] Material glowMaterial;
    [SerializeField] Material normalMaterial;
    [SerializeField] MeshRenderer renderer;

    [SerializeField] Direction direction = Direction.NONE;

    private void Start() 
    {
        renderer = GetComponent<MeshRenderer>();
        SetGlow();
    }

    private void SetGlow()
    {
        if(isAvaible)
            renderer.material = glowMaterial;
        else
            renderer.material = normalMaterial;
    }

    public bool IsAvaible()
    {
        return isAvaible;
    }

    public Direction GetDirection()
    {
        return direction;
    }
}

