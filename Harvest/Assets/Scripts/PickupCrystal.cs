using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCrystal : PickupBehaviour
{
    [SerializeField] private int experienceReward;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    protected override void AwardPickup()
    {
        pickupManager.AwardExperience(experienceReward);
        base.AwardPickup();
    }
}
