using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Granade", menuName = "Weapon/Granade")]
public class VestSettings
{
    public string Name;
    public string Description = "Default granade";
    public KnifeStats knifeStats;
    public Sprite Sprite;
}
