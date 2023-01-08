using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private UIManager uiManager;

    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject highscoreLabel;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenGameOverPanel()
    {
        scoreText.text = inventoryManager.GetHarvestAmount().ToString();

        if (inventoryManager.GetHarvestAmount() >= inventoryManager.GetHighHarvestAmount())
        {
            highscoreLabel.SetActive(true);
        }
        else
        {
            highscoreLabel.SetActive(false);
        }
    }

    public void OKButtonPressed()
    {
        uiManager.CloseGameOverPanel();
    }
}
