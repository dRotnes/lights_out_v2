using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

public class SavingManager : MonoBehaviour
{
    private int currentScene;
    public Player player;

    public static SavingManager instance;

    private void Update()
    {
        if (Input.GetKeyDown("l"))
        {
            LoadGame();
        }
    }

    public void SaveGame()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        SavingSystem.SaveGame(player, currentScene);

        Debug.Log("Game was saved");
    }
    public void LoadGame()
    {
        Debug.Log("ou yea");
        SavingData data = SavingSystem.LoadGame();

        player.numberOfHearts = data.playerHearts;
        player.souls = data.playerSouls;
        player.currentHealth = data.playerHealth;
        player.maxHealth = data.maxHealth;
        player.numberOfKeys = data.numberOfKeys;
        player.numberOfSouls = data.numberOfSouls;
        player.positions[0] = data.position[0];
        player.positions[1] = data.position[1];
        player.positions[2] = data.position[2];
        SceneManager.LoadScene(data.scene);
    }
}
