using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class MainMenu : MonoBehaviour
{
    public GameObject loadingCanvas;
    public Player player;
    public SignalSend newGame;
    public SignalSend loadGame;
    public void PlayGame()
    {
        player.currentHealth = 6;
        player.maxHealth = 6;
        player.numberOfHearts = 3;
        player.numberOfKeys = 0;
        player.numberOfSouls = 0;
        player.souls = 0;
        player.state = PlayerState.walking;


        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadGame()
    {
        Debug.Log("ou yea");
        SavingData data = SavingSystem.LoadGame();
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
