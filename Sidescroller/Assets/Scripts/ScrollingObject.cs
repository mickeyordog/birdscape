using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    Rigidbody2D rb;
    [Range(0, 10)]
    public float multipleOfGameSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-GameManager.instance.scrollSpeed, 0f) * multipleOfGameSpeed;

        GameManager.instance.onSpeedChangeCallback += UpdateSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gameOver == true)
        {
            rb.velocity = Vector2.zero;
        }
    }

    void UpdateSpeed(float newSpeed)
    {
        rb.velocity = new Vector2(-newSpeed, 0f) * multipleOfGameSpeed;
    }
}
