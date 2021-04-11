using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObstacleSO : ScriptableObject
{
    public GameObject prefab;
    public int poolSize = 5;
    public float distanceBetweenSpawns = 9f;
    public int numPerWave = 5;

    public abstract void AdditionalSpawnBehavior(GameObject obstacle);
}
