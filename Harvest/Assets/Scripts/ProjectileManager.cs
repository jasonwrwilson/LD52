﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    private ProjectilePool projectilePool;
    [SerializeField] private float projectileSpawnTime;
    private float nextProjectileSpawnTime;

    [SerializeField] private Vector2 projectileOffset;

    // Start is called before the first frame update
    void Start()
    {
        projectilePool = gameObject.GetComponent<ProjectilePool>();

        nextProjectileSpawnTime = projectileSpawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (nextProjectileSpawnTime > 0)
        {
            nextProjectileSpawnTime -= Time.deltaTime;
            if (nextProjectileSpawnTime <= 0)
            {


                //random direction
                /*float angleInRadians = Random.Range(0, 360) * Mathf.Deg2Rad;
                projectile.SetDirection(new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians)));*/
                //FireBullet();


                nextProjectileSpawnTime = projectileSpawnTime;
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
}
