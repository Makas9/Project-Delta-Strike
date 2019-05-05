using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Granade", menuName = "Weapon/Granade")]
public class GrenadeSettings : WeaponSettings
{
    
    public GrenadeStats stats;

    public GrenadeSettings(GrenadeStats stats)
    {
        this.stats = stats;
    }
}
