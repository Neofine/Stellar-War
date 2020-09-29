using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseWhenClick : MonoBehaviour
{
    public GameObject Panel;
    public GameObject GeneralPanel;
    private bool gameIsPaused= false;

    public void OpenPanel()
    {
        bool isActive = Panel.activeSelf;
        if (GeneralPanel.activeSelf == true && Panel.activeSelf == false)
        {
            GeneralPanel.SetActive(false);
            Panel.SetActive(false);
        }
        else
        {
            Panel.SetActive(!isActive);
        }
    }

    public void PauseGame()
    {
        if (gameIsPaused)
        {
            gameIsPaused = false;
        }
        else
        {         
            gameIsPaused = true;
        }
    }


    void Update()
    {
       
            if (!gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pauseee();
            }
        
    }
    void Resume()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
    }


    void Pauseee()
    {
        Time.timeScale = 0f;
        gameIsPaused = true;
    }
}
