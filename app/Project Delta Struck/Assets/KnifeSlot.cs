using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnifeSlot : ShopSlot {

    public override void Fill(ScriptableObject obj)
    {
        KnifeSettings settings = obj as KnifeSettings;
        GetTitle().text = settings.Name;
        GetPrice().text = settings.knifeStats.Price.ToString();
        Image img = GetImage();
        img.sprite = settings.Sprite;
        Manager.DescriptionLbl.text = settings.Description;
    }


    public override void Buy()
    {
        string Name = GetTitle().text;
        float Price = Data.Instance.GetKnifeSettings(Name).knifeStats.Price;
        if (Price <= Data.Instance.Money)
        {
            Data.Instance.Money -= Price;
            Manager.MoneyDisplay.UpdateMoney();
            Debug.Log(Name);
            Data.Instance.PlayerData.AddItemToInventoryDB(Name);
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

        KnifeSettings settings = Data.Instance.GetKnifeSettings(Name);
        Manager.DescriptionLbl.text = settings.Description;
    }
}
