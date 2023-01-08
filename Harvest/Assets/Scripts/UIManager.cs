using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private bool paused;

    [SerializeField] LevelUpPanel levelUpPanel;
    [SerializeField] GameOverPanel gameOverPanel;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenLevelUpPanel()
    {
        levelUpPanel.gameObject.SetActive(true);
        levelUpPanel.OpenLevelUpPanel();
        PauseGame(true);
    }

    public void CloseLevelUpPanel()
    {
        levelUpPanel.gameObject.SetActive(false);
        PauseGame(false);
    }

    public void OpenGameOverPanel()
    {
        gameOverPanel.gameObject.SetActive(true);
        gameOverPanel.OpenGameOverPanel();
        PauseGame(true);
    }

    public void CloseGameOverPanel()
    {
        gameOverPanel.gameObject.SetActive(false);
        SceneManager.LoadScene("MainMenu");
        PauseGame(false);
    }

    private void PauseGame(bool p)
    {
        paused = p;
        if(paused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

    }
}
