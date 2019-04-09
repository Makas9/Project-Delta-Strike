using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Weapon/Gun")]
public class GunSettings : ScriptableObject
{
    public string Name;
    public float BulletDamage = 1f;
    public float BulletsPerSecond = 5f;
    public float Price = 100f;
    public float ReloadTime = 2f;
    [TextArea(3, 10)]
    public string Description = "Default gun";
    public Sprite Sprite;
}
