using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    private PickupPool pickupPool;
    
    // Start is called before the first frame update
    void Start()
    {
        pickupPool = gameObject.GetComponent<PickupPool>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnExeriencePickups(Vector3 pos, int xp)
    {
        int xpToSpawn = xp;
        while (xpToSpawn > 0)
        {
            Vector3 randomizedPosition = pos;
            randomizedPosition.x += Random.Range(-0.25f, 0.25f);
            randomizedPosition.y += Random.Range(-0.25f, 0.25f);

            PickupBehaviour pickup;
            if (xpToSpawn >= 25)
            {
                pickup = pickupPool.GetProjectile(2, randomizedPosition);
                xpToSpawn -= 25;
            }
            else if (xpToSpawn >= 5)
            {
                pickup = pickupPool.GetProjectile(1, randomizedPosition);
                xpToSpawn -= 5;
            }
            else
            {
                pickup = pickupPool.GetProjectile(0, randomizedPosition);
                xpToSpawn -= 1;
            }
        }
    }

    public void AwardExperience(int xp)
    {
        gameObject.GetComponent<InventoryManager>().EarnExperience(xp);
    }
}
