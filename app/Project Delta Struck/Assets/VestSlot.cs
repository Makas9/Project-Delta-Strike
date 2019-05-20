using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VestSlot : ShopSlot {

    public override void Fill(ScriptableObject obj)
    {
        VestSettings settings = obj as VestSettings;
        GetTitle().text = settings.Name;
        GetPrice().text = settings.vestStats.Price.ToString();
        Image img = GetImage();
        img.sprite = settings.Sprite;
        Manager.DescriptionLbl.text = settings.Description;
    }


    public override void Buy()
    {
        string Name = GetTitle().text;
        float Price = Data.Instance.GetVestSettings(Name).vestStats.Price;
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

        VestSettings settings = Data.Instance.GetVestSettings(Name);
        Manager.DescriptionLbl.text = settings.Description;
    }
}
