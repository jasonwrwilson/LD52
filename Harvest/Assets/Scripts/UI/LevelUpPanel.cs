using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpPanel : MonoBehaviour
{
    [SerializeField] UpgradeButton[] upgradeButtons;
    [SerializeField] InventoryManager inventoryManager;
    [SerializeField] UIManager uiManager;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenLevelUpPanel()
    {
        for(int i = 0; i < upgradeButtons.Length; i++)
        {
            upgradeButtons[i].SetDetails(inventoryManager.GetSelectedUpgradeName(i), inventoryManager.GetSelectedUpgradeDescription(i), inventoryManager.GetSelectedUpgradeIcon(i), inventoryManager.GetSelectedUpgradeLevel(i));
        }
    }

    public void UpgradeButtonPressed(int index)
    {
        inventoryManager.PurchaseSelectedUpgrade(index);
        uiManager.CloseLevelUpPanel();
    }
}
