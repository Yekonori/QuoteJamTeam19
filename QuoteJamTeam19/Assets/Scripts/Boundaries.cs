using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    private Vector2 screenBounds;
    private float objectHeight;
    private Camera mainCamera;
    private Vector3 viewPos;

    void Start()
    {
        objectHeight = transform.GetComponentInChildren<SpriteRenderer>().bounds.extents.y; //extents = size of height / 2
    }

    void LateUpdate()
    {
        if(mainCamera != Camera.main)
        {
            mainCamera = Camera.main;
            Debug.Log("Camera Switched");
        }
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        viewPos = transform.position;
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);
        transform.position = viewPos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(viewPos, screenBounds);
    }
}
