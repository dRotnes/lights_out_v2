using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool paused;
    public GameObject pauseMenuUI;
    public PlayerMovement player;
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (!paused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
        player.BlockOrAllowMovement();
    }
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
        player.BlockOrAllowMovement();
    }
    public void Quit()
    {
        SceneManager.LoadScene(0);
    }
}
