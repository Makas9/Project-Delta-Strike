using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
[System.Serializable]
public class GrenadeData
{
    public string Name;
    public int Count;

    public GrenadeData(string Name, int Count)
    {
        this.Name = Name;
        this.Count = Count;
    }
}
