using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    [SerializeField] protected float lifeTime;
    protected float remainingLifeTime;
    [SerializeField] protected float speed;
    [SerializeField] protected float damage;
    protected Vector2 direction;

    protected ProjectilePool projectilePool;
    protected int poolIndex;

    protected InventoryManager inventoryManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected void Update()
    {
        if(remainingLifeTime > 0)
        {
            remainingLifeTime -= Time.deltaTime;
            if (remainingLifeTime <= 0)
            {
                KillProjectile();
            }
        }
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir;
    }

    public void SetPool(ProjectilePool pp, int index)
    {
        projectilePool = pp;
        poolIndex = index;
    }

    public void SetInventoryManager(InventoryManager inv)
    {
        inventoryManager = inv;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyBehaviour enemy = collision.gameObject.GetComponent<EnemyBehaviour>();

        if (enemy != null && !enemy.IsDead())
        {
            enemy.TakeDamage(damage * GetDamageBonus());
            KillProjectile();
        }
    }

    private void KillProjectile()
    {
        projectilePool.ReplaceProjectile(poolIndex, this);
    }

    public void ResetLifeTime()
    {
        remainingLifeTime = lifeTime;
    }

    protected virtual float GetDamageBonus()
    {
        return 1.0f;
    }

    protected virtual float GetSpeedBonus()
    {
        return 1.0f;
    }
}
