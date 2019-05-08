using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Granade", menuName = "Weapon/Granade")]
public class GrenadeSettings : WeaponSettings
{
    
    public GrenadeStats stats;

    public void Fill(GrenadeStats stats, string Name, string Description)
    {
        this.Name = Name;
        this.Description = Description;
        this.stats = stats;
    }
}
