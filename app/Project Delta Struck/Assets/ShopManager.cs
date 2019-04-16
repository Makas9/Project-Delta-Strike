using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {
    public string ShopTitle = "Shop";
    public bool TopRightBar = true;
    public Transform SelectedOverlay;
    public GameObject ShopSlot;
    public MoneyDisplay MoneyDisplay;
    public Text UpperRightLbl;
    public Text DescriptionLbl;
    public Text ShopTitleLbl;
    public Transform CardsContainer;
    public ScriptableObject[] Items;
    // Use this for initialization
    void Start () {
        ShopTitleLbl.text = ShopTitle;
        CreateShop();

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
        Slots[0].Select();
        UpperRightLbl.transform.parent.gameObject.SetActive(TopRightBar);
    }
}
