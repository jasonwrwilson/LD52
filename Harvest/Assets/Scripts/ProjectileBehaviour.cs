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

    [SerializeField] protected Animator spriteAnimator;

    [SerializeField] protected AudioSource fireSound;
    [SerializeField] protected AudioSource collisionSound;

    protected bool isDead = false;

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
            Death();
        }
    }

    private void KillProjectile()
    {
        projectilePool.ReplaceProjectile(poolIndex, this);
    }

    public void ResetLifeTime()
    {
        remainingLifeTime = lifeTime;
        isDead = false;
        spriteAnimator.SetBool("Idling", true);
        spriteAnimator.SetBool("Dead", false);
    }

    protected virtual float GetDamageBonus()
    {
        return 1.0f;
    }

    protected virtual float GetSpeedBonus()
    {
        return 1.0f;
    }

    public void PlayFireSound()
    {
        fireSound.Play();
    }

    protected void Death()
    {
        Debug.Log("Dead");
        isDead = true;
        spriteAnimator.SetBool("Idling", false);
        spriteAnimator.SetBool("Dead", true);
        collisionSound.Play();
    }

    public void DeathAnimationFinished()
    {
        Debug.Log("Death Animation Finished Event");
        if(isDead)
        {
            KillProjectile();
        }
    }
}
