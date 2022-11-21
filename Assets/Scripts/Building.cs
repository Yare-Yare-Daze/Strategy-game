using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : GridPrimitive
{
    private Renderer _renderer;
    private List<Color> _originColors;

    private void Awake()
    {
        _renderer = GetComponentInChildren<Renderer>();
        _originColors = new List<Color>();
        foreach (var material in _renderer.materials)
        {
            _originColors.Add(material.color);
            
        }
    }

    public void SetTransparent(bool canPlace)
    {
        foreach (var material in _renderer.materials)
        {
            material.color = canPlace ? Color.green : Color.red;
        }
    }

    public void SetNormalColor()
    {
        for (int i = 0; i < _renderer.materials.Length; i++)
        {
            
            _renderer.materials[i].color = _originColors[i];
        }
    }


}
