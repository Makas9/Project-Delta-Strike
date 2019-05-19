using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunSlot : ShopSlot {

    public override void Fill(ScriptableObject obj)
    {
        GunSettings settings = obj as GunSettings;
        GetTitle().text = settings.Name;
        GetPrice().text = settings.gunStats.Price.ToString();
        Image img = GetImage();
        Debug.Log(settings.Name + " " + (settings.Sprite == null));
        img.sprite = settings.Sprite;
        Manager.DescriptionLbl.text = settings.Description;
    }

    public override void Buy()
    {
        string Name = GetTitle().text;
        float Price = Data.Instance.GetGunSettings(Name).gunStats.Price;
        if (Price <= Data.Instance.Money)
        {
            Data.Instance.Money -= Price;
            Manager.MoneyDisplay.UpdateMoney();
            Debug.Log(Name);
            Data.Instance.PlayerData.AddItemToInventoryDB(Name);
            SaveSystem.Instance.SavePlayer(Data.Instance.PlayerData);
            Data.Instance.UserItemsLoaded = false;

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

        GunSettings settings = Data.Instance.GetGunSettings(Name);
        Manager.DescriptionLbl.text = settings.Description;
    }
}
