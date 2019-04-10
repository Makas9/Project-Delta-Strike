using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Granade", menuName = "Weapon/Granade")]
public class GranadeSettings : WeaponSettings
{
    [HideInInspector]
    public string Name { get { return name; } }
    public float ExplosionRadius = 1f;
    public float Price = 100f;
    public float Weight = 1f;
    [TextArea(3, 10)]
    public string Description = "Default granade";
    public Sprite Sprite;
}
