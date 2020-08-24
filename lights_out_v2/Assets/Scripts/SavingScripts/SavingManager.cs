using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

public class SavingManager : MonoBehaviour
{
    private int currentScene;
    private bool[] chests;
    public Player player;
    public BoolValue[] chestBooleans;

    public static SavingManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        if (Input.GetKeyDown("l"))
        {
            LoadGame();
        }
    }

    public void SaveGame()
    {
        Chest[] chestsArray = FindObjectsOfType<Chest>();
        chests = new bool[chestsArray.Length];
        for (int i = 0; i < chestsArray.Length; i++)
        {
            Debug.Log(chestsArray[i].isOpen);
            chests[i] = chestsArray[i].GetOpen();
            Debug.Log(chests[i]);
            /*chests[i] = chestsArray[i].isOpen;*/
        }
        currentScene = SceneManager.GetActiveScene().buildIndex;
        SavingSystem.SaveGame(player, chests, currentScene);

        Debug.Log("Game was saved");
    }
    public void LoadGame()
    {
        Debug.Log("ou yea");
        SavingData data = SavingSystem.LoadGame();
        Debug.Log(data.playerHearts);
        player.numberOfHearts = data.playerHearts;
        player.souls = data.playerSouls;
        player.currentHealth = data.playerHealth;
        player.maxHealth = data.maxHealth;
        player.numberOfKeys = data.numberOfKeys;
        player.numberOfSouls = data.numberOfSouls;
        player.positions = data.position;

        Chest[] chestsArray = FindObjectsOfType<Chest>();
        for (int i = 0; i < data.chests.Length; i++)
        {
            Debug.Log("DEBUGUEI");
            Debug.Log(data.chests[i]);
            chestsArray[i].SetOpen(data.chests[i]);
            Debug.Log(chestsArray[i].isOpen);
        }
        
    }

    public void ResetGame()
    {
        player.currentHealth = player.DEFAULT_currentHealth;
        player.maxHealth = player.DEFAULT_maxHealth;
        player.numberOfHearts = player.DEFAULT_numberOfHearts;
        player.numberOfKeys = player.DEFAULT_numberOfKeys;
        player.numberOfSouls = player.DEFAULT_numberOfSouls;
        player.souls = player.DEFAULT_souls;
        player.state = player.DEFAULT_state;
        player.positions[0] = player.DEFAULT_positions[0];
        player.positions[1] = player.DEFAULT_positions[1];
        player.positions[2] = player.DEFAULT_positions[2];
        Debug.Log(player.positions);
        foreach (BoolValue bv in chestBooleans)
        {
            bv.value = bv.DEAFULT_VALUE;
        }
    }
}
