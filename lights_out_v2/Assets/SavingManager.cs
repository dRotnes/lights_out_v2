using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using Pathfinding.Ionic.Zip;
using UnityEngine.XR.WSA;

public class SavingManager : MonoBehaviour
{
    public HealthUI healthUI;
    public SoulsUI soulsUI;
    public PlayerHealth playerHealthScript;
    public Inventory playerInventory;

    private void Update()
    {
        if (Input.GetKeyDown("s"))
        {
            SaveGame();
        }
        if (Input.GetKeyDown("r"))
        {
            ResetGame();
        }
        if (Input.GetKeyDown("l"))
        {
            LoadGame();
        }
    }
    public void SaveGame()
    {
        float[] positions = new float[3] { playerHealthScript.transform.position.x, playerHealthScript.transform.position.y, playerHealthScript.transform.position.z };
        SavingSystem.SaveGame(healthUI, soulsUI, playerHealthScript, playerInventory, positions);

        Debug.Log("Game was saved");
    }

    public void LoadGame()
    {
        SavingData data = SavingSystem.LoadGame();

        healthUI.heartContainer.value = data.playerHearts;
        playerHealthScript.maxHealth.value = data.maxHealth;
        playerHealthScript.currentHealth.value = data.playerHealth;
        soulsUI.slider.value = data.playerSouls;
        playerInventory.numberOfKeys = data.numberOfKeys;
        playerInventory.numberOfSouls = data.numberOfSouls;

        playerHealthScript.transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);

        healthUI.UpdateHearts();
        Debug.Log("Game was Loaded");
/*
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);*/
    }

    public void ResetGame()
    {
        healthUI.heartContainer.value = 3;
        playerHealthScript.currentHealth.value = 6;
        playerHealthScript.maxHealth.value = 6;
        soulsUI.slider.value = 0;
        playerInventory.numberOfKeys = 0;
        playerInventory.numberOfSouls = 0;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
