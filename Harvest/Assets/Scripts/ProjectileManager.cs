using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    private ProjectilePool projectilePool;

    [SerializeField] private float stoneCooldown;
    private float remainingStoneCooldown;

    [SerializeField] private float scytheSpawnTime;
    private float nextScytheSpawnTime;

    [SerializeField] private Vector2 projectileOffset;

    private InventoryManager inventoryManager;

    // Start is called before the first frame update
    void Start()
    {
        remainingStoneCooldown = 0;
        nextScytheSpawnTime = scytheSpawnTime;
        
        projectilePool = gameObject.GetComponent<ProjectilePool>();

        inventoryManager = gameObject.GetComponent<ScarecrowBehaviour>().GetInventoryManager();
    }

    // Update is called once per frame
    void Update()
    {
        if (remainingStoneCooldown > 0)
        {
            remainingStoneCooldown -= Time.deltaTime;
        }
        
        if (nextScytheSpawnTime > 0 && inventoryManager.GetScytheCount() > 0)
        {
            nextScytheSpawnTime -= Time.deltaTime;
            if (nextScytheSpawnTime <= 0)
            {
                FireScythe();

                nextScytheSpawnTime = scytheSpawnTime;
            }
        }
    }

    public void FireBullet()
    {
        if (remainingStoneCooldown > 0)
        {
            return;
        }
        else
        {
            remainingStoneCooldown = stoneCooldown;
            
            //fire position
            Vector3 scarecrowPosition = gameObject.transform.position;
            Vector3 firePosition = new Vector3(scarecrowPosition.x + projectileOffset.x, scarecrowPosition.y + projectileOffset.y, 0);

            //spawn bullets
            ProjectileBehaviour projectile = projectilePool.GetProjectile(0, firePosition);

            //toward mouse pointer
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mouseDirection = new Vector2(mousePosition.x - firePosition.x, mousePosition.y - firePosition.y);
            mouseDirection.Normalize();
            projectile.SetDirection(mouseDirection);

            if (inventoryManager.GetStoneSpreadCount() > 0)
            {
                for (int i = 0; i < inventoryManager.GetStoneSpreadCount(); i++)
                {
                    Vector2 direction = mouseDirection;

                    float degrees = 10 * ((i + 2) / 2) * (1 - (i % 2) * 2);

                    float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
                    float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

                    float tx = direction.x;
                    float ty = direction.y;

                    direction.x = (cos * tx) - (sin * ty);
                    direction.y = (sin * tx) + (cos * ty);

                    ProjectileBehaviour spreadProjectile = projectilePool.GetProjectile(0, firePosition);
                    spreadProjectile.SetDirection(direction);
                }
            }
        }
    }

    public void FireScythe()
    {
        //fire position
        Vector3 scarecrowPosition = gameObject.transform.position;
        Vector3 firePosition = new Vector3(scarecrowPosition.x + projectileOffset.x, scarecrowPosition.y + projectileOffset.y, 0);

        float randomAngle = Random.Range(0, 360);
        
        for (int i = 0; i < inventoryManager.GetScytheCount(); i++)
        {
            //spawn scythe
            ProjectileBehaviour projectile = projectilePool.GetProjectile(1, firePosition);

            //random direction
            float angleInRadians = (randomAngle + i * (360 / inventoryManager.GetScytheCount())) * Mathf.Deg2Rad;
            projectile.SetDirection(new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians)));
        }
    }
}
