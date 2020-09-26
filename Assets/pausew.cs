using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pausew : MonoBehaviour
{
    public static bool gameIsPaused;

    void Update()
    {
        if (Input.GetKeyDown("m"))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pauseee();
            }
        }
    }

    public void Resume()
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
