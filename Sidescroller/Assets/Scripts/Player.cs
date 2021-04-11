using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    public float upForce = 20f;
    Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Die()
    {
        GameManager.instance.EndGame();
        rb.velocity = Vector2.zero;
        GetComponent<Collider2D>().enabled = false;
        rb.freezeRotation = false;
    }


    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && !GameManager.instance.gameOver)
        {
            rb.AddForce(Vector2.up * upForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
            anim.SetBool("isFlapping", true);
            
        }
    }

    private void Update()
    {
        transform.eulerAngles = new Vector3(0f, 0f, rb.velocity.y * 2f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            collision.gameObject.SetActive(false);
            GameManager.instance.AddCoins(1);
        }
    }


}
