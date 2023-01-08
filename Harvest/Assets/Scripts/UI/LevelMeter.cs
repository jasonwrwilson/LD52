using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMeter : MonoBehaviour
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

    public void UpdateLevelMeter()
    {
        countText.text = "Level " + inventoryManager.GetPlayerLevel().ToString();

        RectTransform countMeterTransform = countMeter.GetComponent<RectTransform>();
        RectTransform countMeterGreyTransform = countMeterGrey.GetComponent<RectTransform>();

        countMeterTransform.sizeDelta = new Vector2(countMeterGreyTransform.sizeDelta.x * inventoryManager.GetPlayerLevelPercent(), countMeterGreyTransform.sizeDelta.y);
    }
}
