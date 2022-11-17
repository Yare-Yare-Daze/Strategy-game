using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsManager : MonoBehaviour
{
    public Vector2Int gridSize = new Vector2Int(10, 10);

    private Building[,] buildingGrid;
    private Building selectedBuild;

    private void Awake()
    {
        buildingGrid = new Building[gridSize.x, gridSize.y];
    }

    public void StartPlaceBuilding(Building prefab)
    {
        if(selectedBuild != null)
        {
            Destroy(selectedBuild.gameObject);
        }

        selectedBuild = Instantiate(prefab);
    }

    private void LateUpdate()
    {
        if (selectedBuild != null)
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (plane.Raycast(ray, out float position))
            {
                Vector3 worldPos = ray.GetPoint(position);

                int xOnGrid = Mathf.RoundToInt(worldPos.x);
                int yOnGrid = Mathf.RoundToInt(worldPos.z);

                bool canPlace = true;

                if (xOnGrid < 0 || xOnGrid > gridSize.x - selectedBuild.size.x) canPlace = false;
                if (yOnGrid < 0 || yOnGrid > gridSize.y - selectedBuild.size.y) canPlace = false;

                if (canPlace && IsPlaceTaken(xOnGrid, yOnGrid)) canPlace = false;


                selectedBuild.transform.position = new Vector3(xOnGrid, worldPos.y, yOnGrid) + new Vector3(0f, 0.4f, 0f);
                selectedBuild.SetTransparent(canPlace);

                if (canPlace && Input.GetMouseButtonDown(0))
                {
                    PlaceSelectedBuild(xOnGrid, yOnGrid);
                }
            }
        }
    }

    private bool IsPlaceTaken(int x, int y)
    {
        for (int i = 0; i < selectedBuild.size.x; i++)
        {
            for (int j = 0; j < selectedBuild.size.y; j++)
            {
                if (buildingGrid[x + i, y + j] != null) return true;
            }
        }

        return false;
    }

    private void PlaceSelectedBuild(int x, int y)
    {
        for (int i = 0; i < selectedBuild.size.x; i++)
        {
            for (int j = 0; j < selectedBuild.size.y; j++)
            {
                buildingGrid[x + i, y + j] = selectedBuild;
            }
        }

        selectedBuild.SetNormalColor();
        selectedBuild = null;
    }

    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                Gizmos.color = new Color(0f, 0f, 1f, 0.5f);
                Gizmos.DrawCube(transform.position + new Vector3(i, -0.4f, j), new Vector3(1, 0.2f, 1));
            }
        }
    }
}
