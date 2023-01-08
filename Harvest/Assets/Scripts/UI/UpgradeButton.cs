using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private Text upgradeText;
    [SerializeField] private Image upgradeIcon;
    [SerializeField] private Text upgradeDescription;
    [SerializeField] private Text upgradeLevelText;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDetails(string name, string description, Sprite icon, int level)
    {
        upgradeText.text = name;
        upgradeDescription.text = description;
        upgradeIcon.sprite = icon;
        upgradeLevelText.text = "Level " + level.ToString();
    }
}
