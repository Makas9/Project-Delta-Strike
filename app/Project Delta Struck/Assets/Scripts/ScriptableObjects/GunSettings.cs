using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Weapon/Gun")]
public class GunSettings : WeaponSettings
{
    public GunStats gunStats;
    public void Fill(GunStats gunStats, string Name, string Description)
    {
        this.Name = Name;
        this.Description = Description;
        this.gunStats = gunStats;
    }
}
