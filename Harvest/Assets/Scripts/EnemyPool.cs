using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public EnemyBehaviour[] enemyPrefabs;

    private List<EnemyBehaviour>[] enemyPools;

    // Start is called before the first frame update
    void Start()
    {
        enemyPools = new List<EnemyBehaviour>[enemyPrefabs.Length];

        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            enemyPools[i] = new List<EnemyBehaviour>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public EnemyBehaviour GetEnemy(int index, Vector3 pos)
    {
        EnemyBehaviour enemy;
        if (enemyPools[index].Count == 0)
        {
            Debug.Log("Create an enemy");
            enemy = Instantiate(enemyPrefabs[index], pos, Quaternion.identity);
            enemy.SetEnemyManager(gameObject.GetComponent<EnemyManager>());
            enemy.SetEnemyPool(this, index);
        }
        else
        {
            enemy = enemyPools[index][0];
            enemy.gameObject.transform.position = pos;
            enemy.gameObject.SetActive(true);
            enemyPools[index].Remove(enemy);
        }

        enemy.ResetHealth();
        return enemy;
    }

    public void ReplaceEnemy(int index, EnemyBehaviour enemy)
    {
        enemy.gameObject.SetActive(false);
        enemyPools[index].Add(enemy);
    }
}
