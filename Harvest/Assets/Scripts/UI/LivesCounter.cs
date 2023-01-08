using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesCounter : MonoBehaviour
{
    [SerializeField] private HarvestIcon[] icons;
    [SerializeField] private InventoryManager inventoryManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLivesCounter()
    {
        int lives = inventoryManager.GetLivesCount();

        Debug.Log(lives);

        for (int i = 0; i < icons.Length; i++)
        {
            icons[i].SetFull(i < lives);
        }
    }
}
