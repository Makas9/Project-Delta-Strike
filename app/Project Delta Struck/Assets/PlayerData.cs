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
    public GrenadeData[] Grenades;
    public string[]      Vests;
    public string[]      Guns;
    public string[]      Knives;
    public int VestsCount;
    public int GunsCount;
    public int KnivesCount;
    public int GrenadesCount;
    public string CurrentVest;
    public string CurrentGun;
    public string CurrentKnife;
    public GrenadeData CurrentGrenades;

    public PlayerData(string CurrentVest, string CurrentGun, string CurrentKnife, string Username)
    {
        Vests  = new string[1];
        Guns   = new string[1];
        Knives = new string[1];
        this.CurrentVest  = Vests [0] = CurrentVest;
        this.CurrentGun   = Guns  [0] = CurrentGun;
        this.CurrentKnife = Knives[0] = CurrentKnife;
        this.Username = Username;
        Grenades = new GrenadeData[0];
    }

    public void AddKnife(string Name)
    {
        foreach (string Knife in Knives)
        {
            if (Knife == Name)
            {
                return;
            }
        }
        SaveSystem.Instance.CallAddItem(Name);
        AddToArray(ref Knives, Name);
        SaveSystem.Instance.SavePlayer(this);
    }


    public GrenadeData GetGrenadeData(string Name)
    {
        foreach (var Granade in Grenades)
        {
            if (Granade.Name == Name)
            {
                Debug.Log(Name);
                return Granade;
            }
        }
        Debug.Log("New");

        GrenadeData data = new GrenadeData(Name, 0);
        AddToArray(ref Grenades, data);
        Debug.Log("Len: " + Grenades.Length);
        return data;
    }
    public void AddGranade(string Name)
    {
        GrenadeData grenade = GetGrenadeData(Name);
        AddToArray(ref Grenades, grenade);
        grenade.Count++;
        SaveSystem.Instance.CallAddItem(Name);
    }

    public void AddGun(string Name)
    {
        foreach (var Gun in Guns)
        {
            if (Gun == Name)
            {
                return;
            }
        }

        AddToArray(ref Guns, Name);
        SaveSystem.Instance.CallAddItem(Name);
    }

    public void AddVest(string Name)
    {
        foreach (string Vest in Vests)
        {
            if (Vest == Name)
            {
                return;
            }
        }

        SaveSystem.Instance.CallAddItem(Name);
        AddToArray(ref Vests, Name);
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
