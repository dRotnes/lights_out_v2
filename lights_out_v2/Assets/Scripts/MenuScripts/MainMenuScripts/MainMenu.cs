using System.Collections;
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
    public BoolValue timeline;

    private void Awake()
    {
        load.value = false;
        timeline.value = false;
    }
    public void PlayGame()
    {
        timeline.value = true;
        load.value = true;
        saveManager.ResetGame();
    }

    public void LoadGame()
    {
        load.value = true;
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
