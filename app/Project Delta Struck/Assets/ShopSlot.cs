using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ShopSlot : MonoBehaviour {

	public virtual void Fill(ScriptableObject obj)
    {
        string Name = GetTitle().text;
        transform.name = Name;
    }

    public abstract void Buy();
    public virtual void Select()
    {
        ShopReferences.Instance.SelectedOverlay.SetParent(transform, false);
        ShopReferences.Instance.SelectedOverlay.SetSiblingIndex(0);
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
