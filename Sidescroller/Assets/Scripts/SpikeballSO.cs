using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spikeball", menuName = "Obstacles/Spikeball")]

public class SpikeballSO : ObstacleSO
{
    public float throwForce = 1000f;
    public float torque = 10f;
    public override void AdditionalSpawnBehavior(GameObject spikeball)
    {
        var rb = spikeball.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(-1f, Random.Range(-0.5f, 1f)).normalized * throwForce);
        rb.AddTorque(torque);
    }
}
