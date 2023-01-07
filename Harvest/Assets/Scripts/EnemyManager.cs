using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private EnemyPool enemyPool;
    [SerializeField] private float enemySpawnTime;
    private float nextEnemySpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        enemyPool = gameObject.GetComponent<EnemyPool>();

        nextEnemySpawnTime = enemySpawnTime;

    }

    // Update is called once per frame
    void Update()
    {
        if (nextEnemySpawnTime > 0)
        {
            nextEnemySpawnTime -= Time.deltaTime;
            if (nextEnemySpawnTime <= 0)
            {
                //spawn enemy
                float angleInRadians = Random.Range(0, 360) * Mathf.Deg2Rad;

                enemyPool.GetEnemy(1, new Vector3(Mathf.Cos(angleInRadians) * 10, Mathf.Sin(angleInRadians) * 10, 0));

                nextEnemySpawnTime = enemySpawnTime;
            }
        }
    }

    public HarvestTile GetTileAt(float x, float y)
    {
        return gameObject.GetComponent<FieldManager>().GetTileAt(x, y);
    }

    public HarvestTile FindClosestCrop(Vector3 pos)
    {
        return gameObject.GetComponent<FieldManager>().FindClosestCrop(pos);
    }
}
