using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Column", menuName = "Obstacles/Column")]
public class ColumnsSO : ObstacleSO
{
    [SerializeField]
    float columnMinY;
    [SerializeField]
    float columnMaxY;
    public override void AdditionalSpawnBehavior(GameObject column)
    {
        column.transform.position = new Vector2(column.transform.position.x, Random.Range(columnMinY, columnMaxY));
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
