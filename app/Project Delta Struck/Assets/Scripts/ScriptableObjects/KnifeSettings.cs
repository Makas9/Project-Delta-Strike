using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Knife", menuName = "Weapon/Knife")]
public class KnifeSettings : ScriptableObject
{
    public string Name;
    public float Damage = 1f;
    public float Price = 100f;
    [TextArea(3, 10)]
    public string Description = "Default knife";
    public Sprite Sprite;
}
