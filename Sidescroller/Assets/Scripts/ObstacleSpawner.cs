using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{

    public GameObject spikeballPrefab;
    public int spikePoolSize = 5;
    public float distanceBetweenSpikes = 9f;
    private GameObject[] spikes;
    private int currentSpike = 0;
    public float spikeForce = 10f;

    public GameObject columnPrefab;                               
    public int columnPoolSize = 5;
    public float distanceBetweenColumns = 9f;                                 
    public float columnMin = -1f;                                  
    public float columnMax = 3.5f;                           
    private GameObject[] columns;                             
    private int currentColumn = 0;                              

    private float spawnXPosition;

    private float timeOfNextSpawn;
    public SpawningState spawningState = SpawningState.Columns;

    GameManager gm;


    void Start()
    {
        gm = GameManager.instance;
        timeOfNextSpawn = 0f;
        spawnXPosition = transform.position.x;

        columns = new GameObject[columnPoolSize];
        spikes = new GameObject[spikePoolSize];
        for (int i = 0; i < columnPoolSize; i++)
        {
            columns[i] = (GameObject)Instantiate(columnPrefab, transform.position, Quaternion.identity);
            columns[i].SetActive(false);
        }
        for (int i = 0; i < spikePoolSize; i++)
        {
            spikes[i] = (GameObject)Instantiate(spikeballPrefab, transform.position, Quaternion.identity);
            spikes[i].SetActive(false);
        }
    }

    // TODO: make generic object pool
    void Update()
    {
        // TODO: replace with coroutine
        if (gm.gameOver || Time.time < timeOfNextSpawn)
            return;
        switch (spawningState)
        {
            case SpawningState.Columns:
                SpawnColumn();
                break;
            case SpawningState.Spikeballs:
                SpawnSpikeballs();
                break;
        }
    }

    private GameObject SpawnObject(GameObject[] objects, ref int currentObject, int poolSize, Vector2 pos, Vector2 velocity)
    {
        float spawnRate = distanceBetweenColumns / GameManager.instance.scrollSpeed;
        timeOfNextSpawn = Time.time + spawnRate * Random.Range(0.75f, 1.5f);

        var obj = objects[currentObject];
        obj.SetActive(true);
        obj.transform.position = pos;
        obj.GetComponent<Rigidbody2D>().velocity = velocity;

        currentObject++;

        if (currentObject >= poolSize)
        {
            currentObject = 0;
        }

        return obj;
    }

    private void SpawnSpikeballs()
    {

        var spike = SpawnObject(spikes, ref currentSpike, spikePoolSize, transform.position, Vector2.zero);
        var rb = spike.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(-1f, Random.Range(-0.5f, 1f)).normalized * spikeForce);
        rb.AddTorque(10f);
    }

    void SpawnColumn()
    {

        var spawnPos = new Vector2(spawnXPosition, Random.Range(columnMin, columnMax));
        var spawnVel = new Vector2(-GameManager.instance.scrollSpeed, 0f);
        var column = SpawnObject(columns, ref currentColumn, columnPoolSize, spawnPos, spawnVel);
        foreach (Transform t in column.transform)
        {
            if (t.tag == "Coin")
            {
                t.gameObject.SetActive(true);
                break;
            }
        }
    }
}

public enum SpawningState
{
    Nothing, Columns, Spikeballs
}
