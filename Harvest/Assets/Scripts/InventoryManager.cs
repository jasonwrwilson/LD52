using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryManager : MonoBehaviour
{
    public UnityEvent onHarvestCountChange = new UnityEvent();

    [SerializeField] private int startingHarvestAmount;
    private int harvestAmount = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        SetHarvestAmount(startingHarvestAmount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddHarvestAmount(int h)
    {
        SetHarvestAmount(harvestAmount + h);
    }

    public void SpendHarvestAmount(int h)
    {
        SetHarvestAmount(Mathf.Max(0, harvestAmount - h));
    }

    public void SetHarvestAmount(int h)
    {
        harvestAmount = h;
        onHarvestCountChange.Invoke();
    }

    public int GetHarvestAmount()
    {
        return harvestAmount;
    }
}
