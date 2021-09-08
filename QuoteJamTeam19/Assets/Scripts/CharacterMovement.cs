using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class CharacterMovement : MonoBehaviour
{
    private BoxCollider2D boxcollider;
    private Rigidbody2D rigidB;
    private float verticalSpeed;
    private Vector2 movement;
    private SpriteRenderer playerImg;
    private bool isSlowed;
    private float slowSpeedValue;
    private Animator animator;

    public float playerSpeed;
    public float horizontalSpeed;
    public bool canRun;

    private void Start()
    {
        GameManager.Instance.player = this;
        DignityBar.Instance.player = this;

        boxcollider = GetComponent<BoxCollider2D>();
        rigidB = GetComponent<Rigidbody2D>();
        playerImg = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        canRun = true;
        isSlowed = false;

        //SetDirty(DignityBar.Instance.GetDignityValue());
    }

    private void Update()
    {
        if (canRun)
        {
            animator.SetBool("Running", true);
            verticalSpeed = Input.GetAxis("Vertical") * playerSpeed;
            if (GameManager.Instance.hasRing)
            {
                if (!isSlowed)
                {
                    movement = new Vector2(-horizontalSpeed, verticalSpeed);
                }
                else
                {
                    movement = new Vector2(-horizontalSpeed + (horizontalSpeed * slowSpeedValue), verticalSpeed - (verticalSpeed * slowSpeedValue));
                }
            }
                
            else
            {
                    if (!isSlowed)
                    {
                        movement = new Vector2(horizontalSpeed, verticalSpeed);
                    }
                    else
                    {
                        movement = new Vector2(horizontalSpeed - (horizontalSpeed * slowSpeedValue), verticalSpeed - (verticalSpeed * slowSpeedValue));
                    }
                }
        }
        else
        {
            animator.SetBool("Running", false);
        }
    }

    private void FixedUpdate()
    {
        if (canRun)
        {
            rigidB.velocity = movement * playerSpeed * Time.fixedDeltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "obstacle")
        {
            CommonObstacle commonObstacle = collision.gameObject.GetComponent<CommonObstacle>();
            Destroy(collision.gameObject);
            StartCoroutine(Blinker());

            if (commonObstacle != null)
            {
                slowSpeedValue = commonObstacle.slowEffect;
                StartCoroutine(SetIsSlowed(commonObstacle.slowDuration));
                DignityBar.Instance.ReduceDignity(commonObstacle.dignityDamage);
            }
            else
            {
                slowSpeedValue = 0.5f;
                StartCoroutine(SetIsSlowed(1.5f));
                DignityBar.Instance.ReduceDignity(5f);
            }            
        }

        if(collision.gameObject.tag == "npc")
        {
            StopPlayer();

            NPCObstacle npc = collision.gameObject.GetComponent<NPCObstacle>();

            if (npc)
            {
                npc.TriggerDialogue();
            }
        }

        if(collision.gameObject.tag == "ring")
        {
            GameManager.Instance.GetRing();
        }
    }

    private IEnumerator SetIsSlowed(float duration)
    {
        isSlowed = true;

        yield return new WaitForSeconds(duration);

        isSlowed = false;
    }

    private IEnumerator Blinker()
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

    public void StopPlayer()
    {
        canRun = false;
        rigidB.velocity = Vector2.zero;
    }

    public void RestartPlayer()
    {
        canRun = true;
    }

    public void SetDirty(float dignityAmount)
    {
        animator.SetFloat("Dirty", 1 - dignityAmount / 100);
    }
}
