using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Weapon/Gun")]
public class GunSettings : WeaponSettings
{
    public GunStats gunStats;

    public GunSettings(GunStats gunStats)
    {
        this.gunStats = gunStats;
    }
}
