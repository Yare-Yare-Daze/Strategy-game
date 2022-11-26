using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsManager : MonoBehaviour
{
    public Vector2Int gridSize = new Vector2Int(10, 10);

    private GridPrimitive[,] objectsGrid;
    private Building selectedBuild;

    private Vector3 mouseReferencePos = Vector3.zero;
    private bool needRotate = false;

    private void Awake()
    {
        objectsGrid = new GridPrimitive[gridSize.x, gridSize.y];
    }

    public void StartPlaceBuilding(Building prefab)
    {
        if (selectedBuild != null)
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

                if (needRotate)
                {
                    Vector3 mouseOffset = Input.mousePosition - mouseReferencePos;
                    Vector3 rotation = new Vector3(0f, -(mouseOffset.x + mouseOffset.y), 0f);
                    selectedBuild.transform.Rotate(rotation);
                    mouseReferencePos = Input.mousePosition;
                }
                else
                {
                    
                    selectedBuild.transform.position = new Vector3(xOnGrid, worldPos.y, yOnGrid);
                    selectedBuild.SetTransparent(canPlace);
                }

                if (!needRotate && canPlace && Input.GetMouseButtonDown(0))
                {
                    needRotate = true;

                    mouseReferencePos = Input.mousePosition;
                    PlaceSelectedBuildOnGrid(xOnGrid, yOnGrid);
                }

                if(needRotate && Input.GetMouseButtonUp(0))
                {
                    RefrashSelectedBuild();
                    needRotate = false;
                }


                /*if (canPlace && Input.GetMouseButtonDown(0))
                {
                    Debug.Log("Invoked GetMouseButtonDown");
                    //PlaceSelectedBuild(xOnGrid, yOnGrid);
                    needRotate = true;

                    mouseReferencePos = Input.mousePosition;
                    PlaceSelectedBuildOnGrid(xOnGrid, yOnGrid);

                }

                if (canPlace && Input.GetMouseButtonUp(0))
                {
                    Debug.Log("Invoked GetMouseButtonUp");
                    RefrashSelectedBuild();
                    needRotate = false;
                }*/

            }
        }
    }

    private bool IsPlaceTaken(int x, int y)
    {
        for (int i = 0; i < selectedBuild.size.x; i++)
        {
            for (int j = 0; j < selectedBuild.size.y; j++)
            {
                if (objectsGrid[x + i, y + j] != null) return true;
            }
        }

        return false;
    }

    private void PlaceSelectedBuildOnGrid(int x, int y)
    {
        for (int i = 0; i < selectedBuild.size.x; i++)
        {
            for (int j = 0; j < selectedBuild.size.y; j++)
            {
                objectsGrid[x + i, y + j] = selectedBuild;
            }
        }
    }

    private void RefrashSelectedBuild()
    {
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
