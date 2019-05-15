using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Coin : CollectableItem
{
    public int Worth = -1;
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (Collected) return;
        PickUp();
        if (Worth == -1)
        {
            Debug.LogWarning("The value of coin has not been asigned");
            return;
        }
        GameMaster.Instance.AddMoney(Worth);
        Collected = true;
    }
}