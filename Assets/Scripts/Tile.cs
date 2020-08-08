using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] bool glow;
    [SerializeField] Material glowMaterial;
    [SerializeField] Material normalMaterial;

    [SerializeField] MeshRenderer renderer;

    private void Start() 
    {
        renderer = GetComponent<MeshRenderer>();
        SetGlow();
    }

    private void SetGlow()
    {
        if(glow)
            renderer.material = glowMaterial;
        else
            renderer.material = normalMaterial;

    }
}
