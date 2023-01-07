using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    [SerializeField] private int fieldWidth, fieldHeight;
    [SerializeField] private HarvestTile tilePrefab;

    private HarvestTile[,] tiles;
    
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

                tiles[x,y] = newTile;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public HarvestTile GetTileAt(float x, float y)
    {
        int convertedX = Mathf.RoundToInt(x + fieldWidth / 2);
        int convertedY = Mathf.RoundToInt(y + fieldWidth / 2);

        if (convertedX >= 0 && convertedX < fieldWidth - 1 && convertedY >= 0 && convertedY <= fieldHeight - 1)
        {
            return tiles[convertedX, convertedY];
        }
        else
        {
            return null;
        }
    }
}
