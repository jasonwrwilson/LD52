using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTime : MonoBehaviour
{
    [SerializeField] Text timerText;
    [SerializeField] EnemyManager enemyManager;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timerText.text = TimeConversion(enemyManager.GetElapsedTime());
    }

    private string TimeConversion(float t)
    {
        int minutes = (int)(t / 60);
        int seconds = (int)(t - minutes * 60);
        string minutesString = minutes.ToString();
        if(minutes < 10)
        {
            minutesString = "0" + minutesString;
        }
        string secondsString = seconds.ToString();
        if(seconds < 10)
        {
            secondsString = "0" + secondsString;
        }
        return minutesString + ":" + secondsString;
    }
}
