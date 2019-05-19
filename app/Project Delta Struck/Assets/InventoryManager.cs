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
    public Transform HalmetsScrollRect;
    public GameObject ListItem;

    // Use this for initialization
    void Start() {
        if (Data.Instance.UserItemsLoaded)
        {
            print("User items loaded already");
            FIllInventoryWithData();
            return;
        }
        else
        {
            print("Calling get user items");
            SaveSystem.Instance.CallGetUserItems();
            StartCoroutine(RefreshItems());
        }
    }

    public IEnumerator RefreshItems()
    {
        while (!Data.Instance.UserItemsLoaded)
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
        Transform HalmetsContent = HalmetsScrollRect.Find("Content");

        ToggleGroup TGGunsContent = GunsScrollRect.Find("Content").GetComponent<ToggleGroup>();
        ToggleGroup TGKnivesContent   = KnivesScrollRect.Find("Content").GetComponent<ToggleGroup>();
        ToggleGroup TGGrenadesContent = GrenadesScrollRect.Find("Content").GetComponent<ToggleGroup>();
        ToggleGroup TGVestsContent    = VestsScrollRect.Find("Content").GetComponent<ToggleGroup>();
        ToggleGroup TGHalmetsContent  = HalmetsScrollRect.Find("Content").GetComponent<ToggleGroup>();
        foreach (var item in Data.Instance.Guns)
        {
            if (Data.Instance.userItems.Contains(item.Name))
            {
                GameObject listItem = Instantiate(ListItem, GunsContent);
                listItem.transform.Find("Label").GetComponent<Text>().text = item.Name;
                Toggle t = listItem.GetComponent<Toggle>();
                t.group = TGGunsContent;
                t.isOn = Data.Instance.CurrentGun == item.Name;
                t.onValueChanged.AddListener(delegate {
                    if (t.isOn)
                    {
                        Data.Instance.PlayerData.CurrentGun = item.Name;
                    }
                    else
                    {
                    }
                });
            }
        }
        foreach (var item in Data.Instance.Knives)
        {
            if (Data.Instance.userItems.Contains(item.Name))
            {
                GameObject listItem = Instantiate(ListItem, KnivesContent);
                listItem.transform.Find("Label").GetComponent<Text>().text = item.Name;
                Toggle t = listItem.GetComponent<Toggle>();
                t.group = TGKnivesContent;
                t.isOn = Data.Instance.CurrentKnife == item.Name;
                t.onValueChanged.AddListener(delegate {
                    if (t.isOn)
                    {
                        Data.Instance.PlayerData.CurrentKnife = item.Name;
                    }
                    else
                    {
                    }
                });
            }
        }

        foreach (var item in Data.Instance.Grenades)
        {
            if (Data.Instance.userItems.Contains(item.Name))
            {
                GameObject listItem = Instantiate(ListItem, GrenadesContent);
                listItem.transform.Find("Label").GetComponent<Text>().text = item.Name;
                Toggle t = listItem.GetComponent<Toggle>();
                t.group = TGGrenadesContent;
                t.isOn = Data.Instance.CurrentGrenade == item.Name;
                t.onValueChanged.AddListener(delegate {
                    if (t.isOn)
                    {
                        Data.Instance.PlayerData.CurrentGrenade = item.Name;
                    }
                    else
                    {
                    }
                });
            }
        }

        foreach (var item in Data.Instance.Vests)
        {
            if (Data.Instance.userItems.Contains(item.Name))
            {
                GameObject listItem = Instantiate(ListItem, VestsContent);
                listItem.transform.Find("Label").GetComponent<Text>().text = item.Name;
                Toggle t = listItem.GetComponent<Toggle>();
                t.group = TGVestsContent;
                t.isOn = Data.Instance.CurrentVest == item.Name;
                t.onValueChanged.AddListener(delegate {
                    if (t.isOn)
                    {
                        Data.Instance.PlayerData.CurrentVest = item.Name;
                    }
                    else
                    {
                    }
                });
            }
        }

        foreach (var item in Data.Instance.Halmets)
        {
            if (Data.Instance.userItems.Contains(item.Name))
            {
                GameObject listItem = Instantiate(ListItem, VestsContent);
                listItem.transform.Find("Label").GetComponent<Text>().text = item.Name;
                Toggle t = listItem.GetComponent<Toggle>();
                t.group = TGHalmetsContent;
                t.isOn = Data.Instance.CurrentHalmet == item.Name;
                t.onValueChanged.AddListener(delegate {
                    if (t.isOn)
                    {
                        Data.Instance.PlayerData.CurrentHalmet = item.Name;
                    }
                    else
                    {
                    }
                });
            }
        }
    }
    public void SaveSelectedItems()
    {
        SaveSystem.Instance.SavePlayer(Data.Instance.PlayerData);
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
