using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public ProjectileBehaviour[] projectilePrefabs;

    private List<ProjectileBehaviour>[] projectilePools;

    // Start is called before the first frame update
    void Start()
    {
        projectilePools = new List<ProjectileBehaviour>[projectilePrefabs.Length];

        for (int i = 0; i < projectilePools.Length; i++)
        {
            projectilePools[i] = new List<ProjectileBehaviour>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public ProjectileBehaviour GetProjectile(int index, Vector3 pos)
    {
        ProjectileBehaviour projectile;
        if (projectilePools[index].Count == 0)
        {
            Debug.Log("Create a projectile");
            projectile = Instantiate(projectilePrefabs[index], pos, Quaternion.identity);
            projectile.SetPool(this, index);
        }
        else
        {
            projectile = projectilePools[index][0];
            projectile.gameObject.transform.position = pos;
            projectile.gameObject.SetActive(true);
            projectilePools[index].Remove(projectile);
        }

        projectile.ResetLifeTime();
        return projectile;
    }

    public void ReplaceProjectile(int index, ProjectileBehaviour projectile)
    {
        projectile.gameObject.SetActive(false);
        projectilePools[index].Add(projectile);
    }
}