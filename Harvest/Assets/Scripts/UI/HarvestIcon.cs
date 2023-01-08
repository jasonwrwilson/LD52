using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HarvestIcon : MonoBehaviour
{
    [SerializeField] private Sprite fullSprite;
    [SerializeField] private Sprite emptySprite;
    [SerializeField] private Image iconImage;

    private bool full;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetFull(bool f)
    {
        full = f;
        if(full)
        {
            iconImage.sprite = fullSprite;
        }
        else
        {
            iconImage.sprite = emptySprite;
        }
    }

    public bool IsFull()
    {
        return full;
    }

}
