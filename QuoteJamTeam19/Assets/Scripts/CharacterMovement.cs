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
    private bool isSlowed;

    public float verticalSpeed;
    public float horizontalSpeed;
    public bool canRun;

    private void Start()
    {
        boxcollider = GetComponent<BoxCollider2D>();
        rigidB = GetComponent<Rigidbody2D>();
        playerImg = GetComponentInChildren<SpriteRenderer>();
        canRun = true;
        isSlowed = false;
    }

    private void Update()
    {
        if (canRun)
        {
            moveY = Input.GetAxis("Vertical");

            if (!isSlowed)
            {
                movement = new Vector2(horizontalSpeed, moveY);
            }
            else
            {
                Slowed(100, 0.8f);
            }
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
            Destroy(collision.gameObject);
            StartCoroutine(Blinker());
            isSlowed = true;
        }
    }


    private void Slowed(int duration, float slowAmountPercent)
    {
        float timer = 0;
        while(timer <= duration)
        {
            movement = new Vector2(horizontalSpeed - (horizontalSpeed * slowAmountPercent), verticalSpeed - (verticalSpeed * slowAmountPercent));
            timer += Time.deltaTime;
        }
        isSlowed = false;
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
