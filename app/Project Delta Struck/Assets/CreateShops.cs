using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateShops : MonoBehaviour {
    public GameObject Shop;
	// Use this for initialization
	void Start () {
		
	}

    public void CreateKnivesShop()
    {
        foreach (var item in Data.Instance.Knives)
        {
            GameObject ShopObj = Instantiate(Shop);
            ShopManager shopManager = ShopObj.GetComponent<ShopManager>();
        }
    }

    public void CreateVestsShop()
    {

    }

    public void CreateGunsShop()
    {

    }

    public void CreateGrenadesShop()
    {

    }
}
