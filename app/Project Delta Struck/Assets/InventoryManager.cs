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

    bool itemsLoaded = false;
    // Use this for initialization
    void Start() {
        if (Data.Instance.ItemsLoaded)
        {
            FIllInventoryWithData();
            return;
        }
        CallGetItems();
        Data.Instance.ItemsLoaded = true;
    }
	

    public void CallGetItems()
    {
        StartCoroutine(GetItems());
    }

    IEnumerator GetItems()
    {
        WWWForm form = new WWWForm();
        Debug.Log(DBManager.username);
        form.AddField("username", DBManager.username);
        WWW www = new WWW("http://codeblacksmith.tk/ProjectDeltaStruct/getAllItems.php", form);
        yield return www;
        Debug.Log(www.text);
        FromJsonToObjects(www.text);
        CallGetUserItems();
    }
    public void CallGetUserItems()
    {
        StartCoroutine(GetUserItems());
    }

    IEnumerator GetUserItems()
    {
        WWWForm form = new WWWForm();
        Debug.Log(DBManager.username);
        form.AddField("username", DBManager.username);
        WWW www = new WWW("http://codeblacksmith.tk/ProjectDeltaStruct/getUserItems.php", form);
        yield return www;
        Debug.Log(www.text);
        SetUserItems(www.text);
        FIllInventoryWithData();
    }

    public void SetUserItems(string list)
    {
        string[] items = list.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        Data.Instance.userItems = items.ToList();
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
            if (Data.Instance.userItems.Contains(item.Name))
            {
                GameObject listItem = Instantiate(ListItem, GrenadesContent);
                listItem.transform.Find("Label").GetComponent<Text>().text = item.Name;
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

    public void FillShopsWithData()
    {
        
    }

    string[] objSeperators = { "Object:" };
    void FromJsonToObjects(string json)
    {
        string[] objects = json.Split(objSeperators, System.StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < objects.Length; i++)
        {
            string[] objectParts = objects[i].Split('|');
            string Type = objectParts[0];
            string Name = objectParts[1];
            string Description = objectParts[2];
            string Object = objectParts[3];
            print("Type: " + Type);
            print("Name: " + Name);
            print("Description: " + Description);
            print("objecet: " + Object);
            switch (Type)
            {
                case "Gun":
                    GunStats gunStats = JsonUtility.FromJson<GunStats>(Object);
                    GunSettings gun = ScriptableObject.CreateInstance<GunSettings>();
                    gun.Fill(gunStats, Name, Description);
                    Data.Instance.AddGun(gun);
                    Data.Instance.PlayerData.AddGun(gun.Name);
                    break;
                case "Vest":
                    VestStats vestStats = JsonUtility.FromJson<VestStats>(Object);
                    VestSettings vest = ScriptableObject.CreateInstance<VestSettings>();
                    vest.Fill(Name, Description, vestStats);
                    Data.Instance.PlayerData.AddVest(vest.Name);
                    Data.Instance.AddVest(vest);
                    break;
                case "Knife":
                    KnifeStats knifeStats = JsonUtility.FromJson<KnifeStats>(Object);
                    KnifeSettings knife = ScriptableObject.CreateInstance<KnifeSettings>();
                    knife.Fill(knifeStats, Name, Description);
                    Data.Instance.PlayerData.AddGun(knife.Name);
                    Data.Instance.AddKnife(knife);
                    break;
                case "Grenade":
                    GrenadeStats grenadeStats = JsonUtility.FromJson<GrenadeStats>(Object);
                    GrenadeSettings grenade = ScriptableObject.CreateInstance<GrenadeSettings>();
                    grenade.Fill(grenadeStats, Name, Description);
                    Data.Instance.PlayerData.AddGun(grenade.Name);
                    Data.Instance.AddGrenade(grenade);
                    break;
                default:
                    Debug.Log("Unrecognised item type");
                    break;
            }
        }
        itemsLoaded = true;
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
