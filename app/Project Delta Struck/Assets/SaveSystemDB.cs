using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.IO;

public static class SaveSystemDB
{
    /// <summary>
    /// Player combo (vehicle and rockets equipped) data file path
    /// </summary>

    public static bool PlayerDataExists()
    {
        throw new Exception("Unimplemented");
    }

    public static void SavePlayer(PlayerData data)
    {
        
    }

    public static PlayerData LoadPlayer()
    {
        throw new Exception("Unimplemented");
    }
}
