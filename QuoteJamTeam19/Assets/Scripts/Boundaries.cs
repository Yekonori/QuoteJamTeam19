using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;
    private Camera mainCamera;
    private Vector3 viewPos;

    void Start()
    {
        if (mainCamera != Camera.main)
        {
            mainCamera = Camera.main;
            Debug.Log("Camera Switched");
        }
        objectWidth = transform.GetComponentInChildren<SpriteRenderer>().bounds.extents.x; //extents = size of width / 2
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
        if (!GameManager.Instance.hasRing)
        {
            viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
            viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);
        }
        else
        {

        }
        
        transform.position = viewPos;
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(viewPos, screenBounds);
    }
}
