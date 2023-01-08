using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HarvestCounter : MonoBehaviour
{
    [SerializeField] private Text countText;
    [SerializeField] private GameObject countMeter;
    [SerializeField] private GameObject countMeterGrey;
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
        countText.text = "Level " + inventoryManager.GetHarvestLevel().ToString();

        RectTransform countMeterTransform = countMeter.GetComponent<RectTransform>();
        RectTransform countMeterGreyTransform = countMeterGrey.GetComponent<RectTransform>();

        countMeterTransform.sizeDelta = new Vector2(countMeterGreyTransform.sizeDelta.x * inventoryManager.GetHarvestLevelPercent(), countMeterGreyTransform.sizeDelta.y);
    }
}
