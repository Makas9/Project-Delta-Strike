using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class GranadeSlot : ShopSlot
{
    public override void Fill(ScriptableObject obj)
    {
        GranadeSettings settings = obj as GranadeSettings;
        GetTitle().text = transform.name = settings.Name;
        GetPrice().text = settings.Price.ToString();
    }


    public override void Buy()
    {
        float Price = Data.Instance.GetGranadeSettings(GetTitle().text).Price;
        if (Price <= Data.Instance.Money)
        {
            Data.Instance.Money -= Price;
            Data.Instance.AddGrenade(GetTitle().text);
            MoneyDisplay.Instance.UpdateMoney();
        }
        else
        {
            // Pop up money
        }
    }
}
