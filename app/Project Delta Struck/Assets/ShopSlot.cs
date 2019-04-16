using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ShopSlot : MonoBehaviour {
    public ShopManager Manager;
	public virtual void Fill(ScriptableObject obj)
    {
        string Name = GetTitle().text;
        transform.name = Name;
    }

    public abstract void Buy();
    public virtual void Select()
    {
        Manager.SelectedOverlay.SetParent(transform, false);
        Manager.SelectedOverlay.SetSiblingIndex(0);
    }

    public Image GetImage()
    {
        return transform.Find("ImageContainer/Image").GetComponent<Image>();
    }

    public Text GetTitle()
    {
        return transform.Find("Title/Text").GetComponent<Text>();
    }

    public Text GetPrice()
    {
        return transform.Find("PriceContainer/Price").GetComponent<Text>();
    }

    public Button GetBuyButton()
    {
        return transform.Find("PriceContainer").GetComponent<Button>();
    }
}
