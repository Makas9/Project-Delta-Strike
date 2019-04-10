using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

[System.Serializable]
public class PlayerData
{
    public float Money = 1000f;
    public int LevelReached = 1;
    public GrenadeData[] Granades;
    public string[]      Vests;
    public string[]      Guns;
    public string[]      Knives;
    public int VestsCount;
    public int GunsCount;
    public int KnivesCount;
    public int GranadesCount;
    public string CurrentVest;
    public string CurrentGun;
    public string CurrentKnife;

    public PlayerData(string CurrentVest, string CurrentGun, string CurrentKnife)
    {
        Vests  = new string[1];
        Guns   = new string[1];
        Knives = new string[1];
        this.CurrentVest  = Vests [0] = CurrentVest;
        this.CurrentGun   = Guns  [0] = CurrentGun;
        this.CurrentKnife = Knives[0] = CurrentKnife;

        Granades = new GrenadeData[0];
    }

    public void AddKnife(string Name)
    {
        foreach (string Knife in Knives)
        {
            if (Knife == Name)
            {
                throw new Exception("Knife " + Name + " is already bought");
            }
        }

        AddToArray(Knives, Name);
        SaveSystem.SavePlayer(this);
    }

    public void AddGranade(string Name)
    {
        foreach (var Granade in Granades)
        {
            if (Granade.Name == Name)
            {
                Granade.Count++;
                return;
            }
        }

        AddToArray(Granades, new GrenadeData(Name, 1));
        SaveSystem.SavePlayer(this);
    }

    public void AddGun(string Name)
    {
        foreach (var Gun in Guns)
        {
            if (Gun == Name)
            {
                throw new Exception("Gun " + Name + " is already bought");
            }
        }

        AddToArray(Guns, Name);
        SaveSystem.SavePlayer(this);
    }

    public void AddVest(string Name)
    {
        foreach (string Vest in Vests)
        {
            if (Vest == Name)
            {
                throw new Exception("Vest " + Name + " is already bought");
            }
        }

        AddToArray(Vests, Name);
        SaveSystem.SavePlayer(this);
    }

    public void AddToArray<T>(T[] array, T value)
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
