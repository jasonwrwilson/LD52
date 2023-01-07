using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HarvestCounter : MonoBehaviour
{
    [SerializeField] private Text countText;
    [SerializeField] private InventoryManager inventoryManager;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHarvestCount()
    {
        countText.text = "Harvest: " + inventoryManager.GetHarvestAmount().ToString();
    }
}
