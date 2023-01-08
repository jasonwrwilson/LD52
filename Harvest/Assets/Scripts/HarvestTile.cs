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
    private FieldManager fieldManager;

    [SerializeField] private GameObject rainEffect;
    [SerializeField] private GameObject sparkleEffect;

    // Start is called before the first frame update
    void Start()
    {
        SetGrowthStage(0);
    }

    // Update is called once per frame
    void Update()
    {
        float rainBonus = 1.0f;
        if (fieldManager.InWaterRange(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y)))
        {
            rainEffect.SetActive(true);
            rainBonus = 2.0f;
        }
        else
        {
            rainEffect.SetActive(false);
        }
        
        if (growthStage > 0 && growthStage <= growthStageImages.Length && currentStageTimer > 0)
        {
            currentStageTimer -= Time.deltaTime * rainBonus;
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
        //highlight.SetActive(true);
    }

    private void OnMouseExit()
    {
        //highlight.SetActive(false);
    }

    private void OnMouseDown()
    {
        /*if (ReadyForPlanting())
        {
            PlantCrop();
        }
        else if (ReadyForHarvest())
        {
            Harvest();
        }*/
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

            if (gs == growthStageImages.Length)
            {
                sparkleEffect.SetActive(true);
            }
            else
            {
                sparkleEffect.SetActive(false);
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

    public void SetFieldManager(FieldManager field)
    {
        fieldManager = field;
    }

    public void SetHealth(float h)
    {
        health = h;
        growthSprite.color = Color.Lerp(unhealthyColor, healthyColor, health / maxHealth);

        if (health <= 0)
        {
            Death();
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
            fieldManager.CropCleared();
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
            fieldManager.CropPlanted();
        }
    }

    public bool ReadyForPlanting()
    {
        return growthStage == 0;
    }

    private void Death()
    {
        SetGrowthStage(0);
        fieldManager.CropCleared();
        inventoryManager.LoseLife(1);
    }

    public bool HasCrop()
    {
        return growthStage > 0;
    }
}
