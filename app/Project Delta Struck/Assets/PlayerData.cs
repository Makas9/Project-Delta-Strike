using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
[System.Serializable]
public class PlayerData
{
    public string Username;
    public float Money = 1000f;
    public int LevelReached = 1;
    public int VestsCount;
    public int GunsCount;
    public int KnivesCount;
    public string CurrentVest;
    public string CurrentGun;
    public string CurrentKnife;
    public string CurrentGrenade;
    public string CurrentHalmet;
    public GrenadeData CurrentGrenades;

    public PlayerData(string CurrentVest, string CurrentGun, string CurrentKnife, string CurrentHalmet, string Username)
    {

        this.CurrentVest  = CurrentVest;
        this.CurrentGun   = CurrentGun;
        this.CurrentKnife = CurrentKnife;
        this.CurrentHalmet = CurrentHalmet;
        this.Username = Username;
    }


    public void AddItemToInventoryDB(string Name)
    {
        SaveSystem.Instance.CallAddItem(Name);
    }

    public void AddToArray<T>(ref T[] array, T value)
    {
        if (array == null)
        {
            array = new T[1];
            array[0] = value;
            return;
        }
        List<T> newArrayList = array.ToList();
        newArrayList.Add(value);
        array = newArrayList.ToArray();
    }
}
