using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Vest", menuName = "Weapon/vest")]
public class VestSettings : ScriptableObject
{
    public string Name;
    public string Description = "Default granade";
    public VestStats vestStats;
    public Sprite Sprite;

    public void Fill(string name, string description, VestStats vestStats)
    {
        Name = name;
        Description = description;
        this.vestStats = vestStats;
    }
}
