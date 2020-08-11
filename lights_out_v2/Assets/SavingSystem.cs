﻿using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Pathfinding.Ionic.Zip;


public static class SavingSystem
{
    public static void SaveGame(HealthUI healthUI, SoulsUI soulsUI, PlayerHealth playerHealthScript, Inventory playerInventory, float[] positions)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/gamedata.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        SavingData data = new SavingData(healthUI, soulsUI, playerHealthScript, playerInventory, positions);
        formatter.Serialize(stream, data);
        stream.Close();

    }

    public static SavingData LoadGame()
    {
        string path = Application.persistentDataPath + "/gamedata.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SavingData data = formatter.Deserialize(stream) as SavingData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("Save File not found in " + path);
            return null;
        }
    }
}