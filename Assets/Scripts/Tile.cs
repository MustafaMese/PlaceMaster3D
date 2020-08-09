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
        SetGlow();
        SetTransparent();
    }
    public void SetGlow()
    {
        if(isAvaibleToPut)
            meshRenderer.material = glowMaterial;
        else
            meshRenderer.material = normalMaterial;
    }

    public bool IsTransparent()
    {
        return isTransparent;
    }
    
    public void SetTransparent()
    {
        if (isTransparent)
            meshRenderer.material = transparentMaterial;
    }

    public bool IsAvaibleToPut()
    {
        return isAvaibleToPut;
    }

    public Direction GetDirection()
    {
        return direction;
    }
}

