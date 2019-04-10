using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

[System.Serializable]
public class PlayerData
{
    public float CoinsEarned = 100f;
    public int LevelReached = 1;
    public string[] Granades;
    public string[] Vests;
    public GunData[] Guns;
    public string[] Knives;
    public int VestsCount;
    public int GunsCount;
    public int KnivesCount;
    public int GranadesCount;
    public string CurrentVest;
    public string CurrentGun;
    public string CurrentKnife;

    public GunData GetCurrentGunData()
    {
        foreach (GunData data in Guns)
        {
            if (data.Name == CurrentGun) return data;
        }
        throw new Exception("Gun data " + CurrentGun + " doesnt exist");
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
        foreach (string Granade in Granades)
        {
            if (Granade == Name)
            {
                throw new Exception("Knife " + Name + " is already bought");
            }
        }

        AddToArray(Granades, Name);
        SaveSystem.SavePlayer(this);
    }

    public void AddVeste(string Name)
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

    public string[] AddToArray(string[] array, string value)
    {
        List<string> newArrayList = array.ToList();
        newArrayList.Add(value);
        return newArrayList.ToArray();
    }
}
