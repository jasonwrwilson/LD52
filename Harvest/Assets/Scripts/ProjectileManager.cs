using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    private ProjectilePool projectilePool;
    [SerializeField] private float scytheSpawnTime;
    private float nextScytheSpawnTime;

    [SerializeField] private int startingScytheCount;
    private int scytheCount;

    [SerializeField] private Vector2 projectileOffset;

    // Start is called before the first frame update
    void Start()
    {
        nextScytheSpawnTime = scytheSpawnTime;
        scytheCount = startingScytheCount;
        
        projectilePool = gameObject.GetComponent<ProjectilePool>();        
    }

    // Update is called once per frame
    void Update()
    {
        if (nextScytheSpawnTime > 0 && scytheCount > 0)
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
    }

    public void FireScythe()
    {
        //fire position
        Vector3 scarecrowPosition = gameObject.transform.position;
        Vector3 firePosition = new Vector3(scarecrowPosition.x + projectileOffset.x, scarecrowPosition.y + projectileOffset.y, 0);

        float randomAngle = Random.Range(0, 360);
        
        for (int i = 0; i < scytheCount; i++)
        {
            //spawn scythe
            ProjectileBehaviour projectile = projectilePool.GetProjectile(1, firePosition);

            //random direction
            float angleInRadians = (randomAngle + i * (360 / scytheCount)) * Mathf.Deg2Rad;
            projectile.SetDirection(new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians)));
        }
    }
}
