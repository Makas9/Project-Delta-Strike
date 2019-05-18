using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class GrenadeSlot : ShopSlot
{
    public override void Fill(ScriptableObject obj)
    {
        GrenadeSettings settings = obj as GrenadeSettings;
        GetTitle().text = transform.name = settings.Name;
        GetPrice().text = settings.stats.Price.ToString();
        Image img = GetImage();
        img.sprite = settings.Sprite;
        Manager.DescriptionLbl.text = settings.Description;
    }


    public override void Buy()
    {
        string Name = GetTitle().text;
        float Price = Data.Instance.GetGrenadeSettings(Name).stats.Price;
        if (Price <= Data.Instance.Money)
        {
            Data.Instance.Money -= Price;
            Manager.MoneyDisplay.UpdateMoney();
            Debug.Log(Name);
            int count = Data.Instance.GetGrenadesCount(Name);
            Data.Instance.PlayerData.AddItemToInventoryDB(Name);
            Manager.UpperRightLbl.text = count.ToString();
            SaveSystem.Instance.SavePlayer(Data.Instance.PlayerData);
            Data.Instance.ItemsLoaded = false;
        }
        else
        {
            // Pop up money
        }
    }

    public override void Select()
    {
        base.Select();
        string Name = GetTitle().text;
        int count = Data.Instance.GetGrenadesCount(Name);
        Manager.UpperRightLbl.text = count.ToString();

        GrenadeSettings settings = Data.Instance.GetGrenadeSettings(Name);
        Manager.DescriptionLbl.text = settings.Description;
    }
}
