using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Knife", menuName = "Weapon/Knife")]
public class KnifeSettings : WeaponSettings
{
    [HideInInspector]
    public string Name { get { return name; } }
    public float Price = 100f;
    [TextArea(3, 10)]
    public string Description = "Default knife";
    public Sprite Sprite;
}
