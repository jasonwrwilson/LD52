using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] Text scoreText;
    [SerializeField] Text highscoreText;
    [SerializeField] private InventoryManager inventoryManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScoreText()
    {
        scoreText.text = inventoryManager.GetHarvestAmount().ToString();
        highscoreText.text = inventoryManager.GetHighHarvestAmount().ToString();
    }
}
