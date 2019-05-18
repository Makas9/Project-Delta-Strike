using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Halmet", menuName = "Halmet")]
public class HalmetSettings : ScriptableObject
{
    public string Name;
    public string Description = "Default granade";
    public HalmetStats HalmetStats;
    public Sprite Sprite;

    public void Fill(string name, string description, HalmetStats HalmetStats)
    {
        Name = name;
        Description = description;
        this.HalmetStats = HalmetStats;
    }
}
