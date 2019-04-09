using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Vest", menuName = "Vest")]
public class VestSettings : ScriptableObject
{
    public string Name;
    public float Defence = 1f;
    public float Price = 100f;
    [TextArea(3, 10)]
    public string Description = "Default granade";
    public Sprite Sprite;
}
