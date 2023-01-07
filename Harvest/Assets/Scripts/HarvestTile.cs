using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestTile : MonoBehaviour
{
    [SerializeField] private Color healthyColor;
    [SerializeField] private Color unhealthyColor;

    [SerializeField] private float maxHealth;
    private float health;
    
    [SerializeField] private GameObject highlight;
    [SerializeField] private float growthTime;
    private float currentStageTimer;

    [SerializeField] private Sprite[] growthStageImages;
    private int growthStage;

    [SerializeField] private SpriteRenderer growthSprite;

    private InventoryManager inventoryManager;
    
    // Start is called before the first frame update
    void Start()
    {
        SetGrowthStage(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (growthStage > 0 && growthStage <= growthStageImages.Length && currentStageTimer > 0)
        {
            currentStageTimer -= Time.deltaTime;
            if (currentStageTimer <= 0)
            {
                SetGrowthStage(growthStage + 1);
                currentStageTimer = growthTime / (growthStageImages.Length + 1);
            }
        }

    }

    public void HighlightTile()
    {
        highlight.SetActive(true);
    }

    public void DehighlightTile()
    {
        highlight.SetActive(false);
    }

    private void OnMouseEnter()
    {
        highlight.SetActive(true);
    }

    private void OnMouseExit()
    {
        highlight.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (ReadyForPlanting())
        {
            PlantCrop();
        }
        else if (ReadyForHarvest())
        {
            Harvest();
        }
    }
    private void SetGrowthStage(int gs)
    {
        if (gs != growthStage && gs <= growthStageImages.Length)
        {
            growthStage = gs;
            if (gs > 0)
            {
                growthSprite.gameObject.SetActive(true);
                growthSprite.sprite = growthStageImages[growthStage - 1];
            }
            else
            {
                growthSprite.gameObject.SetActive(false);
            }
        }
    }

    public int GetGrowthStage()
    {
        return growthStage;
    }

    public void SetInventoryManager(InventoryManager inv)
    {
        inventoryManager = inv;
    }

    public void SetHealth(float h)
    {
        health = h;
        growthSprite.color = Color.Lerp(unhealthyColor, healthyColor, health / maxHealth);

        if (health == 0)
        {
            SetGrowthStage(0);
        }
    }

    public void TakeDamage(float d)
    {
        SetHealth(Mathf.Max(health - d, 0));
    }

    public void Harvest()
    {
        if (ReadyForHarvest())
        {
            inventoryManager.AddHarvestAmount(10);
            SetGrowthStage(0);
        }
    }

    public bool ReadyForHarvest()
    {
        return growthStage == growthStageImages.Length;
    }

    public void PlantCrop()
    {
        if (ReadyForPlanting())
        {
            SetHealth(maxHealth);
            SetGrowthStage(1);
            currentStageTimer = growthTime / (growthStageImages.Length + 1);
        }
    }

    public bool ReadyForPlanting()
    {
        return growthStage == 0;
    }
}
