using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private int maxLives;
    private int lives;
    
    public UnityEvent onExperienceChange = new UnityEvent();
    public UnityEvent onLivesCountChange = new UnityEvent();
    public UnityEvent onScoreChange = new UnityEvent();

    private int playerExperience;
    private int playerLevel = 1;
    
    private int harvestAmount = 0;
    private int highHarvestAmount = 0;

    [SerializeField] private string[] upgradeNames;
    [SerializeField] private string[] upgradeDescription;
    [SerializeField] private Sprite[] upgradeIcons;
    [SerializeField] private int[] upgradeStartingLevels;
    [SerializeField] private int[] upgradePrerequisite;
    private int[] upgradeLevel;

    private int[] selectedUpgrades = new int[3];

    private float stoneDamageBonus = 1.0f;
    private float stoneDamageBonusImprovement = 0.5f;
    private float stoneSpeedBonus = 1.0f;
    private float stoneSpeedImprovement = 0.5f;
    private int startingStoneSpreadCount = 2;
    private int stoneSpreadCountImprovement = 1;
    private int startingScytheCount = 2;
    private int scytheCountImprovement = 1;
    private float scytheDamageBonus = 1.0f;
    private float scytheDamageBonusImprovement = 0.5f;
    private float scytheSpeedBonus = 1.0f;
    private float scytheSpeedBonusImprovement = 0.5f;
    private float startingSprinklerRange = 0.0f;
    private float sprinklerRangeImprovement = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        LoadRecord();

        SetHarvestAmount(0);
        
        SetLivesCount(maxLives);
        SetExperience(0);

        upgradeLevel = new int[upgradeStartingLevels.Length];
        for(int i = 0; i < upgradeStartingLevels.Length; i++)
        {
            upgradeLevel[i] = upgradeStartingLevels[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GainLife(int l)
    {
        SetLivesCount(Mathf.Min(lives + l, maxLives));
    }

    public void LoseLife(int l)
    {
        SetLivesCount(Mathf.Max(0, lives - l));
    }

    private void SetLivesCount(int l)
    {
        lives = l;
        onLivesCountChange.Invoke();

        if(lives <= 0)
        {
            SaveRecord();
            gameObject.GetComponent<UIManager>().OpenGameOverPanel();
        }
    }

    public int GetLivesCount()
    {
        return lives;
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
        if (harvestAmount > highHarvestAmount)
        {
            highHarvestAmount = harvestAmount;
        }
        onScoreChange.Invoke();
    }

    public int GetHarvestAmount()
    {
        return harvestAmount;
    }

    public int GetHighHarvestAmount()
    {
        return highHarvestAmount;
    }

    public void EarnExperience(int xp)
    {
        SetExperience(playerExperience + xp);
    }

    private void SetExperience(int xp)
    {
        playerExperience = xp;
        while (playerExperience >= NextPlayerLevelExperienceRequirement())
        {
            playerExperience -= NextPlayerLevelExperienceRequirement();
            LevelUp();
        }
        onExperienceChange.Invoke();
    }

    public int GetPlayerExperience()
    {
        return playerExperience;
    }

    public float GetPlayerLevelPercent()
    {
        return (float)playerExperience / (float)NextPlayerLevelExperienceRequirement();
    }

    public int NextPlayerLevelExperienceRequirement()
    {
        return GetPlayerLevel() * 100;
    }

    public int GetPlayerLevel()
    {
        return playerLevel;
    }

    private void LevelUp()
    {
        playerLevel += 1;
        SelectUpgrades();
        gameObject.GetComponent<UIManager>().OpenLevelUpPanel();
    }

    private void SelectUpgrades()
    {
        for (int i = 0; i < 3; i++)
        {
            bool choosingUpgrade = true;
            while (choosingUpgrade)
            {
                int randomUpgrade = Random.Range(0, upgradeNames.Length);
                if (upgradeLevel[upgradePrerequisite[randomUpgrade]] > 0)
                {
                    if (randomUpgrade != 7 || lives < maxLives)
                    {
                        if (i == 0)
                        {
                            selectedUpgrades[i] = randomUpgrade;
                            choosingUpgrade = false;
                        }
                        else
                        {
                            bool safe = true;
                            for (int j = 0; j < i; j++)
                            {
                                if (selectedUpgrades[j] == randomUpgrade)
                                {
                                    safe = false;
                                }
                            }

                            if (safe)
                            {
                                selectedUpgrades[i] = randomUpgrade;
                                choosingUpgrade = false;
                            }
                        }
                    }
                }
            }
        }
    }

    public string GetSelectedUpgradeName(int index)
    {
        return upgradeNames[selectedUpgrades[index]];
    }

    public string GetSelectedUpgradeDescription(int index)
    {
        return upgradeDescription[selectedUpgrades[index]];
    }

    public Sprite GetSelectedUpgradeIcon(int index)
    {
        return upgradeIcons[selectedUpgrades[index]];
    }

    public int GetSelectedUpgradeLevel(int index)
    {
        return upgradeLevel[selectedUpgrades[index]] + 1;
    }

    public void PurchaseSelectedUpgrade(int index)
    {
        int upgradeIndex = selectedUpgrades[index];

        upgradeLevel[upgradeIndex] += 1;

        if (upgradeIndex == 7)
        {
            GainLife(1);
        }
    }

    public float GetStoneDamageBonus()
    {
        return stoneDamageBonus + stoneDamageBonusImprovement * upgradeLevel[0];
    }
 
    public float GetStoneSpeedBonus()
    {
        return stoneSpeedBonus + stoneSpeedImprovement * upgradeLevel[1];
    }

    public int GetStoneSpreadCount()
    {
        if (upgradeLevel[2] > 0)
        {
            return startingStoneSpreadCount + stoneSpreadCountImprovement * upgradeLevel[2];
        }
        else
        {
            return 0;
        }
    }

    public int GetScytheCount()
    {
        if (upgradeLevel[3] > 0)
        {
            return startingScytheCount + scytheCountImprovement * upgradeLevel[3];
        }
        else
        {
            return 0;
        }
    }

    public float GetScytheDamageBonus()
    {
        return scytheDamageBonus + scytheDamageBonusImprovement * upgradeLevel[4];
    }

    public float GetScytheSpeedBonus()
    {
        return scytheSpeedBonus + scytheSpeedBonusImprovement * upgradeLevel[5];
    }

    public float GetSprinklerRange()
    {
        if (upgradeLevel[6] > 0)
        {
            return startingSprinklerRange + sprinklerRangeImprovement * upgradeLevel[6];
        }
        else
        {
            return 0;
        }
    }
    private void SaveRecord()
    {
        PlayerPrefs.SetInt("Highscore", highHarvestAmount);
        PlayerPrefs.Save();
        Debug.Log("Game data saved!");
    }

    private void LoadRecord()
    {
        if (PlayerPrefs.HasKey("Highscore"))
        {
            highHarvestAmount = PlayerPrefs.GetInt("Highscore");
            Debug.Log("Game data saved!");
        }
    }
}
