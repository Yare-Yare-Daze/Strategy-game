using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float offsetInPixels = 100f;
    public float speed = 5f;
    public float maxZoomIn = 5f;
    public float maxZoomOut = 12f;

    private Camera mainCamera;
    private float screenWidth;
    private float screenHeight;

    void Start()
    {
        mainCamera = Camera.main;
        screenWidth = mainCamera.pixelWidth;
        screenHeight = mainCamera.pixelHeight;
    }

    void Update()
    {
        Vector3 targetCameraPosition = Vector3.zero;
        Vector3 targetCameraZoom = Vector3.zero;

        if(Input.mousePosition.x < (0 + offsetInPixels))
        {
            targetCameraPosition = Vector3.left;
            
        }
        else if(Input.mousePosition.x > (screenWidth - offsetInPixels))
        {
            targetCameraPosition = Vector3.right;
        }
        else if(Input.mousePosition.y > (screenHeight - offsetInPixels))
        {
            targetCameraPosition = Vector3.forward;
        }
        else if(Input.mousePosition.y < (0 + offsetInPixels))
        {
            targetCameraPosition = Vector3.back;
        }

        gameObject.transform.position += targetCameraPosition * speed * Time.deltaTime;

        Debug.Log(Input.mouseScrollDelta.y);

        if(Input.mouseScrollDelta.y == -1 && gameObject.transform.position.y < maxZoomOut)
        {
            targetCameraZoom = Vector3.up;
        }
        else if(Input.mouseScrollDelta.y == 1 && gameObject.transform.position.y > maxZoomIn)
        {
            targetCameraZoom = Vector3.down;
        }

        gameObject.transform.position += targetCameraZoom;

        //if((Input.mouseScrollDelta.y == 1 && gameObject.transform.position.y < maxZoomOut) || (Input.mouseScrollDelta.y == -1 && gameObject.transform.position.y > maxZoomIn))
        //{
        //    gameObject.transform.position += new Vector3(0f, Input.mouseScrollDelta.y, 0f);
        //}

        //if(gameObject.transform.position.y > maxZoomIn && gameObject.transform.position.y < maxZoomOut)
        //{
        //    gameObject.transform.position += new Vector3(0f, Input.mouseScrollDelta.y, 0f);
        //}

    }
}
