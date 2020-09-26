using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseWhenClick : MonoBehaviour
{
    private bool gameIsPaused= false;

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
