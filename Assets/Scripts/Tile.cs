using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tile : MonoBehaviour
{
    [SerializeField] bool isAvaibleToPut;
    [SerializeField] bool isTransparent;
    [SerializeField] Material glowMaterial;
    [SerializeField] Material normalMaterial;
    [SerializeField] Material transparentMaterial;
    [SerializeField] MeshRenderer renderer;

    [SerializeField] Direction direction = Direction.NONE;

    private void Start() 
    {
        renderer = GetComponent<MeshRenderer>();
        SetGlow();
        SetTransparent();
    }
    public void SetGlow()
    {
        if(isAvaibleToPut)
            renderer.material = glowMaterial;
        else
            renderer.material = normalMaterial;
    }

    public bool IsTransparent()
    {
        return isTransparent;
    }
    
    public void SetTransparent()
    {
        if (isTransparent)
            renderer.material = transparentMaterial;
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

