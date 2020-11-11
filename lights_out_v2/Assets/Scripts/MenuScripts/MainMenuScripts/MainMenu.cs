using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class MainMenu : MonoBehaviour
{
    public BoolValue load;

    public void PlayGame()
    {
        load.value = false;
        SceneManager.LoadScene(1);
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
