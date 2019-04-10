using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Data : MonoBehaviour
{
    public static Data Instance;
    private GranadeSettings[] Granades;
    private VestSettings[] Vests;
    private GunSettings[] Guns;
    private KnifeSettings[] Knives;

    public string CurrentVest;
    public string CurrentGun;
    public string CurrentKnife;

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }

    public GranadeSettings GetGranadeSettings(string Name)
    {
        foreach (GranadeSettings g in Granades)
        {
            if (g.Name == Name) return g;
        }
        throw new Exception("Granade " + Name + " does note exit");
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

}
