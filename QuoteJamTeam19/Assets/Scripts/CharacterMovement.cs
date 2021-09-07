using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class CharacterMovement : MonoBehaviour
{
    private BoxCollider2D boxcollider;
    private Rigidbody2D rigidB;
    private float moveY;
    private Vector2 movement;

    public float verticalSpeed;
    public float horizontalSpeed;
    public bool canRun;

    private void Start()
    {
        boxcollider = GetComponent<BoxCollider2D>();
        rigidB = GetComponent<Rigidbody2D>();
        canRun = true;
    }

    private void Update()
    {
        if (canRun)
        {
            moveY = Input.GetAxis("Vertical");
            movement = new Vector2(horizontalSpeed, moveY);
        }
    }

    private void FixedUpdate()
    {
        rigidB.velocity = movement * verticalSpeed * Time.fixedDeltaTime;
    }

    public void StopPlayer()
    {
        canRun = false;
        movement = Vector2.zero;
    }
}
