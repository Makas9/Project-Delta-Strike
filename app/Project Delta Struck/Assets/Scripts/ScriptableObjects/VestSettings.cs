using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Vest", menuName = "Vest")]
public class VestSettings : ScriptableObject
{
    [HideInInspector]
    public string Name { get { return name; } }
    public float Defence = 1f;
    public float Price = 100f;
    [TextArea(3, 10)]
    public string Description = "Default granade";
    //public Sprite Sprite;
}
