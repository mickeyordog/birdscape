using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    float timeBetweenWaves = 3f;
    float timeOfNextSpawn;

    GameManager gm;

    [SerializeField]
    ObstacleSO[] obstacleSOs;
    List<GameObject[]> objectArrays;

    int currenObstacleSOIndex = 0;
    int currentIndexInCurrentArray = 0;
    int numSpawnedThisWave = 0;
    


    void Start()
    {
        gm = GameManager.instance;
        timeOfNextSpawn = 0f;
        objectArrays = new List<GameObject[]>();

        foreach (ObstacleSO so in obstacleSOs)
        {
            var arr = new GameObject[so.poolSize];
            
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = Instantiate(so.prefab, transform.position, Quaternion.identity) as GameObject;
                arr[i].SetActive(false);
            }
            objectArrays.Add(arr);
        }
        currenObstacleSOIndex = Random.Range(0, obstacleSOs.Length);
    }

    // TODO: make generic object pool
    void Update()
    {
        // TODO: replace with coroutine
        if (gm.gameOver || Time.time < timeOfNextSpawn)
            return;
        SpawnObject();
    }

    private void SpawnObject()
    {

        var currentObstacleSO = obstacleSOs[currenObstacleSOIndex];

        if (numSpawnedThisWave >= currentObstacleSO.numPerWave)
        {
            currenObstacleSOIndex = Random.Range(0, obstacleSOs.Length);
            currentObstacleSO = obstacleSOs[currenObstacleSOIndex];
            numSpawnedThisWave = 0;
            currentIndexInCurrentArray = 0;
            timeOfNextSpawn = Time.time + timeBetweenWaves;
            return;
        }

        float spawnRate = currentObstacleSO.distanceBetweenSpawns / GameManager.instance.scrollSpeed;
        timeOfNextSpawn = Time.time + spawnRate * Random.Range(0.75f, 1.5f);

        GameObject obj = objectArrays[currenObstacleSOIndex][currentIndexInCurrentArray];
        obj.SetActive(true);
        obj.transform.position = transform.position;
        obj.GetComponent<Rigidbody2D>().velocity = new Vector2(-gm.scrollSpeed, 0);

        currentObstacleSO.AdditionalSpawnBehavior(obj);

        currentIndexInCurrentArray++;

        if (currentIndexInCurrentArray >= currentObstacleSO.poolSize)
        {
            currentIndexInCurrentArray = 0;
        }
        numSpawnedThisWave++;
    }


}
