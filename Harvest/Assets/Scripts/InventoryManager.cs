using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private int maxLives;
    private int lives;
    
    public UnityEvent onHarvestCountChange = new UnityEvent();
    public UnityEvent onExperienceChange = new UnityEvent();
    public UnityEvent onLivesCountChange = new UnityEvent();

    private int playerExperience;
    private int playerLevel = 1;
    
    private int harvestAmount = 0;
    private int harvestLevel = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        SetLivesCount(maxLives);
        SetExperience(0);
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
        if (harvestAmount >= NextHarvestLevelAmount())
        {
            harvestAmount -= NextHarvestLevelAmount();
            //LevelUp();
        }
        onHarvestCountChange.Invoke();
    }

    public int GetHarvestAmount()
    {
        return harvestAmount;
    }

    public int NextHarvestLevelAmount()
    {
        return GetHarvestLevel() * 100;
    }

    public float GetHarvestLevelPercent()
    {
        return (float)GetHarvestAmount() / (float)NextHarvestLevelAmount();
    }

    public int GetHarvestLevel()
    {
        return harvestLevel;
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
    }
}
