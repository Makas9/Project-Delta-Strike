using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Knife", menuName = "Weapon/Knife")]
public class KnifeSettings : WeaponSettings
{

    public KnifeStats knifeStats;

    public KnifeSettings(KnifeStats knifeStats)
    {
        this.knifeStats = knifeStats;
    }
}
