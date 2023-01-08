using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPool : MonoBehaviour
{
    public PickupBehaviour[] pickupPrefabs;

    private List<PickupBehaviour>[] pickupPools;

    // Start is called before the first frame update
    void Start()
    {
        pickupPools = new List<PickupBehaviour>[pickupPrefabs.Length];

        for (int i = 0; i < pickupPools.Length; i++)
        {
            pickupPools[i] = new List<PickupBehaviour>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public PickupBehaviour GetProjectile(int index, Vector3 pos)
    {
        PickupBehaviour pickup;
        if (pickupPools[index].Count == 0)
        {
            Debug.Log("Create a pickup");
            pickup = Instantiate(pickupPrefabs[index], pos, Quaternion.identity);
            pickup.SetPickupManager(gameObject.GetComponent<PickupManager>());
            pickup.SetPool(this, index);
        }
        else
        {
            pickup = pickupPools[index][0];
            pickup.gameObject.transform.position = pos;
            pickup.gameObject.SetActive(true);
            pickupPools[index].Remove(pickup);
        }

        pickup.ResetDeathDelay();
        return pickup;
    }

    public void ReplaceProjectile(int index, PickupBehaviour pickup)
    {
        pickup.gameObject.SetActive(false);
        pickupPools[index].Add(pickup);
    }
}
