using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {
    public enum Shop { Guns, Grenades, Knives, Vests, Halmets }
    public string ShopTitle = "Shop";
    public bool TopRightBar = true;
    public Transform SelectedOverlay;
    public GameObject ShopSlot;
    public MoneyDisplay MoneyDisplay;
    public Text UpperRightLbl;
    public Text DescriptionLbl;
    public Text ShopTitleLbl;
    public Transform CardsContainer;
    private ScriptableObject[] Items;
    public Shop shop;
    // Use this for initialization
    void Start () {
        ShopTitleLbl.text = ShopTitle;
        FillItemsList();
        CreateShop();
    }

    public void FillItemsList()
    {
        switch (shop)
        {
            case Shop.Grenades:
                Items = Data.Instance.Grenades;
                break;
            case Shop.Guns:
                Items = Data.Instance.Guns;
                break;
            case Shop.Knives:
                Items = Data.Instance.Knives;
                break;
            case Shop.Vests:
                Items = Data.Instance.Vests;
                break;
            case Shop.Halmets:
                Items = Data.Instance.Halmets;
                break;
        }
    }

    public void CreateShop()
    {
        List<ShopSlot> Slots = new List<ShopSlot>();
        foreach (var item in Items)
        {
            ShopSlot slot = Instantiate(ShopSlot, CardsContainer).GetComponent<ShopSlot>();
            slot.Manager = this;
            slot.Fill(item);
            Slots.Add(slot);
        }
        if (Items.Length > 0)
        {
            Slots[0].Select();
        }
        UpperRightLbl.transform.parent.gameObject.SetActive(TopRightBar);
    }
}
