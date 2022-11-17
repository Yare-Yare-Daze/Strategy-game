using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : GridPrimitive
{
    private List<Renderer> childRenderers;

    private void Start()
    {
        childRenderers = new List<Renderer>();
        for (int i = 0; i < transform.childCount; i++)
        {
            childRenderers.Add(transform.GetChild(i).GetComponent<Renderer>());
        }
    }

    public void SetTransparent(bool canPlace)
    {
        foreach (Renderer renderer in childRenderers)
        {
            renderer.material.color = canPlace ? Color.green : Color.red;
        }
    }

    public void SetNormalColor()
    {
        foreach(Renderer renderer in childRenderers)
        {
            renderer.material.color = Color.white;
        }
    }


}
