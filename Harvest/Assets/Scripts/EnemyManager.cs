using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private EnemyPool enemyPool;
    [SerializeField] private float enemySpawnTime;
    private float nextEnemySpawnTime;

    private float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0;
        
        enemyPool = gameObject.GetComponent<EnemyPool>();

        nextEnemySpawnTime = enemySpawnTime;

    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (nextEnemySpawnTime > 0)
        {
            nextEnemySpawnTime -= Time.deltaTime;
            if (nextEnemySpawnTime <= 0)
            {
                //spawn a crow
                SpawnEnemy(0);

                if (elapsedTime > 300.0f)
                {
                    //spawn 2 crows and a bunny
                    SpawnEnemy(0);
                    SpawnEnemy(0);
                    SpawnEnemy(1);

                    //50% chance to spawn a groundhog
                    if (Random.Range(0, 2) == 1)
                    {
                        SpawnEnemy(2);
                    }
                }
                else if (elapsedTime > 240.0f)
                {
                    //spawn 2 crows and a bunny
                    SpawnEnemy(0);
                    SpawnEnemy(0);
                    SpawnEnemy(1);
                }
                else if (elapsedTime > 180.0f)
                {
                    //spawn a bunny and a crow
                    SpawnEnemy(0);
                    SpawnEnemy(1);
                }
                else if (elapsedTime > 120.0f)
                {
                    //spawn a bunny
                    SpawnEnemy(1);
                }
                else if (elapsedTime > 60.0f)
                {
                    //spawn a second crow
                    SpawnEnemy(0);
                }

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

    public void SpawnExperiencePickups(Vector3 pos, int xp)
    {
        gameObject.GetComponent<PickupManager>().SpawnExeriencePickups(pos, xp);
    }

    public float GetElapsedTime()
    {
        return elapsedTime;
    }

    private void SpawnEnemy(int enemyIndex)
    {
        //spawn enemy
        float angleInRadians = Random.Range(0, 360) * Mathf.Deg2Rad;

        enemyPool.GetEnemy(0, new Vector3(Mathf.Cos(angleInRadians) * 10, Mathf.Sin(angleInRadians) * 10, 0));
    }
}
