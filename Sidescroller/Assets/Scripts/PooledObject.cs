using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    Rigidbody2D rb;
    float minXPos;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        minXPos = GameManager.instance.despawnPoint.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < minXPos)
        {
            gameObject.SetActive(false);
        }
    }
}
