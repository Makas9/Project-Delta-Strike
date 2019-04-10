using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.IO;

public static class SaveSystem
{
    /// <summary>
    /// Player statistics data file path
    /// </summary>
    static readonly string playerDataPath = Path.Combine(Application.persistentDataPath, "playerData.data");
    /// <summary>
    /// Player combo (vehicle and rockets equipped) data file path
    /// </summary>

    public static bool PlayerDataExists()
    {
        return File.Exists(playerDataPath);
    }

    public static void SavePlayer(PlayerData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(playerDataPath, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        Debug.Log(playerDataPath);
        if (File.Exists(playerDataPath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(playerDataPath, FileMode.Open);
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            Debug.Log("path exists");
            return data;
        }
        else
        {
            Debug.Log("Save file not found in " + playerDataPath);
            PlayerData data = new PlayerData("Basic", "Basic", "Basic");
            SavePlayer(data);
            return data;
        }
    }

    public static void DeletePlayerData()
    {
        if (File.Exists(playerDataPath))
            File.Delete(playerDataPath);
    }
}
