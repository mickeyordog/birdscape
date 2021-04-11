using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float scrollSpeed = 3f;
    public bool gameOver = false;
    public Transform despawnPoint;
    int numCoins = 0;
    float distance = 0f;
    public Text coinText;
    public Text distanceText;

    public Transform gameOverScreen;

    public delegate void OnSpeedChange(float newSpeed);
    public OnSpeedChange onSpeedChangeCallback;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one instance of " + GetType());
            Destroy(gameObject);
        }
        instance = this;
    }

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            distance += scrollSpeed * Time.deltaTime;
            distanceText.text = ((int)distance).ToString() + "m";

            IncreaseSpeed(0.1f * Time.deltaTime);
        }
        if (gameOver && Input.GetMouseButtonDown(0))
        {
            //...reload the current scene.
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void PlayerScored()
    {
        if (gameOver)
            return;
    }

    public void EndGame()
    {
        gameOver = true;
        gameOverScreen.gameObject.SetActive(true);
    }

    void IncreaseSpeed(float amount)
    {
        scrollSpeed += amount;
        if (onSpeedChangeCallback != null)
            onSpeedChangeCallback.Invoke(scrollSpeed);
    }

    public void AddCoins(int amount)
    {
        if (amount > 0)
        {
            numCoins += amount;
            coinText.text = numCoins.ToString();
        }
    }
}
