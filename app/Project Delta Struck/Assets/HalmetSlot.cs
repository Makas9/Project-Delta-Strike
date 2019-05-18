using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HalmetSlot : ShopSlot {

    public override void Fill(ScriptableObject obj)
    {
        HalmetSettings settings = obj as HalmetSettings;
        GetTitle().text = settings.Name;
        GetPrice().text = settings.HalmetStats.Price.ToString();
        Image img = GetImage();
        Debug.Log(settings.Name + " " + (settings.Sprite == null));
        img.sprite = settings.Sprite;
        Manager.DescriptionLbl.text = settings.Description;
    }

    public override void Buy()
    {
        string Name = GetTitle().text;
        float Price = Data.Instance.GetHalmetSettings(Name).HalmetStats.Price;
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

        HalmetSettings settings = Data.Instance.GetHalmetSettings(Name);
        Manager.DescriptionLbl.text = settings.Description;
    }
}
