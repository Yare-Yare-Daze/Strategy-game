using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPrimitive : MonoBehaviour
{
    [Header("GridPrimitive: Origin class for grid objects")]
    public Vector2Int size = Vector2Int.one;

    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                Gizmos.color = new Color(0f, 1f, 0f, 0.5f);
                Gizmos.DrawCube(transform.position + new Vector3(i, -0.4f, j), new Vector3(1, 0.2f, 1));
            }
        }
    }
}
