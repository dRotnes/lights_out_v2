using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Pathfinding.Ionic.Zip;
using System.Collections.Generic;

public static class SavingSystem
{
    public static DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath + "/gamedata.data");
    public static void SaveGame(Player player, bool[] chestsArray, bool[] flArray, bool[] wbArray, bool[] enArray, int sceneIndex)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = dir.FullName;
        FileStream stream = new FileStream(path, FileMode.Create);

        SavingData data = new SavingData(player, chestsArray, flArray, wbArray, enArray, sceneIndex);
        formatter.Serialize(stream, data);
        stream.Close();

    }

    public static SavingData LoadGame()
    {
        string path = dir.FullName;
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
