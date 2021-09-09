using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidMovement : MonoBehaviour
{
    public GameObject aDest, bDest;
    public float speed = 10f;
    public float delayBeforeGoingBack = 1.0f;
    public float randomDelayStrenght = 0.1f;

    private bool isGoingUp;
    private bool canMove = true;
    private Rigidbody2D rb;
    void Start()
    {
        //transform.position = aDest.transform.position;
        isGoingUp = true;
        rb = gameObject.GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isGoingUp && canMove)
        {
            rb.velocity = Vector2.up * speed * Time.deltaTime;
            //sprite haut
        }
        if(!isGoingUp && canMove)
        {
            //-vector.up
            rb.velocity = Vector2.up * -speed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "A")
        {
            isGoingUp = true;
            StartCoroutine(Wait(delayBeforeGoingBack));
        }

        if(collision.gameObject.name == "B")
        {
            isGoingUp = false;
            StartCoroutine(Wait(delayBeforeGoingBack));
        }
        if (!canMove)
        {
            rb.velocity = Vector2.zero;
        }
    }

    public IEnumerator Wait(float duration)
    {
        float randomDuration = Random.Range(duration - randomDelayStrenght, duration + randomDelayStrenght);
        canMove = false;
        aDest.GetComponent<Collider2D>().enabled = false;
        bDest.GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(randomDuration);
        canMove = true;
        yield return new WaitForSeconds(randomDuration);
        aDest.GetComponent<Collider2D>().enabled = true;
        bDest.GetComponent<Collider2D>().enabled = true;
    }
}
