using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehaviour : MonoBehaviour
{
    protected PickupPool pickupPool;
    protected int poolIndex;
    protected ScarecrowBehaviour targetScarecrow;

    [SerializeField] protected float travelTime;
    protected float remainingTravelTime;
    protected Vector3 startingPosition;
    protected PickupManager pickupManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (remainingTravelTime > 0 && targetScarecrow != null)
        {
            remainingTravelTime -= Time.deltaTime;

            if (remainingTravelTime < 0)
            {
                remainingTravelTime = 0;
            }

            Vector3 targetPosition = targetScarecrow.gameObject.transform.position;
            Vector3 pos = Vector3.Lerp(targetPosition, startingPosition, remainingTravelTime / travelTime);

            gameObject.transform.position = pos;

            if (remainingTravelTime == 0)
            {
                AwardPickup();
            }
        }
    }

    public void SetPool(PickupPool pp, int index)
    {
        pickupPool = pp;
        poolIndex = index;
    }

    public void SetPickupManager(PickupManager pickman)
    {
        pickupManager = pickman;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger Pickup");
        if (targetScarecrow == null)
        {
            ScarecrowBehaviour scarecrow = collision.gameObject.GetComponent<ScarecrowBehaviour>();

            if (scarecrow != null)
            {
                Debug.Log("Target scarecrow!");
                targetScarecrow = scarecrow;
                startingPosition = gameObject.transform.position;

                remainingTravelTime = travelTime;
            }
        }
    }

    protected void KillProjectile()
    {
        targetScarecrow = null;
        pickupPool.ReplaceProjectile(poolIndex, this);
    }

    protected virtual void AwardPickup()
    {
        Debug.Log("Award Pickup!");
        KillProjectile();
    }

    
}
