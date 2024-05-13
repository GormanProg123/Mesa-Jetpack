using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GordonScript : MonoBehaviour
{
    public Rigidbody2D rb20;
    public float flypower;
    public float gravityScale;
    public static bool GameOver;
    public Animator animator;
    
    bool isJumping = false;

    public int scoreIncrementInterval = 30;
    private int frameCounter = 0;

    private Logic logic;

    public GameObject gameOverScreen;

    void Start()
    {
        rb20 = GetComponent<Rigidbody2D>();
        rb20.gravityScale = gravityScale;

        logic = FindObjectOfType<Logic>();
    }

    void Update()
    {
        if (!GameOver)
        {
            frameCounter++;
            if (frameCounter >= scoreIncrementInterval)
            {
                logic.UpdateScoreText(1);
                frameCounter = 0;
            }

            if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
            {
                Jump();
                animator.SetBool("jump", true);
                
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                isJumping = false;
                animator.SetBool("jump", false);
                
            }
        }

        if (!GameOver && isJumping)
        {
            rb20.velocity = Vector2.up * flypower;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!GameOver)
        {
            if (collision.CompareTag("Coin"))
            {
                Destroy(collision.gameObject); 
                logic.UpdateCoinCount(1);
            }
            else if (collision.CompareTag("rocketPrefab") || collision.CompareTag("prefabToSpawn"))
            {
                GameOver = true;
                logic.GameOver();
                rb20.velocity = new Vector2(rb20.velocity.x, -10f);
                rb20.gravityScale = 0f;
                gameOverScreen.SetActive(true);
            }
        }
    }

    void Jump()
    {
        rb20.velocity = Vector2.up * flypower;
        isJumping = true;
    }

    public void AnimationFinished()
    {
        animator.SetBool("down", true);
        
    }
}