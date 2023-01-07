using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] protected Animator spriteAnimator;
    [SerializeField] protected float speed;
    [SerializeField] protected float attackRate;
    protected float attackTimer;
    [SerializeField] protected float damage;
    [SerializeField] protected float maxHealth;
    protected float health;
    protected EnemyManager enemyManager;
    protected EnemyPool enemyPool;
    protected int poolIndex;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetEnemyManager(EnemyManager enem)
    {
        enemyManager = enem;
    }

    public void SetEnemyPool(EnemyPool pool, int index)
    {
        enemyPool = pool;
        poolIndex = index;
    }

    private void ReturnToPool()
    {
        enemyPool.ReplaceEnemy(poolIndex, this);
    }

    public void TakeDamage(float d)
    {
        SetHealth(Mathf.Max(0, health - d));
    }

    private void SetHealth(float h)
    {
        health = h;
        if (health <= 0)
        {
            ReturnToPool();
        }
    }

    public void ResetHealth()
    {
        SetHealth(maxHealth);
    }

    public bool IsDead()
    {
        return health <= 0;
    }


}
