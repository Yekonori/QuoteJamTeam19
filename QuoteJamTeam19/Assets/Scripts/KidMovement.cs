using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidMovement : MonoBehaviour
{
    public GameObject bDest;
    public float speed = 10f;
    public float delayBeforeGoingBack = 1.0f;
    public float randomDelayStrenght = 0.1f;

    private bool isGoingUp;
    private bool canMove = true;
    private Rigidbody2D rb;

    private Vector3 start;
    private Vector3 finish;

    private bool atStart = true;

    private float tolerance;

    public Animator animator;

    void Start()
    {
        start = transform.position;
        finish = bDest.transform.position;

        SetGoUp();

        tolerance = speed * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove)
            return;

        if (atStart)
        {
            if (transform.position != finish)
            {
                Vector3 heading = finish - transform.position;
                transform.position += (heading / heading.magnitude) * speed * Time.deltaTime;

                if (heading.magnitude < tolerance)
                {
                    transform.position = finish;
                    StartCoroutine(Wait());
                }
            }
        }
        else
        {
            if (transform.position != start)
            {
                Vector3 heading = start - transform.position;
                transform.position += (heading / heading.magnitude) * speed * Time.deltaTime;

                if (heading.magnitude < tolerance)
                {
                    transform.position = start;
                    StartCoroutine(Wait());
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canMove = false;
        }
    }

    public IEnumerator Wait()
    {
        float randomDuration = Random.Range(delayBeforeGoingBack - randomDelayStrenght, delayBeforeGoingBack + randomDelayStrenght);

        canMove = false;

        atStart = !atStart;
        SetGoUp();

        yield return new WaitForSeconds(randomDuration);

        canMove = true;
    }

    private void SetGoUp()
    {
        if (atStart)
        {
            if (finish.x > start.x)
            {
                animator.SetBool("GoUp", true);
            }
            else
            {
                animator.SetBool("GoUp", false);
            }
        }
        else
        {
            if (finish.x > start.x)
            {
                animator.SetBool("GoUp", false);
            }
            else
            {
                animator.SetBool("GoUp", true);
            }
        }
    }
}
