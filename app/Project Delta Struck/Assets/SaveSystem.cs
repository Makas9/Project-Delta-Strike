using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.IO;

public static class SaveSystem
{
    public static void SavePlayer(PlayerData data)
    {
        Debug.Log("Username: " + data.Username);
        string PlayerDataDir = Path.Combine(Application.persistentDataPath, data.Username);
        string PlayerDataPath = Path.Combine(PlayerDataDir, "playerData.data");
        if (!Directory.Exists(PlayerDataDir))
        {
            Directory.CreateDirectory(PlayerDataDir);
        }
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(PlayerDataPath, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer(string username)
    {
        string PlayerDataDir = Path.Combine(Application.persistentDataPath, username);
        string playerDataPath = Path.Combine(PlayerDataDir, "playerData.data");
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
            PlayerData data = new PlayerData("Basic", "Basic", "Basic", DBManager.username);
            SavePlayer(data);
            return data;
        }
    }

    public static void DeletePlayerData(string username)
    {
        string PlayerDataDir = Path.Combine(Application.persistentDataPath, username);
        string playerDataPath = Path.Combine(PlayerDataDir, "playerData.data");

        if (File.Exists(playerDataPath))
            File.Delete(playerDataPath);
    }
}
