﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class MainMenu : MonoBehaviour
{
    public GameObject loadingCanvas;
    public SavingManager saveManager;
    public SignalSend test;
    public BoolValue load;

    private void Start()
    {
        load.value = false;
    }
    public void PlayGame()
    {

        saveManager.ResetGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }

    public void LoadGame()
    {
        Debug.Log("ou yea");
        SavingData data = SavingSystem.LoadGame();
        load.value = load.DEAFULT_VALUE;
        SceneManager.LoadScene(data.scene);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    private IEnumerator LoadingCanvasCO(bool load)
    {
        Debug.Log("coroutine started");
        loadingCanvas.SetActive(true);
        yield return new WaitForSeconds(5f);
        if (load == true)
        {
            Debug.Log("yes");
            LoadGame();
        }
        else
        {
            PlayGame();
        }
    }
}
