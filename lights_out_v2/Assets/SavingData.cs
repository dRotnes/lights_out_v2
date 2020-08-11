using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SavingData 
{
    public float playerSouls;
    public float playerHealth;
    public float playerHearts;
    public int numberOfKeys;
    public int numberOfSouls;
    public float currentCheckPoint;
    public float maxHealth;
    public float[] position;

    public SavingData(HealthUI healthUI, SoulsUI soulsUI, PlayerHealth playerHealthScript, Inventory playerInventory, float[] checkPoint)
    {
        numberOfSouls = playerInventory.numberOfSouls;
        numberOfKeys = playerInventory.numberOfKeys;
        
        playerSouls = soulsUI.slider.value;
       
        playerHearts = healthUI.heartContainer.value;
        playerHealth = playerHealthScript.currentHealth.value;
        maxHealth = playerHealthScript.maxHealth.value;

        position = new float[3];
        position[0] = checkPoint[0];
        position[1] = checkPoint[1];
        position[2] = checkPoint[2];
    }
}
