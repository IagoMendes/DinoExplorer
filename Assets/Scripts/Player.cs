//using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    AudioSource jump;
    SpriteRenderer sprite;
    public GameManager gameManager;

    public float lives;

    private bool isFacingRight;
    private bool isJumping;
    private bool isDead;
    private bool canShoot;

    private float speed = 3.0f;
    private float scale = 0.5f;

    public GameObject rightBullet;
    public GameObject leftBullet;
    Vector3 position;
    public float wait = 0.4f;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        lives = 3f;
        isFacingRight = true;
        isJumping = false;
        isDead = false;
        canShoot = false;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        jump = GetComponent<AudioSource>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
                isFacingRight = true;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
                isFacingRight = false;
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }

            if (Input.GetKeyDown(KeyCode.Space) && (!isJumping))
            {
                jump.Play();
                rb.AddForce(Vector2.up * 350);
                isJumping = true;
            }

            timer += Time.deltaTime;
            if (timer > wait && Input.GetKey(KeyCode.Q) && canShoot)
            {
                timer = 0;

                if (isFacingRight)
                {
                    position = new Vector3(gameObject.transform.position.x + 0.2f, gameObject.transform.position.y + 0.6f, 0);
                    Instantiate(rightBullet, position, Quaternion.identity);
                }

                else
                {
                    position = new Vector3(gameObject.transform.position.x - 0.2f, gameObject.transform.position.y + 0.6f, 0);
                    Instantiate(leftBullet, position, Quaternion.identity);
                }
            }
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
        ControllAnimation();
    }

    void ControllAnimation()
    {
        if (isDead)
        {
            animator.SetBool("Died", true);
        }
        else
        {
            if (rb.velocity.x >= speed || rb.velocity.x <= -speed)
            {
                animator.SetBool("Running", true);
            }
            else if (rb.velocity.x == 0)
            {
                animator.SetBool("Running", false);
            }


            if (rb.velocity.y > 0 && isJumping)
            {
                animator.SetBool("Jumping", true);
            }

            if (isJumping == false)
            {
                animator.SetBool("Jumping", false);
            }

            if (!(isFacingRight))
            {
                transform.localScale = new Vector2(-scale, scale);
            }
            else
            {
                transform.localScale = new Vector2(scale, scale);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJumping = false;
        }

        if (collision.gameObject.tag == "Water")
        {
            isDead = true;
            Invoke("Die", 1f);
        }

        if (collision.gameObject.tag == "JumpMushroom")
        {
            rb.AddForce(Vector2.up * 500);
        }

        if (collision.gameObject.tag == "Checkpoint")
        {
            gameManager.NextStage();
        }

        if (collision.gameObject.tag == "PowerUp")
        {
            StartCoroutine("PowerUp");
            canShoot = true;
            Destroy(collision.gameObject);
        }
    }

    IEnumerator PowerUp()
    {
        for (int i = 0; i < 20; i++)
        {
            if (sprite != null)
            {
                Color rainbow = new Color(Random.value, Random.value, Random.value);
                sprite.color = rainbow;
            }
            yield return new WaitForSeconds(0.1f);
        }

        sprite.color = new Color(255, 255, 255);
    }


    void Die()
    {
        gameManager.Die();
    }

    void OnBecameInvisible()
    {
        lives = 0;
    }
}
