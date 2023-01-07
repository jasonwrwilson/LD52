using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    [SerializeField] private int fieldWidth, fieldHeight;
    [SerializeField] private HarvestTile tilePrefab;
    private HarvestTile[,] tiles;
    [SerializeField] private float cropPlantingTime;
    private float remainingCropPlantingTime;
    private int cropsGrowing;
    [SerializeField] int maxCropsGrowing;
    
    private Vector2 scarecrowTileLocation;
    
    // Start is called before the first frame update
    [SerializeField]
    private void Start()
    {
        tiles = new HarvestTile[fieldWidth,fieldHeight];
        
        for (int x = 0; x < fieldWidth; x++)
        {
            for (int y = 0; y < fieldHeight; y++)
            {
                HarvestTile newTile = Instantiate(tilePrefab, new Vector3(x - Mathf.RoundToInt(fieldWidth / 2), y - Mathf.RoundToInt(fieldHeight / 2), 0), Quaternion.identity);
                newTile.SetInventoryManager(gameObject.GetComponent<InventoryManager>());
                newTile.SetFieldManager(this);

                tiles[x,y] = newTile;
            }
        }

        cropsGrowing = 0;
        remainingCropPlantingTime = cropPlantingTime;
    }

    // Update is called once per frame
    void Update()
    {
        //check scarecrow tile for harvestability
        HarvestTile harvestTile = GetTileAt(scarecrowTileLocation.x, scarecrowTileLocation.y);
        if (harvestTile != null && harvestTile.ReadyForHarvest())
        {
            harvestTile.Harvest();
        }

        if (remainingCropPlantingTime > 0 && cropsGrowing < maxCropsGrowing)
        {
            remainingCropPlantingTime -= Time.deltaTime;
            if (remainingCropPlantingTime <= 0)
            {
                //plant a crop
                bool plantingCrop = true;
                while (plantingCrop)
                {
                    int randomX = Random.Range(0, fieldWidth);
                    int randomY = Random.Range(0, fieldHeight);

                    int convertedX = Mathf.RoundToInt(scarecrowTileLocation.x + fieldWidth / 2);
                    int convertedY = Mathf.RoundToInt(scarecrowTileLocation.y + fieldHeight / 2);

                    if (!(randomX == convertedX && randomY == convertedY) && tiles[randomX, randomY].ReadyForPlanting())
                    {
                        tiles[randomX, randomY].PlantCrop();
                        plantingCrop = false;
                        remainingCropPlantingTime = cropPlantingTime;
                    }
                }
            }
        }
    }

    public HarvestTile GetTileAt(float x, float y)
    {
        int convertedX = Mathf.RoundToInt(x + fieldWidth / 2);
        int convertedY = Mathf.RoundToInt(y + fieldHeight / 2);

        if (ValidTileLocation(convertedX, convertedY))
        {
            return tiles[convertedX, convertedY];
        }
        else
        {
            return null;
        }
    }

    public void RegisterScarecrowLocation(float x, float y)
    {
        scarecrowTileLocation = new Vector2(x, y);
    }

    public bool CanMoveRight()
    {
        int convertedX = Mathf.RoundToInt(scarecrowTileLocation.x + fieldWidth / 2 - 1);
        int convertedY = Mathf.RoundToInt(scarecrowTileLocation.y + fieldHeight / 2);

        return ValidTileLocation(convertedX, convertedY);
    }

    public bool CanMoveLeft()
    {
        int convertedX = Mathf.RoundToInt(scarecrowTileLocation.x + fieldWidth / 2 + 1);
        int convertedY = Mathf.RoundToInt(scarecrowTileLocation.y + fieldHeight / 2);

        return ValidTileLocation(convertedX, convertedY);
    }

    public bool CanMoveUp()
    {
        int convertedX = Mathf.RoundToInt(scarecrowTileLocation.x + fieldWidth / 2);
        int convertedY = Mathf.RoundToInt(scarecrowTileLocation.y + fieldHeight / 2 + 1);

        return ValidTileLocation(convertedX, convertedY);
    }

    public bool CanMoveDown()
    {
        int convertedX = Mathf.RoundToInt(scarecrowTileLocation.x + fieldWidth / 2);
        int convertedY = Mathf.RoundToInt(scarecrowTileLocation.y + fieldHeight / 2 - 1);

        return ValidTileLocation(convertedX, convertedY);
    }

    private bool ValidTileLocation(int x, int y)
    {
        return x >= 0 && x <= fieldWidth - 1 && y >= 0 && y <= fieldHeight - 1;
    }

    public void CropCleared()
    {
        cropsGrowing -= 1;
        if (cropsGrowing < 0)
        {
            cropsGrowing = 0;
        }
    }

    public void CropPlanted()
    {
        cropsGrowing += 1;
        if (cropsGrowing > maxCropsGrowing)
        {
            cropsGrowing = maxCropsGrowing;
        }
    }
}
