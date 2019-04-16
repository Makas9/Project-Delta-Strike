using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunSlot : ShopSlot {

    public override void Fill(ScriptableObject obj)
    {
        GunSettings settings = obj as GunSettings;
        GetTitle().text = transform.name = settings.Name;
        GetPrice().text = settings.Price.ToString();
        Image img = GetImage();
        img.sprite = settings.Sprite;
        Manager.DescriptionLbl.text = settings.Description;
    }


    public override void Buy()
    {
        string Name = GetTitle().text;
        float Price = Data.Instance.GetGrenadeSettings(Name).Price;
        if (Price <= Data.Instance.Money)
        {
            Data.Instance.Money -= Price;
            Manager.MoneyDisplay.UpdateMoney();
            Debug.Log(Name);
            Data.Instance.AddGun(Name);
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
