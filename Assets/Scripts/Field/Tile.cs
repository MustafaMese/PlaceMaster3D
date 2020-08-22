using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tile : MonoBehaviour
{
    [SerializeField] bool isAvaibleToPut = false;
    [SerializeField] bool isTransparent = false;
    [SerializeField] Material glowMaterial = null;
    [SerializeField] Material normalMaterial = null;
    [SerializeField] Material transparentMaterial = null;
    [SerializeField] MeshRenderer meshRenderer = null;

    [SerializeField] Direction direction = Direction.NONE;

    private void Start() 
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    public void SetGlow()
    {
        if(isAvaibleToPut)
            meshRenderer.material = glowMaterial;
    }

    public void SetIsAvaibleToPut(bool a)
    {
        isAvaibleToPut = a;
    }

    public bool IsTransparent()
    {
        return isTransparent;
    }
    
    public void SetMaterial()
    {
        if (isAvaibleToPut)
            SetGlow();
        else if (isTransparent)
            SetTransparent();
        else
            meshRenderer.material = normalMaterial;
    }

    public void SetTransparent()
    {
        if (isTransparent)
            meshRenderer.material = transparentMaterial;
    }

    public void SetTransparent(bool b)
    {
        isTransparent = b;
    }

    public bool IsAvaibleToPut()
    {
        return isAvaibleToPut;
    }

    public Direction GetDirection()
    {
        return direction;
    }

    public void SetDirection(Direction d)
    {
        direction = d;
    }
}

