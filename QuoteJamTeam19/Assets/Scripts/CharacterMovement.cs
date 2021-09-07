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
    private SpriteRenderer playerImg;

    public float verticalSpeed;
    public float horizontalSpeed;
    public bool canRun;

    private void Start()
    {
        boxcollider = GetComponent<BoxCollider2D>();
        rigidB = GetComponent<Rigidbody2D>();
        playerImg = GetComponentInChildren<SpriteRenderer>();
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
        if (canRun)
        {
            rigidB.velocity = movement * verticalSpeed * Time.fixedDeltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "obstacle")
        {
            Debug.Log("Collision!");
            canRun = false;
            rigidB.velocity = Vector2.zero;
            StartCoroutine(Blinker());
        }
    }

    IEnumerator Blinker()
    {
        Color tmp = playerImg.color;

        for (int i = 0; i < 3; i++)
        {
            tmp.a = 0;
            playerImg.color = tmp;
            yield return new WaitForSeconds(0.25f);
            tmp.a = 255;
            playerImg.color = tmp;
            yield return new WaitForSeconds(0.25f);
        }
    }
}
