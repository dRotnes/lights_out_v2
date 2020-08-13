using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SavingData 
{
    public float playerSouls;
    public float playerHealth;
    public int playerHearts;
    public int numberOfKeys;
    public int numberOfSouls;
    public float maxHealth;
    public float[] position;
    public PlayerState state;
    public int scene;


    public SavingData(Player player, int sceneIndex)
    {
        numberOfSouls = player.numberOfSouls;
        numberOfKeys = player.numberOfKeys;
        
        playerSouls = player.souls;
       
        playerHearts = player.numberOfHearts;
        playerHealth = player.currentHealth;
        maxHealth = player.maxHealth;

        position = new float[3];
        position[0] = player.positions[0];
        position[1] = player.positions[1];
        position[2] = player.positions[2];
        state = player.state; 
        scene = sceneIndex;
    }
}
