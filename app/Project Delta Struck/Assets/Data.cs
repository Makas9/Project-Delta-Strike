using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Data : MonoBehaviour
{
    public static Data Instance;
    public GrenadeSettings[] Grenades;
    public VestSettings[] Vests;
    public GunSettings[] Guns;
    public KnifeSettings[] Knives;
    public List<string> userItems = new List<string>();
    [HideInInspector]
    public PlayerData PlayerData;
    [HideInInspector]
    public bool ItemsLoaded = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        //Debug.Log(JsonUtility.ToJson(Guns[0].gunStats));
        //Debug.Log(JsonUtility.ToJson(Vests[0]));
        //Debug.Log(JsonUtility.ToJson(Knives[0]));
        //Debug.Log(JsonUtility.ToJson(Grenades[0].stats));
    }

    public void AddGun(GunSettings settings)
    {
        AddToArray(ref Guns, settings);
    }
    public void AddGrenade(GrenadeSettings settings)
    {
        AddToArray(ref Grenades, settings);
    }
    public void AddVest(VestSettings settings)
    {
        AddToArray(ref Vests, settings);
    }
    public void AddKnife(KnifeSettings settings)
    {
        AddToArray(ref Knives, settings);
    }


    public void SetPlayerData(PlayerData data)
    {
        PlayerData = data;
    }

    public string CurrentVest => PlayerData.CurrentVest;
    public string CurrentGun => PlayerData.CurrentGun;
    public string CurrentKnife => PlayerData.CurrentKnife;

    public float Money
    {
        get => PlayerData.Money;
        set => PlayerData.Money = value;
    }

    public void AddGrenade(string Name) => PlayerData.AddGranade(Name);
    public void AddGun(string Name) => PlayerData.AddGun(Name);
    public GrenadeData GetGrenadeData(string Name) => PlayerData.GetGrenadeData(Name);

    public GrenadeSettings GetGrenadeSettings(string Name)
    {
        foreach (GrenadeSettings g in Grenades)
        {
            if (g.Name == Name) return g;
        }
        throw new Exception("Grenade " + Name + " does note exit");
    }

    public GunSettings GetGunSettings(string Name)
    {
        foreach (GunSettings g in Guns)
        {
            if (g.Name == Name) return g;
        }
        throw new Exception("Gun " + Name + " does note exit");
    }

    public KnifeSettings GetKnifeSettings(string Name)
    {
        foreach (KnifeSettings g in Knives)
        {
            if (g.Name == Name) return g;
        }
        throw new Exception("Knife " + Name + " does note exit");
    }

    public VestSettings GetVestSettings(string Name)
    {
        foreach (VestSettings g in Vests)
        {
            if (g.Name == Name) return g;
        }
        throw new Exception("Vest " + Name + " does note exit");
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
