using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Knife", menuName = "Weapon/Knife")]
public class KnifeSettings : WeaponSettings
{

    public KnifeStats knifeStats;

    public void Fill(KnifeStats knifeStats, string Name, string Description)
    {
        this.Name = Name;
        this.Description = Description;
        this.knifeStats = knifeStats;
    }
}
