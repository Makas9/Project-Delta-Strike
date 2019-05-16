using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {
    public Transform GunsScrollRect;
    public Transform KnivesScrollRect;
    public Transform VestsScrollRect;
    public Transform GrenadesScrollRect;
    public GameObject ListItem;

    
    // Use this for initialization
    void Start() {
        if (Data.Instance.ItemsLoaded)
        {
            FIllInventoryWithData();
            return;
        }
        else
        {
            SaveSystem.Instance.CallGetItems();
            StartCoroutine(RefreshItems());
        }
    }

    public IEnumerator RefreshItems()
    {
        while (!Data.Instance.ItemsLoaded)
        {
            print("items not loaded return");
            yield return null;
        }
        FIllInventoryWithData();
    }


    public void FIllInventoryWithData()
    {
        Transform GunsContent = GunsScrollRect.Find("Content");
        Transform KnivesContent = KnivesScrollRect.Find("Content");
        Transform GrenadesContent = GrenadesScrollRect.Find("Content");
        Transform VestsContent = VestsScrollRect.Find("Content");
        foreach (var item in Data.Instance.Guns)
        {
            if (Data.Instance.userItems.Contains(item.Name))
            {
                GameObject listItem = Instantiate(ListItem, GunsContent);
                listItem.transform.Find("Label").GetComponent<Text>().text = item.Name;
            }
        }
        foreach (var item in Data.Instance.Knives)
        {
            if (Data.Instance.userItems.Contains(item.Name))
            {
                GameObject listItem = Instantiate(ListItem, KnivesContent);
                listItem.transform.Find("Label").GetComponent<Text>().text = item.Name;
            }
        }

        foreach (var item in Data.Instance.Grenades)
        {
            print("grenade");
            if (Data.Instance.userItems.Contains(item.Name))
            {
                for (int i = 0; i < Data.Instance.userItems.Count(s => s == item.Name); i++)
                {
                    GameObject listItem = Instantiate(ListItem, GrenadesContent);
                    listItem.transform.Find("Label").GetComponent<Text>().text = item.Name;
                }
            }
        }

        foreach (var item in Data.Instance.Vests)
        {
            if (Data.Instance.userItems.Contains(item.Name))
            {
                GameObject listItem = Instantiate(ListItem, VestsContent);
                listItem.transform.Find("Label").GetComponent<Text>().text = item.Name;
            }
        }
    }
}

/*
switch (Type)
            {
                case "Gun":
                    GunStats gunStats = JsonUtility.FromJson<GunStats>(Object);
                    GunSettings gun = new GunSettings(gunStats, Name, Description);
                    Data.Instance.AddGun(gun);
                    break;
                case "Vest":
                    VestStats vestStats = JsonUtility.FromJson<VestStats>(Object);
                    VestSettings vest = new VestSettings(Name, Description, vestStats);
                    Data.Instance.AddVest(gun);
                    break;
                case "Grenade":
                    KnifeStats knifeStats = JsonUtility.FromJson<KnifeStats>(Object);
                    KnifeSettings knife = new KnifeSettings(knifeStats, Name, Description);
                    Data.Instance.AddKnife(knife);
                    break;
                case "Knife":
                    GrenadeStats grenadeStats = JsonUtility.FromJson<GrenadeStats>(Object);
                    GrenadeSettings grenade = new GrenadeSettings(grenadeStats, Name, Description);
                    Data.Instance.AddGrenade(grenade);
                    break;
                default:
                    Debug.Log("Unrecognised item type");
                    break;
            }
            
     
switch (Type)
            {
                case "Gun":
                    GunStats gunStats = JsonUtility.FromJson<GunStats>(Object);
                    GunSettings gun = ScriptableObject.CreateInstance<GunSettings>();
                    gun.Fill(gunStats, Name, Description);
                    Data.Instance.AddGun(gun);
                    break;
                case "Vest":
                    VestStats vestStats = JsonUtility.FromJson<VestStats>(Object);
                    VestSettings vest = ScriptableObject.CreateInstance<VestSettings>();
                    vest.Fill(Name, Description, vestStats);
                    Data.Instance.AddVest(vest);
                    break;
                case "Grenade":
                    KnifeStats knifeStats = JsonUtility.FromJson<KnifeStats>(Object);
                    KnifeSettings knife = ScriptableObject.CreateInstance<KnifeSettings>();
                    knife.Fill(knifeStats, Name, Description);
                    Data.Instance.AddKnife(knife);
                    break;
                case "Knife":
                    GrenadeStats grenadeStats = JsonUtility.FromJson<GrenadeStats>(Object);
                    GrenadeSettings grenade = ScriptableObject.CreateInstance<GrenadeSettings>();
                    grenade.Fill(grenadeStats, Name, Description);
                    Data.Instance.AddGrenade(grenade);
                    break;
                default:
                    Debug.Log("Unrecognised item type");
                    break;
            }
     */
